using UnityEngine.InputSystem;
using System;

public class UIInputReader : IDisposable
{
    private readonly ViewStack _viewStack;

    private readonly InputAction _cancelAction;
    private readonly InputAction _pauseAction;

    public event Action CancelPerformed;
    public event Action PausePerformed;

    public UIInputReader(ViewStack viewStack)
    {
        _viewStack = viewStack;
        _cancelAction = InputSystem.actions.FindAction("Cancel");
        _pauseAction = InputSystem.actions.FindAction("Pause");

        _cancelAction.performed += OnCancelPerformed;
        _pauseAction.performed += OnPausePerformed;
    }

    private void OnPausePerformed(InputAction.CallbackContext _)
    {
        PausePerformed?.Invoke();
    }

    public void Dispose()
    {
        _cancelAction.performed -= OnCancelPerformed;
        _pauseAction.performed -= OnPausePerformed;
        
        _cancelAction?.Dispose();
        _pauseAction?.Dispose();
    }

    private void OnCancelPerformed(InputAction.CallbackContext ctx)
    {
        bool isKeyboard = ctx.control.device is Keyboard;

        if (_viewStack.HasActiveView)
        {
            CancelPerformed?.Invoke();
        }
        else if (isKeyboard)
        {
            PausePerformed?.Invoke();
        }
    }
}