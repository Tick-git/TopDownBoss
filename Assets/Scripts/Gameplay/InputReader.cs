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
    private Vector2 _lastAimDirection;

    private void OnEnable()
    {
        _cam = Camera.main;
        _moveAction = InputSystem.actions.FindAction("Move");
        _attackAction = InputSystem.actions.FindAction("Attack");
        _rollAction = InputSystem.actions.FindAction("Roll");
        _lookAction = InputSystem.actions.FindAction("Look");
        _lastAimDirection = Vector2.right;
    }

    public bool AttackIsPressed => _attackAction.IsPressed();
    public bool RollWasPressed => _rollAction.WasPressedThisFrame();
    public Vector2 MoveDirection =>  _moveAction.ReadValue<Vector2>();
    public Vector2 AimDirection => CalculateNextAimDirection();

    private Vector2 CalculateNextAimDirection()
    {
        var smoothnessFactor = 5;
        
        var direction = _lookAction.ReadValue<Vector2>().normalized;
        float dotProduct = Vector2.Dot(_lastAimDirection, direction);
        bool smallAimDirectionChange = dotProduct > 0;
        
        if (smallAimDirectionChange)
            direction = Vector2.Lerp(_lastAimDirection, direction, (smoothnessFactor / dotProduct) * Time.deltaTime);

        _lastAimDirection = direction;
        
        return direction;
    }
}