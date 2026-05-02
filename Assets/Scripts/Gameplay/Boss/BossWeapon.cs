using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    [SerializeField] private Transform _weapon;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Magazine _magazine;
    [SerializeField] private Transform _firePoint;

    private Vector3 _smoothedRotation;
    private float _bulletSpeed = 20;
    
    public void ApplyAim(Vector2 target)
    {
        var direction = (target - (Vector2)_weapon.position).normalized;

        if (direction.magnitude == 0f)
            return;

        direction.Normalize();

        bool aimingLeft = target.x < transform.position.x;
        float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (aimingLeft)
            rotationAngle -= 180;

        _weapon.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
    }

    public void Shoot(Vector2 target)
    {
        var direction = (target - (Vector2)_firePoint.position).normalized;

        _magazine.TryGetBullet(out var bullet1);
        _magazine.TryGetBullet(out var bullet2);
        _magazine.TryGetBullet(out var bullet3);

        bullet1.transform.position = _firePoint.position;
        bullet2.transform.position = _firePoint.position;
        bullet3.transform.position = _firePoint.position;

        bullet1.StartFlight(Quaternion.Euler(0, 0, 5f) * direction, 10, _bulletSpeed);
        bullet2.StartFlight(direction, 10, _bulletSpeed);
        bullet3.StartFlight(Quaternion.Euler(0, 0, -5f) * direction, 10, _bulletSpeed);
    }
}