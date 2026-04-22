using System;
using UnityEngine;

public class MouseVisibilityController : IDisposable
{
    private bool _usingMouse;
    private bool _uiVisible;
    
    private readonly ViewStack _viewStack;
    private readonly InputManager _inputManager;

    public MouseVisibilityController(ViewStack viewStack, InputManager inputManager)
    {
        _viewStack = viewStack;
        _inputManager = inputManager;

        _inputManager.DeviceChanged += OnDeviceChanged;
        _viewStack.ActiveViewChanged += OnViewStackChanged;
    }
    
    public void Dispose()
    {
        _inputManager.DeviceChanged -= OnDeviceChanged;
        _viewStack.ActiveViewChanged -= OnViewStackChanged;
    }

    private void OnViewStackChanged(ActiveViewChangedArgs activeViewChangedArgs)
    {
        _uiVisible = activeViewChangedArgs.CurrentActiveView != null;
        
        SetCursorVisibility();
    }
    
    private void OnDeviceChanged(InputDeviceType deviceType)
    {
        _usingMouse = deviceType == InputDeviceType.Mouse;
        
        SetCursorVisibility();
    }
    
    private void SetCursorVisibility()
    {
        bool visible = _usingMouse && _uiVisible;
        
        if (visible == Cursor.visible) return;

        Cursor.visible = visible;
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
    }
}