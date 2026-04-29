using UnityEngine;

public class WeaponRotator
{
    private readonly SpriteRenderer _spriteRenderer;
    private readonly Transform _transform;

    public WeaponRotator(SpriteRenderer spriteRenderer, Transform transform)
    {
        _spriteRenderer = spriteRenderer;
        _transform = transform;
    }

    public void Rotate(Vector2 direction)
    {
        if (direction.magnitude == 0f)
            return;
        
        direction.Normalize();
        
        bool aimingLeft = direction.x < 0;
        float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        _spriteRenderer.flipY = aimingLeft;
        _transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
    }
}