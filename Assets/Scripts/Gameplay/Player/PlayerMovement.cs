using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementData _playerMovementData;
    
    public void Move(Vector2 direction)
    {
        direction.Normalize();
        
        transform.position += (Vector3) direction * (Time.deltaTime * _playerMovementData.MoveSpeed);
    }

    public void Roll(Vector2 direction)
    {
        direction.Normalize();
        
        transform.position += (Vector3) direction * (Time.deltaTime * _playerMovementData.RollSpeed);
    }
}