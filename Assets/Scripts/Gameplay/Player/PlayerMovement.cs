using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementData _playerMovementData;
    
    private Rigidbody2D _rigidbody;
    
    public float MoveSpeedMultiplier { get; private set; }

    public void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        ResetMoveSpeedMultiplicator();
    }

    public void Move(Vector2 direction, float deltaTime)
    {
        direction.Normalize();
        
        _rigidbody.MovePosition(_rigidbody.position + direction * (deltaTime * _playerMovementData.MoveSpeed * MoveSpeedMultiplier));
    }

    public void Roll(Vector2 direction, float deltaTime)
    {
        direction.Normalize();
        
        _rigidbody.MovePosition(_rigidbody.position + direction * (deltaTime * _playerMovementData.RollSpeed));
    }

    public void SetMoveSpeedMultiplicator(float value)
    {
        MoveSpeedMultiplier = value;
    }

    public void ResetMoveSpeedMultiplicator()
    {
        MoveSpeedMultiplier = 1;
    }
}