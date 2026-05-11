using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayBootstrap : MonoBehaviour
{
    private PauseView _pauseView;
    private PauseManager _pauseManager;
    private SettingView _settingsView;

    void Start()
    {
        if (!Root.Instance.Initialized) return;
        
        var viewStack = Root.Instance.ViewStack;
        var audioEmitterUI = Root.Instance.AudioEmitterUI;
        var gameFlow = Root.Instance.GameFlowService;
        var settingsManager = Root.Instance.SettingsManager;
        var uiInputReader = Root.Instance.UIInputReader;
        _pauseManager = new PauseManager();

        _settingsView = CreateSettingView(viewStack, settingsManager, audioEmitterUI);
        _pauseView = new PauseView(viewStack.GetUIRoot().Q<VisualElement>("PauseView"), audioEmitterUI);

        _settingsView.LeaveSettingRequested += viewStack.Pop;

        _pauseView.ResumeRequested += OnResumeRequested;
        _pauseView.SettingsRequested += viewStack.Push<SettingView>;
        _pauseView.MainMenuRequested += gameFlow.LoadMainMenu;
        _pauseView.ExitRequested += Application.Quit;

        uiInputReader.PausePerformed += OnPausePerformed;

        viewStack.Register(_settingsView);
        viewStack.Register(_pauseView);
    }

    private void OnDestroy()
    {
        var viewStack = Root.Instance.ViewStack;
        var gameFlow = Root.Instance.GameFlowService;
        var uiInputReader = Root.Instance.UIInputReader;

        _settingsView.LeaveSettingRequested -= OnResumeRequested;

        _pauseView.ResumeRequested -= viewStack.Pop;
        _pauseView.SettingsRequested -= viewStack.Push<SettingView>;
        _pauseView.MainMenuRequested -= gameFlow.LoadMainMenu;
        _pauseView.ExitRequested -= Application.Quit;

        uiInputReader.PausePerformed -= OnPausePerformed;

        _pauseView.Dispose();
        _settingsView.Dispose();

        viewStack.Unregister(_settingsView);
        viewStack.Unregister(_pauseView);
    }

    private void OnPausePerformed()
    {
        var viewStack = Root.Instance.ViewStack;

        _pauseManager.Pause();
        viewStack.Push<PauseView>();
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