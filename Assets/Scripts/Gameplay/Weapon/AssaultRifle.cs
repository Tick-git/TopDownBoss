using UnityEngine;

public class AssaultRifle : MonoBehaviour
{
    [SerializeField] private AssaultRifleData _data;
    [SerializeField] private Transform _weaponCenter;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Magazine _magazine;
    
    private float _lastShotTime;
    
    private SpriteRenderer _weaponSpriteRenderer;
    private Vector2 CenterPosition => _weaponCenter.position;
    
    private void Awake()
    {
        _weaponSpriteRenderer = GetComponent<SpriteRenderer>();
        _lastShotTime = -float.MaxValue;
        
        SetWeaponTransform(Vector2.right);
    }

    public void ApplyAim(Vector2 direction)
    {
        if (direction.magnitude <= 0.001f)
            return;
        
        bool aimingLeft = direction.x < 0;
        _weaponSpriteRenderer.flipY = aimingLeft;
        
        SetWeaponTransform(direction);
    }

    private void SetWeaponTransform(Vector2 direction)
    {
        float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        transform.position = CenterPosition + direction * _data.OrbitRadius;
        transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
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