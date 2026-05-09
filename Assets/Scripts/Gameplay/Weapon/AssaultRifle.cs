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
    private IDamageable _owner;
    private Vector2 CenterPosition => _weaponCenter.position;

    private const float DirectionSmoothFactor = 5;

    public void Initialize(IDamageable owner)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _lastShotTime = -float.MaxValue;
        _owner = owner;

        ApplyAim(Vector2.right);
    }

    public void ApplyAim(Vector2 direction)
    {
        if (direction.magnitude <= 0.001f)
            return;

        PointInDirection(SmoothDirection(direction.normalized, Time.deltaTime));
    }

    public void PointInDirection(Vector2 direction)
    {
        if (direction.magnitude <= 0.001f)
            return;

        bool aimingLeft = direction.x < 0;
        float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _spriteRenderer.flipY = aimingLeft;
        transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        transform.position = CenterPosition + direction * _data.OrbitRadius;

        _currentDirection = direction;
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

    public bool TryShoot()
    {
        if (Time.time < _lastShotTime + (1 / _data.FireRatePerSecond))
            return false;

        _magazine.TryGetBullet(out Bullet bullet);
        _lastShotTime = Time.time;

        var bulletParams = new BulletFlightParams(
            _firePoint.position,
            transform.right,
            _data.Damage,
            _data.Speed,
            _data.Range,
            _owner);

        bullet.StartFlight(bulletParams);

        return true;
    }
}