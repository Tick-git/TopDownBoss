using System;
using UnityEngine.UIElements;

public class ProjectButton : IInteractableView, IFocusableView, IDisposable
{
    private readonly Button _button;
    private readonly AudioEmitterUI _audioEmitter;

    public event Action Clicked;
    public event Action<IFocusableView> FocusChanged;

    private const string FocusMode = "button-focus-mode";
    private const string HoverMode = "button-hover-mode";

    public ProjectButton(Button button, AudioEmitterUI audioEmitter)
    {
        _button = button;
        _audioEmitter = audioEmitter;

        _button.clicked += OnClicked;
    }

    public void Dispose()
    {
        UnregisterHighlightEvents();
        _button.clicked -= OnClicked;
    }

    public void SetInteractMode(InteractionModeUI interactionMode)
    {
        switch (interactionMode)
        {
            case InteractionModeUI.Focus:
                SetInteractable(true);
                SetMode(FocusMode, HoverMode, true);
                break;
            case InteractionModeUI.Hover:
                SetInteractable(true);
                SetMode(HoverMode, FocusMode, false);
                break;
            case InteractionModeUI.Locked:
                SetInteractable(false);
                break;
        }
    }

    private void SetMode(string modeToAdd, string modeToRemove, bool focusable)
    {
        _button.RemoveFromClassList(modeToRemove);
        _button.AddToClassList(modeToAdd);
        _button.focusable = focusable;

        UnregisterHighlightEvents();

        if (focusable)
        {
            _button.RegisterCallback<FocusEvent>(OnFocus);
        }
        else
        {
            _button.RegisterCallback<MouseOverEvent>(OnMouseOver);
            _button.Blur();
        }
    }

    public void Show()
    {
        _button.style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        _button.style.display = DisplayStyle.None;
    }

    private void SetInteractable(bool interactable)
    {
        _button.SetEnabled(interactable);
    }

    private void UnregisterHighlightEvents()
    {
        _button.UnregisterCallback<MouseOverEvent>(OnMouseOver);
        _button.UnregisterCallback<FocusEvent>(OnFocus);
    }

    private void OnFocus(FocusEvent evt)
    {
        _audioEmitter?.PlayHoverSound();
        FocusChanged?.Invoke(this);
    }

    private void OnMouseOver(MouseOverEvent evt)
    {
        _audioEmitter?.PlayHoverSound();
    }

    private void OnClicked()
    {
        _audioEmitter?.PlayClickedSound();
        Clicked?.Invoke();
    }

    public void Focus()
    {
        _button.Focus();
    }
}