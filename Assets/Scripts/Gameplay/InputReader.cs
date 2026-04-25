using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject
{
    private InputAction _moveAction;

    private void OnEnable()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
    }
    
    public Vector2 MoveInput =>  _moveAction.ReadValue<Vector2>();
}