using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    [SerializeField] private BossSpreadShotData _smallSpreadShotData;
    [SerializeField] private BossSpreadShotData _largeSpreadShotData;
    [SerializeField] private Transform _weapon;
    [SerializeField] private Magazine _magazine;
    [SerializeField] private Transform _firePoint;

    private IDamageable _owner;
    
    private float _currentAngle;
    private bool _lastAimedLeft;

    public void Initialize(IDamageable owner)
    {
        _owner = owner;
    }

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
        
        if (_lastAimedLeft != aimingLeft)
            _currentAngle = rotationAngle;
        else
            _currentAngle = Mathf.LerpAngle(_currentAngle, rotationAngle, Time.deltaTime * 20);
        
        _weapon.rotation = Quaternion.Euler(0f, 0f, _currentAngle);
        _lastAimedLeft = aimingLeft;
    }

    public void AimToDefault(Vector2 target)
    {
        bool aimingLeft = target.x < transform.position.x;

        if (aimingLeft)
        {
            target = _weapon.position + Vector3.left;
        }
        else
        {
            target =  _weapon.position + Vector3.right;
        }
        
        ApplyAim(target);
    }

    public void ShootSmallSpread(Vector2 target, Vector2 targetVelocity)
    {
        var bulletData = _smallSpreadShotData;

        var shootDistance = (target - (Vector2)_firePoint.position).magnitude;

        var shootTarget = target + targetVelocity * (shootDistance / bulletData.Speed);

        SpreadShot(shootTarget, _smallSpreadShotData);
    }

    public void ShootBigSpread(Vector2 target)
    {
        SpreadShot(target, _largeSpreadShotData);
    }

    private void SpreadShot(Vector2 target, BossSpreadShotData data)
    {
        var direction = (target - (Vector2)_firePoint.position).normalized;
        var bulletSpreadSpacing = (data.SpreadAngle * 2) / (data.BulletCount - 1);

        for (int i = 0; i < data.BulletCount; i++)
        {
            var curSpreadAngle = data.SpreadAngle - (bulletSpreadSpacing * i);
            var curDirection = Quaternion.Euler(0, 0, curSpreadAngle) * direction;

            var bulletParams = new BulletFlightParams(
                _firePoint.position,
                curDirection,
                data.Damage,
                data.Speed,
                data.Range,
                _owner);

            var bullet = _magazine.GetBullet();
            bullet.StartFlight(bulletParams);
        }
    }
}