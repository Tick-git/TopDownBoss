using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class SettingView : View, IInteractableView, IFocusableView
{
    public event Action LeaveSettingRequested;

    private readonly ProjectButton _backButton;

    private IFocusableView _lastFocusedView;
    
    private readonly SettingsManager _settingsManager;

    private readonly List<SliderSettingView> _sliderSettingViews = new();
    
    public SettingView(VisualElement root, SettingsManager settingsManager, AudioEmitterUI audioEmitter) : base(root)
    {
        var masterVolumeSlider = new SliderSettingView(root.Q<SliderInt>("MasterSlider"), settingsManager.MasterVolume);
        var musicVolumeSlider = new SliderSettingView(root.Q<SliderInt>("MusicSlider"), settingsManager.MusicVolume);
        var sfxVolumeSlider = new SliderSettingView(root.Q<SliderInt>("SFXSlider"), settingsManager.SfxVolume);
        var uiVolumeSlider = new SliderSettingView(root.Q<SliderInt>("UISlider"), settingsManager.UIVolume);

        _backButton = new(root.Q<Button>("BackButton"), audioEmitter);
        _backButton.Clicked += OnBackRequested;
        _backButton.FocusChanged += OnFocusChanged;

        _sliderSettingViews.Add(masterVolumeSlider);
        _sliderSettingViews.Add(musicVolumeSlider);
        _sliderSettingViews.Add(sfxVolumeSlider);
        _sliderSettingViews.Add(uiVolumeSlider);

        foreach (var sliderSettingView in _sliderSettingViews)
        {
            sliderSettingView.OnFocusChanged += OnFocusChanged;
        }
        
        _settingsManager =  settingsManager;
    }
    
    public override void Dispose()
    {
        base.Dispose();
        
        _backButton.Clicked -= OnBackRequested;
        _backButton.FocusChanged -= OnFocusChanged;
        _backButton.Dispose();

        foreach (var sliderSettingView in _sliderSettingViews)
        {
            sliderSettingView.OnFocusChanged -= OnFocusChanged;
            sliderSettingView.Dispose();
        }
    }
    
    private void OnFocusChanged(IFocusableView view)
    {
        _lastFocusedView = view;
    }

    private void OnBackRequested()
    {
        _settingsManager.SavePlayerPrefs();
        LeaveSettingRequested?.Invoke();
    }

    public void SetInteractMode(InteractionModeUI interactionMode)
    {
        _backButton.SetInteractMode(interactionMode);
    }

    public override bool TryHandleCancel()
    {
        LeaveSettingRequested?.Invoke();
        return true;
    }

    public void Focus()
    {
        if (_lastFocusedView != null)
        {
            _lastFocusedView.Focus();
        }
        else
        {
            _backButton.Focus();
        }
    }
    
    private class SliderSettingView : IDisposable, IFocusableView
    {
        private readonly SliderInt _slider;
        private readonly Setting<float> _setting;

        public event Action<SliderSettingView> OnFocusChanged;

        public SliderSettingView(SliderInt slider, Setting<float> setting)
        {
            _slider = slider;
            _setting = setting;

            slider.value = CalculateVolumePercentage(setting.Value);

            slider.RegisterValueChangedCallback(OnChanged);
            slider.RegisterCallback<FocusEvent>(OnFocus);
            setting.ValueChanged += UpdateView;
        }

        private void OnFocus(FocusEvent evt)
        {
            OnFocusChanged?.Invoke(this);
        }

        public void Dispose()
        {
            _slider.UnregisterValueChangedCallback(OnChanged);
            _setting.ValueChanged -= UpdateView;
        }

        private void OnChanged(ChangeEvent<int> evt)
        {
            _setting.Value = evt.newValue / 100.0f;
        }

        private void UpdateView(float value)
        {
            _slider.SetValueWithoutNotify(CalculateVolumePercentage(value));
        }

        private int CalculateVolumePercentage(float value)
        {
            return (int)(value * 100);
        }

        public void Focus()
        {
            _slider.Focus();
        }
    }
}