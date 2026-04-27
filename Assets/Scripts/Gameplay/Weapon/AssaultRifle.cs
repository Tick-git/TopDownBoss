using UnityEngine;

public class AssaultRifle : MonoBehaviour
{
    [SerializeField] private AssaultRifleData _data;
    [SerializeField] private Transform _weaponCenter;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Magazine _magazine;
    
    private readonly float _aimSmoothnessFactor = 5;
    private float _lastShotTime;
    
    private SpriteRenderer _weaponSpriteRenderer;
    private Vector2 _currentAimDirection;
    private Vector2 CenterPosition => _weaponCenter.position;
    
    private void Awake()
    {
        _weaponSpriteRenderer = GetComponent<SpriteRenderer>();
        _lastShotTime = -float.MaxValue;
        _currentAimDirection = Vector2.right;
        SetWeaponTransform();
    }

    public void ApplyAim(Vector2 direction)
    {
        if (direction.magnitude <= 0.001f)
            return;
        
        _currentAimDirection = CalculateAimDirection(direction);
        
        bool aimingLeft = _currentAimDirection.x < 0;
        _weaponSpriteRenderer.flipY = _currentAimDirection.x < 0;
        
        SetWeaponTransform();
    }

    private Vector2 CalculateAimDirection(Vector2 direction)
    {
        direction.Normalize();
        
        float dotProduct = Vector2.Dot(_currentAimDirection, direction);
        bool smallAimDirectionChange = dotProduct > 0;
        
        if (smallAimDirectionChange)
            direction = Vector2.Lerp(_currentAimDirection, direction, _aimSmoothnessFactor / dotProduct * Time.deltaTime);
        
        return direction;
    }

    private void SetWeaponTransform()
    {
        float rotationAngle = Mathf.Atan2(_currentAimDirection.y, _currentAimDirection.x) * Mathf.Rad2Deg;
        
        transform.position = CenterPosition + _currentAimDirection * _data.OrbitRadius;
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