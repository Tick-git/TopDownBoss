using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.XInput;

public class InputManager : MonoBehaviour
{
    public event Action<InputDeviceType> DeviceChanged;
    public event Action CancelPerformed;
    public event Action ContinueCanceled;

    private InputDevice _currentDevice;
    public InputDeviceType CurrentDeviceType { get; private set; }

    private IDisposable _onAnyButtonSubscription;

    private InputAction _cancelAction;

    private void OnEnable()
    {
        _cancelAction = InputSystem.actions.FindAction(InputActionName.UI.Cancel);
        _cancelAction.Enable();
        _cancelAction.performed += OnCancelPerformed;
        InputSystem.actions.FindAction("Continue").canceled += OnContinueCanceled;

        _onAnyButtonSubscription = InputSystem.onAnyButtonPress.Call(OnAnyInput);
    }

    private void OnContinueCanceled(InputAction.CallbackContext obj)
    {
        ContinueCanceled?.Invoke();
    }

    private void OnDisable()
    {
        _cancelAction.Disable();
        _cancelAction.performed -= OnCancelPerformed;
        _onAnyButtonSubscription.Dispose();
    }

    private void OnCancelPerformed(InputAction.CallbackContext obj)
    {
        CancelPerformed?.Invoke();
    }

    private void Update()
    {
        DetectMouseMovement();
        DetectGamepadMovement();
    }

    private void OnAnyInput(InputControl control)
    {
        InvokeDeviceChanged(control.device);
    }

    private void InvokeDeviceChanged(InputDevice device)
    {
        if (_currentDevice == device) return;

        _currentDevice = device;
        CurrentDeviceType = GetDeviceType(device);

        DeviceChanged?.Invoke(CurrentDeviceType);
    }

    private void DetectMouseMovement()
    {
        var mouse = Mouse.current;
        if (mouse == null) return;

        if (mouse.delta.ReadValue().sqrMagnitude > 0.01f)
        {
            if (_currentDevice is Mouse) return;

            InvokeDeviceChanged(mouse);
        }
    }

    private void DetectGamepadMovement()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null) return;

        if (gamepad.leftStick.ReadValue().sqrMagnitude > 0.01f || gamepad.rightStick.ReadValue().sqrMagnitude > 0.01f)
        {
            if (_currentDevice == gamepad) return;

            InvokeDeviceChanged(gamepad);
        }
    }

    private InputDeviceType GetDeviceType(InputDevice device)
    {
        if (device is Mouse)
            return InputDeviceType.Mouse;
        if (device is Keyboard)
            return InputDeviceType.Keyboard;
        if (device is XInputController)
            return InputDeviceType.XInput;
        if (device is DualShockGamepad)
            return InputDeviceType.DualShock;

        return InputDeviceType.None;
    }
}

public enum InputDeviceType
{
    Keyboard,
    Mouse,
    DualShock,
    XInput,
    None
}