using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    [SerializeField] private ScreenFadeController _fadeController;
    public bool IsSceneLoading => _isBusy;

    private readonly Dictionary<string, string> _loadedSceneBySlot = new();
    private readonly Queue<SceneTransitionPlan> _pendingPlans = new();
    private bool _isBusy;

    public SceneTransitionPlan CreateTransition()
    {
        return new SceneTransitionPlan();
    }

    private void ExecuteTransition(SceneTransitionPlan plan)
    {
        if (_isBusy)
        {
            _pendingPlans.Enqueue(plan);
            return;
        }
        
        _isBusy = true;
        StartCoroutine(TransitionCoroutine(plan));
    }

    private IEnumerator TransitionCoroutine(SceneTransitionPlan plan)
    {
        if (plan.Overlay && _fadeController != null)
        {
            yield return _fadeController.FadeInRoutine();
        }

        foreach (var slotKey in plan.ScenesToUnload)
        {
            yield return UnloadSceneRoutine(slotKey);
        }
        
        if (plan.ClearUnusedAssets) yield return CleanupUnusedAssetsRoutine();
        
        foreach (var kvp in plan.SceneToLoad)
        {
            if (_loadedSceneBySlot.ContainsKey(kvp.Key))
            {
                yield return UnloadSceneRoutine(kvp.Key);
            }
            yield return LoadAdditiveSceneRoutine(kvp.Key, kvp.Value, plan.ActiveSceneName == kvp.Value);
        }

        if (plan.Overlay && _fadeController != null)
        {
            yield return _fadeController.FadeOutRoutine();
        }

        _isBusy = false;
        
        if (_pendingPlans.Count > 0)
        {
            ExecuteTransition(_pendingPlans.Dequeue());
        }
    }

    private IEnumerator LoadAdditiveSceneRoutine(string slotKey, string sceneName, bool setActive)
    {
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        if (loadOp == null) yield break;
        loadOp.allowSceneActivation = false;
        while (loadOp.progress < 0.9f) yield return null;
        loadOp.allowSceneActivation = true;
        while (!loadOp.isDone) yield return null;
        if (setActive)
        {
            Scene newScene = SceneManager.GetSceneByName(sceneName);
            if (newScene.IsValid() && newScene.isLoaded)
            {
                SceneManager.SetActiveScene(newScene);
            }
        }
        _loadedSceneBySlot[slotKey] = sceneName;
    }

    private IEnumerator UnloadSceneRoutine(string slotKey)
    {
        if (!_loadedSceneBySlot.TryGetValue(slotKey, out string sceneName)) yield break;
        if (string.IsNullOrEmpty(sceneName)) yield break;
        AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(sceneName);
        if (unloadOp != null)
        {
            while (!unloadOp.isDone) yield return null;
        }
        _loadedSceneBySlot.Remove(slotKey);
    }

    private IEnumerator CleanupUnusedAssetsRoutine()
    {
        AsyncOperation unloadOp = Resources.UnloadUnusedAssets();
        if (unloadOp != null)
        {
            while (!unloadOp.isDone) yield return null;
        }
    }

    public class SceneTransitionPlan
    {
        public Dictionary<string, string> SceneToLoad { get; } = new();
        public List<string> ScenesToUnload { get; } = new();
        public string ActiveSceneName { get; private set; } = "";
        public bool ClearUnusedAssets { get; private set; }
        public bool Overlay { get; private set; }

        public SceneTransitionPlan Load(string slotKey, string sceneName, bool setActive = false)
        {
            SceneToLoad.Add(slotKey, sceneName);
            if (setActive) ActiveSceneName = sceneName;
            return this;
        }
        public SceneTransitionPlan Unload(string sceneName)
        {
            ScenesToUnload.Add(sceneName);
            return this;
        }
        public SceneTransitionPlan WithFade()
        {
            Overlay = true;
            return this;
        }
        public SceneTransitionPlan ClearAssets()
        {
            ClearUnusedAssets = true;
            return this;
        }
        public void Execute()
        {
            Instance.ExecuteTransition(this);
        }
    }
}