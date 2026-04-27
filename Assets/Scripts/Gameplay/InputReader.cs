using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject
{
    private InputAction _moveAction;
    private InputAction _attackAction;
    
    private Camera _cam;
    private InputAction _rollAction;
    private InputAction _lookAction;

    private void OnEnable()
    {
        _cam = Camera.main;
        _moveAction = InputSystem.actions.FindAction("Move");
        _attackAction = InputSystem.actions.FindAction("Attack");
        _rollAction = InputSystem.actions.FindAction("Roll");
        _lookAction = InputSystem.actions.FindAction("Look");
    }

    public bool AttackIsPressed => _attackAction.IsPressed();
    public bool RollWasPressed => _rollAction.WasPressedThisFrame();
    public Vector2 MoveDirection =>  _moveAction.ReadValue<Vector2>();
    public Vector2 AimPosition => _lookAction.ReadValue<Vector2>().normalized;
}