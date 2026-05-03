using System;
using UnityEngine.UIElements;

public class MainMenuFeature : IDisposable
{
    private readonly ViewStack _viewStack;
    private readonly SettingsManager _settingsManager;
    private readonly GameFlowService _gameFlowService;
    private readonly InputManager _inputManager;
    private readonly AudioEmitterUI _audioEmitter;

    private MainMenuView _mainMenuView;
    private SettingView _settingsView;

    public MainMenuFeature(ViewStack viewStack, GameFlowService gameFlowService, SettingsManager settingsManager,
        InputManager inputManager, AudioEmitterUI audioEmitter)
    {
        _viewStack = viewStack;
        _gameFlowService = gameFlowService;
        _settingsManager = settingsManager;
        _inputManager = inputManager;
        _audioEmitter = audioEmitter;
    }

    public void Initialize()
    {
        _mainMenuView = CreateMainMenuView(_viewStack, _audioEmitter);
        SetMainMenuViewSubscriptions(true);

        _settingsView = CreateSettingView(_viewStack, _settingsManager, _audioEmitter);
        SetSettingsViewSubscriptions(true);

        _viewStack.Register(_mainMenuView);
        _viewStack.Register(_settingsView);
    }

    public void Dispose()
    {
        SetMainMenuViewSubscriptions(false);
        SetSettingsViewSubscriptions(false);

        _mainMenuView?.Dispose();
        _settingsView?.Dispose();

        _viewStack.Unregister(_mainMenuView);
        _viewStack.Unregister(_settingsView);
    }

    private MainMenuView CreateMainMenuView(ViewStack viewStack, AudioEmitterUI audioEmitter)
    {
        var root = viewStack.GetUIRoot().Q<VisualElement>("MainView");
        var mainMenuView = new MainMenuView(root, audioEmitter);

        return mainMenuView;
    }

    private SettingView CreateSettingView(ViewStack viewStack, SettingsManager settingsManager,
        AudioEmitterUI audioEmitter)
    {
        var root = viewStack.GetUIRoot().Q<VisualElement>("SettingView");
        var settingsView = new SettingView(root, settingsManager, audioEmitter);

        return settingsView;
    }

    private void SetMainMenuViewSubscriptions(bool enable)
    {
        if (enable)
        {
            _mainMenuView.ExitGameRequested += _gameFlowService.ExitGame;
            _mainMenuView.SettingViewRequested += _viewStack.Push<SettingView>;
            _mainMenuView.StartGameRequested += _gameFlowService.LoadGameplay;
            _inputManager.ContinueCanceled += OnAnyKeyPressed;
        }
        else
        {
            _mainMenuView.ExitGameRequested -= _gameFlowService.ExitGame;
            _mainMenuView.SettingViewRequested -= _viewStack.Push<SettingView>;
            _mainMenuView.StartGameRequested -= _gameFlowService.LoadGameplay;
            _inputManager.ContinueCanceled -= OnAnyKeyPressed;
        }
    }

    private void OnAnyKeyPressed()
    {
        _inputManager.ContinueCanceled -= OnAnyKeyPressed;
        _mainMenuView.ShowButtons();
    }

    private void SetSettingsViewSubscriptions(bool enable)
    {
        if (enable)
        {
            _settingsView.LeaveSettingRequested += _viewStack.Pop;
        }
        else
        {
            _settingsView.LeaveSettingRequested -= _viewStack.Pop;
        }
    }
}