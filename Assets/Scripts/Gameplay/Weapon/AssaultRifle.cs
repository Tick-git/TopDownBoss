using UnityEngine;

public class AssaultRifle : MonoBehaviour
{
    [SerializeField] private AssaultRifleData _data;
    [SerializeField] private Transform _weaponCenter;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Magazine _magazine;

    private float _lastShotTime;
    private Vector2 _currentDirection;
    SpriteRenderer _spriteRenderer;
    private Vector2 CenterPosition => _weaponCenter.position;

    private const float DirectionSmoothFactor = 10;

    public void Initialize()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _lastShotTime = -float.MaxValue;

        ApplyAim(Vector2.right);
    }

    public void ApplyAim(Vector2 direction)
    {
        if (direction.magnitude <= 0.001f)
            return;

        _currentDirection = SmoothDirection(direction.normalized, Time.deltaTime);

        bool aimingLeft = direction.x < 0;
        float rotationAngle = Mathf.Atan2(_currentDirection.y, _currentDirection.x) * Mathf.Rad2Deg;

        _spriteRenderer.flipY = aimingLeft;
        transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        transform.position = CenterPosition + _currentDirection * _data.OrbitRadius;
    }

    private Vector2 SmoothDirection(Vector2 direction, float deltaTime)
    {
        Vector2 result = direction;

        float dot = Vector2.Dot(_currentDirection.normalized, direction);
        bool smallDirectionDifference = dot > 0;

        if (smallDirectionDifference)
        {
            result = Vector2.Lerp(_currentDirection, direction, deltaTime * (DirectionSmoothFactor / dot));
        }

        return result;
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