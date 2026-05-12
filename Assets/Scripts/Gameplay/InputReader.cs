using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject
{
    private InputActions _inputActions;
    private Vector2 _lastAimDirection;

    private void OnEnable()
    {
        _inputActions = new InputActions();
        
        _inputActions.Player.Pause.performed += OnPausePerformed;
        _inputActions.UI.ClosePause.performed += OnPausePerformed;
        _inputActions.UI.Cancel.performed += OnCancelPerformed;
    }

    private void OnDisable()
    {
        _inputActions.Player.Pause.performed -= OnPausePerformed;
        _inputActions.UI.ClosePause.performed -= OnPausePerformed;
        _inputActions.UI.Cancel.performed -= OnCancelPerformed;
    }

    public void EnableGameplayInput()
    {
        _inputActions.Player.Enable();
        _inputActions.UI.Disable();
    }
    
    public void EnableUIInput()
    {
        _inputActions.Player.Disable();
        _inputActions.UI.Enable();
    }

    public bool AttackIsPressed => _inputActions.Player.Attack.IsPressed();
    public bool RollWasPressed => _inputActions.Player.Roll.WasPressedThisFrame();
    public Vector2 MoveDirection => _inputActions.Player.Move.ReadValue<Vector2>();
    public Vector2 AimDirection => CalculateNextAimDirection();
    public event Action PausePerformed;
    public event Action CancelPerformed;

    private Vector2 CalculateNextAimDirection()
    {
        var smoothnessFactor = 5;

        var direction = _inputActions.Player.Look.ReadValue<Vector2>().normalized;
        float dotProduct = Vector2.Dot(_lastAimDirection, direction);
        bool smallAimDirectionChange = dotProduct > 0;

        if (smallAimDirectionChange)
            direction = Vector2.Lerp(_lastAimDirection, direction, (smoothnessFactor / dotProduct) * Time.deltaTime);

        _lastAimDirection = direction;

        return direction;
    }
    
    private void OnCancelPerformed(InputAction.CallbackContext obj) => CancelPerformed?.Invoke();
    private void OnPausePerformed(InputAction.CallbackContext obj) => PausePerformed?.Invoke();
}
