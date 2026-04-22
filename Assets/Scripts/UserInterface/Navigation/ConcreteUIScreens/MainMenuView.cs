using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuView : View, IInteractableView, IFocusableView, IAnimatableView
{
    public event Action StartGameRequested;
    public event Action SettingViewRequested;
    public event Action ExitGameRequested;

    private readonly ProjectButton _playButton;
    private readonly ProjectButton _settingButton;
    private readonly ProjectButton _exitButton;
    
    private IFocusableView _lastFocusedElement;

    private readonly Label _pressAnyButtonLabel;

    private IVisualElementScheduledItem _pulsatingPressAnyButtonLabel;
    private readonly VisualElement _buttonContainer;
    
    private bool _onAnyButtonLabelPulsing;
    private float _pulseTime;

    private static bool _onKeyPressedLabelShown = false;
    public MainMenuView(VisualElement root, AudioEmitterUI audioEmitter) : base(root)
    {
        _playButton = new(root.Q<Button>("PlayButton"), audioEmitter);
        _settingButton = new(root.Q<Button>("SettingButton"), audioEmitter);
        _exitButton = new(root.Q<Button>("ExitButton"), audioEmitter);
        _buttonContainer = root.Q<VisualElement>("ButtonContainer");
        _pressAnyButtonLabel = root.Q<Label>("PressAnyButtonLabel");

        _playButton.Clicked += OnPlayButtonClicked;
        _settingButton.Clicked += OnSettingButtonClicked;
        _exitButton.Clicked += OnExitButtonClicked;

        _playButton.FocusChanged += OnButtonFocusChanged;
        _settingButton.FocusChanged += OnButtonFocusChanged;
        _exitButton.FocusChanged += OnButtonFocusChanged;
    }

    public override void Dispose()
    {
        base.Dispose();
        
        _playButton.Clicked -= OnPlayButtonClicked;
        _settingButton.Clicked -= OnSettingButtonClicked;
        _exitButton.Clicked -= OnExitButtonClicked;
        
        _playButton.FocusChanged -= OnButtonFocusChanged;
        _settingButton.FocusChanged -= OnButtonFocusChanged;
        _exitButton.FocusChanged -= OnButtonFocusChanged;
        
        _playButton.Dispose();
        _settingButton.Dispose();
        _exitButton.Dispose();
    }

    public override void Show()
    {
        base.Show();
        
        if (_onKeyPressedLabelShown)
            ShowButtons();
        else
        {
            _onKeyPressedLabelShown = true;
            HideButtons();
        }

        _onAnyButtonLabelPulsing = true;
    }

    public void ShowButtons()
    {
        _pulsatingPressAnyButtonLabel?.Pause();
        _pressAnyButtonLabel.style.display = DisplayStyle.None;

        _playButton.Show();
        _settingButton.Show();
        _exitButton.Show();
        
        _buttonContainer.AddToClassList("fade-in");
        _buttonContainer.RemoveFromClassList("transparent");
        
        Focus();
    }
    
    private void HideButtons()
    {
        _playButton.Hide();
        _settingButton.Hide();
        _exitButton.Hide();
        _pressAnyButtonLabel.style.display = DisplayStyle.Flex;

        _buttonContainer.AddToClassList("transparent");
    }
    
    private void OnButtonFocusChanged(IFocusableView button)
    {
        _lastFocusedElement = button;
    }

    private void OnExitButtonClicked() => ExitGameRequested?.Invoke();

    private void OnSettingButtonClicked() => SettingViewRequested?.Invoke();

    private void OnPlayButtonClicked() => StartGameRequested?.Invoke();


    public override bool TryHandleCancel()
    {
        return true;
    }

    public void SetInteractMode(InteractionModeUI interactionMode)
    {
        _playButton.SetInteractMode(interactionMode);
        _settingButton.SetInteractMode(interactionMode);
        _exitButton.SetInteractMode(interactionMode);
    }
    
    public void Update()
    {
        if (!_onAnyButtonLabelPulsing) return;
        
        _pulseTime += Time.deltaTime * 3;

        float t = (Mathf.Sin(_pulseTime) + 1f) * 0.5f;
        t = Mathf.Pow(t, 0.9f);

        float opacity = Mathf.Lerp(0.05f, 1f, t);

        _pressAnyButtonLabel.style.opacity = opacity;
    }

    public void Focus()
    {
        if (_lastFocusedElement != null)
        {
            _lastFocusedElement.Focus();
        }
        else
        {
            _playButton.Focus();
        }
    }
}