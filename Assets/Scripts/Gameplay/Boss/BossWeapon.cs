using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    [SerializeField] private Transform _weapon;
    [SerializeField] private SpriteRenderer _renderer;
    
    public void ApplyAim(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2) transform.position).normalized;
        
        _renderer.flipY = targetPosition.x < transform.position.x;
        
        float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _weapon.rotation = Quaternion.Euler(0, 0, rotationAngle);
    }
}