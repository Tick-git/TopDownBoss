using System;
using UnityEngine;

public class ViewInteractionController : IDisposable
{
    private readonly ViewStack _viewStack;
    private readonly InputManager _inputManager;
    private InteractionModeUI _interactionMode;

    public InteractionModeUI InteractionMode => _interactionMode;

    public event Action<InteractionModeUI> InteractionModeChanged;

    public ViewInteractionController(ViewStack viewStack, InputManager inputManager)
    {
        _viewStack = viewStack;
        _inputManager = inputManager;
        _interactionMode = InteractionModeUI.None;

        _viewStack.ActiveViewChanged += OnActiveViewChanged;
        _inputManager.DeviceChanged += OnDeviceChanged;
    }

    public void Dispose()
    {
        _viewStack.ActiveViewChanged -= OnActiveViewChanged;
        _inputManager.DeviceChanged -= OnDeviceChanged;
    }

    private void OnDeviceChanged(InputDeviceType inputDeviceType)
    {
        _interactionMode = inputDeviceType == InputDeviceType.Mouse ? InteractionModeUI.Hover : InteractionModeUI.Focus;

        if (_viewStack.ActiveView is IInteractableView interactableView)
        {
            interactableView.SetInteractMode(InteractionMode);
        }

        InteractionModeChanged?.Invoke(InteractionMode);
    }

    private void OnActiveViewChanged(ActiveViewChangedArgs viewChangedArgs)
    {
        if (viewChangedArgs.PreviousActiveView is IInteractableView previousActiveView)
        {
            previousActiveView.SetInteractMode(InteractionModeUI.Locked);
        }

        if (viewChangedArgs.CurrentActiveView is IInteractableView currentActiveView)
        {
            currentActiveView.SetInteractMode(InteractionMode);
        }
    }


    public void LockInteraction()
    {
        if (_viewStack.ActiveView is IInteractableView interactableView)
        {
            interactableView.SetInteractMode(InteractionModeUI.Locked);
        }
    }

    public void UnlockInteraction()
    {
        if (_viewStack.ActiveView is IInteractableView interactableView)
        {
            interactableView.SetInteractMode(InteractionMode);
        }
    }
}