using System;
using UnityEngine.UIElements;

public class PauseView : View, IInteractableView, IFocusableView
{
    public event Action ResumeRequested;
    public event Action SettingsRequested;
    public event Action MainMenuRequested;
    public event Action ExitRequested;

    private readonly ProjectButton _resumeButton;
    private readonly ProjectButton _settingsButton;
    private readonly ProjectButton _mainMenuButton;
    private readonly ProjectButton _exitButton;
    private IFocusableView _lastFocusedView;

    public PauseView(VisualElement root, AudioEmitterUI audioEmitterUI) : base(root)
    {
        _resumeButton = new(root.Q<Button>("ResumeButton"), audioEmitterUI);
        _settingsButton = new(root.Q<Button>("SettingButton"), audioEmitterUI);
        _mainMenuButton = new(root.Q<Button>("MainMenuButton"), audioEmitterUI);
        _exitButton = new(root.Q<Button>("ExitButton"), audioEmitterUI);

        _resumeButton.Clicked += OnResumeRequested;
        _settingsButton.Clicked += OnSettingsRequested;
        _mainMenuButton.Clicked += OnMainMenuRequested;
        _exitButton.Clicked += OnExitRequested;

        _resumeButton.FocusChanged += OnFocusChanged;
        _settingsButton.FocusChanged += OnFocusChanged;
        _mainMenuButton.FocusChanged += OnFocusChanged;
        _exitButton.FocusChanged += OnFocusChanged;
    }

    private void OnFocusChanged(IFocusableView view)
    {
        _lastFocusedView =  view;
    }

    public override bool TryHandleCancel()
    {
        ResumeRequested?.Invoke();
        return true;
    }

    public override void Dispose()
    {
        base.Dispose();
        
        _resumeButton.Clicked -= OnResumeRequested;
        _settingsButton.Clicked -= OnSettingsRequested;
        _mainMenuButton.Clicked -= OnMainMenuRequested;
        _exitButton.Clicked -= OnExitRequested;
        
        _resumeButton.FocusChanged -= OnFocusChanged;
        _settingsButton.FocusChanged -= OnFocusChanged;
        _mainMenuButton.FocusChanged -= OnFocusChanged;
        _exitButton.FocusChanged -= OnFocusChanged;
        
        _resumeButton.Dispose();
        _settingsButton.Dispose();
        _mainMenuButton.Dispose();
        _exitButton.Dispose();
    }

    private void OnExitRequested()
    {
        ExitRequested?.Invoke();
    }

    private void OnMainMenuRequested()
    {
        MainMenuRequested?.Invoke();
    }

    private void OnSettingsRequested()
    {
        SettingsRequested?.Invoke();
    }

    private void OnResumeRequested()
    {
        ResumeRequested?.Invoke();
    }

    public void SetInteractMode(InteractionModeUI interactionMode)
    {
        _resumeButton.SetInteractMode(interactionMode);
        _settingsButton.SetInteractMode(interactionMode);
        _mainMenuButton.SetInteractMode(interactionMode);
        _exitButton.SetInteractMode(interactionMode);
    }

    public void Focus()
    {
        if (_lastFocusedView != null)
            _lastFocusedView.Focus();
        else
            _resumeButton.Focus();
    }
}