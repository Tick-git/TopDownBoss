using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _speed = 5;
    private float _rollSpeed = 7.5f;
    
    public void Move(Vector2 direction)
    {
        direction.Normalize();
        
        transform.position += (Vector3) direction * (Time.deltaTime * _speed);
    }

    public void Roll(Vector2 direction)
    {
        direction.Normalize();
        
        transform.position += (Vector3) direction * (Time.deltaTime * _rollSpeed);
    }
}