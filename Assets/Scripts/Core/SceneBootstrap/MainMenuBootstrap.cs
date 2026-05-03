using UnityEngine;

public class MainMenuBootstrap : MonoBehaviour
{
    private MainMenuFeature _mainMenuFeature;

    private void Start()
    {
        var viewStack = Root.Instance.ViewStack;
        var settingsManager = Root.Instance.SettingsManager;
        var gameFlowService = Root.Instance.GameFlowService;
        var inputManager = Root.Instance.InputManager;
        var audioEmitterUI = Root.Instance.AudioEmitterUI;

        _mainMenuFeature =
            new MainMenuFeature(viewStack, gameFlowService, settingsManager, inputManager, audioEmitterUI);
        _mainMenuFeature.Initialize();

        viewStack.Push<MainMenuView>();
    }

    private void OnDestroy()
    {
        _mainMenuFeature.Dispose();
    }
}