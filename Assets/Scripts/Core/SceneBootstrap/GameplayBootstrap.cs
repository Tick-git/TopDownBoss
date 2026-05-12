using UnityEngine;
using UnityEngine.UIElements;

public class GameplayBootstrap : MonoBehaviour
{
    [SerializeField] HUDManager _hudManager;

    private PauseView _pauseView;
    private PauseManager _pauseManager;
    private SettingView _settingsView;

    private void Awake()
    {
        if (!Root.HasInstance) return;

        var viewStack = Root.Instance.ViewStack;
        var audioEmitterUI = Root.Instance.AudioEmitterUI;
        var gameFlow = Root.Instance.GameFlowService;
        var settingsManager = Root.Instance.SettingsManager;
        var inputReader = Root.Instance.InputReader;

        _pauseManager = new PauseManager();
        viewStack.ActiveViewChanged += OnActiveViewChanged;

        _settingsView = CreateSettingView(viewStack, settingsManager, audioEmitterUI);
        _pauseView = new PauseView(viewStack.GetUIRoot().Q<VisualElement>("PauseView"), audioEmitterUI);

        _settingsView.LeaveSettingRequested += viewStack.Pop;

        _pauseView.ResumeRequested += OnResumeRequested;
        _pauseView.SettingsRequested += viewStack.Push<SettingView>;
        _pauseView.MainMenuRequested += gameFlow.LoadMainMenu;
        _pauseView.ExitRequested += Application.Quit;

        inputReader.PausePerformed += OnPausePerformed;

        viewStack.Register(_settingsView);
        viewStack.Register(_pauseView);
    }

    private void OnActiveViewChanged(ActiveViewChangedArgs args)
    {
        if (args.IsFirstActiveView)
        {
            _hudManager.HideHUD();
        }
        else if (args.HasNoActiveView)
        {
            _hudManager.ShowHUD();
        }
    }

    private void OnDestroy()
    {
        if (!Root.HasInstance) return;

        var viewStack = Root.Instance.ViewStack;
        var gameFlow = Root.Instance.GameFlowService;
        var uiInputReader = Root.Instance.InputReader;

        viewStack.ActiveViewChanged -= OnActiveViewChanged;
        _settingsView.LeaveSettingRequested -= OnResumeRequested;

        _pauseView.ResumeRequested -= viewStack.Pop;
        _pauseView.SettingsRequested -= viewStack.Push<SettingView>;
        _pauseView.MainMenuRequested -= gameFlow.LoadMainMenu;
        _pauseView.ExitRequested -= Application.Quit;

        uiInputReader.PausePerformed -= OnPausePerformed;

        _pauseManager.Dispose();
        _pauseView.Dispose();
        _settingsView.Dispose();

        viewStack.Unregister(_settingsView);
        viewStack.Unregister(_pauseView);
    }

    private void OnPausePerformed()
    {
        var viewStack = Root.Instance.ViewStack;

        if (viewStack.HasActiveView)
        {
            _pauseManager.Resume();
            viewStack.Clear();
        }
        else
        {
            _pauseManager.Pause();
            viewStack.Push<PauseView>();
        }
    }

    private void OnResumeRequested()
    {
        var viewStack = Root.Instance.ViewStack;

        _pauseManager.Resume();
        viewStack.Pop();
    }

    private SettingView CreateSettingView(ViewStack viewStack, SettingsManager settingsManager,
        AudioEmitterUI audioEmitter)
    {
        var root = viewStack.GetUIRoot().Q<VisualElement>("SettingView");
        var settingsView = new SettingView(root, settingsManager, audioEmitter);

        return settingsView;
    }
}