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
    }

    public void ApplyAim(Vector2 target)
    {
        Vector2 aimDirection = (target - CenterPosition).normalized;
        
        bool aimingLeft = aimDirection.x < 0;
        
        transform.position = CenterPosition + aimDirection * _data.OrbitRadius;
        transform.right = aimDirection;
        
        _weaponSpriteRenderer.flipY = aimingLeft;
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