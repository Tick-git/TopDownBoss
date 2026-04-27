using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementData _playerMovementData;
    
    private Rigidbody2D _rigidbody;

    public void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction, float deltaTime)
    {
        direction.Normalize();
        
        _rigidbody.MovePosition(_rigidbody.position + direction * (deltaTime * _playerMovementData.MoveSpeed));
    }

    public void Roll(Vector2 direction, float deltaTime)
    {
        direction.Normalize();
        
        _rigidbody.MovePosition(_rigidbody.position + direction * (deltaTime * _playerMovementData.RollSpeed));
    }
}