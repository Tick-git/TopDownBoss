using UnityEngine;

public class AssaultRifle : MonoBehaviour
{
    [SerializeField] private AssaultRifleData _data;
    [SerializeField] private Transform _weaponCenter;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Magazine _magazine;
    
    private float _lastShotTime;
    private WeaponRotator _weaponRotator;
    private Vector2 CenterPosition => _weaponCenter.position;
    
    public void Initialize()
    {
        _weaponRotator = new WeaponRotator(GetComponent<SpriteRenderer>(), transform);
        _lastShotTime = -float.MaxValue;
        
        ApplyAim(Vector2.right);
    }

    public void ApplyAim(Vector2 direction)
    {
        if (direction.magnitude <= 0.001f)
            return;

        _weaponRotator.Rotate(direction);
        transform.position = CenterPosition + direction * _data.OrbitRadius;
    }

    public void TryShoot()
    {
        if (Time.time < _lastShotTime + (1 / _data.FireRatePerSecond))
            return;
        
        _magazine.TryGetBullet(out Bullet bullet);
        _lastShotTime = Time.time;
        
        bullet.transform.position = _firePoint.position;
        bullet.StartFlight(transform.right, _data.Damage, _data.BulletVelocity);
    }
}