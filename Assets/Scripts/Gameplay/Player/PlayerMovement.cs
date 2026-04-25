using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _speed = 5;
    
    public void Move(Vector2 direction)
    {
        direction.Normalize();
        
        transform.position += (Vector3) direction * (Time.deltaTime * _speed);
    }
}