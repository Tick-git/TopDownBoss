using System;
using UnityEngine;

public class ViewFocusController : IDisposable
{
    private readonly ViewInteractionController _viewInteractionController;
    private readonly ViewStack _viewStack;

    public ViewFocusController(ViewStack viewStack, ViewInteractionController viewInteractionController)
    {
        _viewStack = viewStack;
        _viewInteractionController = viewInteractionController;

        _viewStack.ActiveViewChanged += OnActiveViewChanged;
        _viewInteractionController.InteractionModeChanged += OnInteractionModeChanged;
    }

    public void Dispose()
    {
        _viewStack.ActiveViewChanged -= OnActiveViewChanged;
        _viewInteractionController.InteractionModeChanged -= OnInteractionModeChanged;
    }

    private void OnInteractionModeChanged(InteractionModeUI mode)
    {
        if (mode != InteractionModeUI.Focus) return;

        if (_viewStack.ActiveView is IFocusableView focusableView)
        {
            focusableView.Focus();
        }
    }

    private void OnActiveViewChanged(ActiveViewChangedArgs activeViewChangedArgs)
    {
        if (_viewInteractionController.InteractionMode != InteractionModeUI.Focus) return;

        if (activeViewChangedArgs.CurrentActiveView is IFocusableView currentView)
        {
            currentView.Focus();
        }
    }
}