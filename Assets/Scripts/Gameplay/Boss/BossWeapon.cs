using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    [SerializeField] private SpreadShotWeaponData _smallSpreadShotWeaponData;
    [SerializeField] private SpreadShotWeaponData _largeSpreadShotWeaponData;
    [SerializeField] private Transform _weapon;
    [SerializeField] private Magazine _magazine;
    [SerializeField] private Transform _firePoint;

    private IDamageable _owner;

    private float _currentAngle;
    private bool _lastAimedLeft;

    public Vector2 FirePointPosition => _firePoint.position;
    public float SmallSpreadShotBulletSpeed => _smallSpreadShotWeaponData.Speed;

    public void Initialize(IDamageable owner)
    {
        _owner = owner;
    }

    public void ApplyAim(Vector2 target, float deltaTime)
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
            _currentAngle = Mathf.LerpAngle(_currentAngle, rotationAngle, deltaTime * 20);

        _weapon.rotation = Quaternion.Euler(0f, 0f, _currentAngle);
        _lastAimedLeft = aimingLeft;
    }

    public void AimToDefault(Vector2 target, float deltaTime)
    {
        bool aimingLeft = target.x < transform.position.x;

        if (aimingLeft)
        {
            target = _weapon.position + Vector3.left;
        }
        else
        {
            target = _weapon.position + Vector3.right;
        }

        ApplyAim(target, deltaTime);
    }

    public void ShootSmallSpread(Vector2 target)
    {
        SpreadShot(target, _smallSpreadShotWeaponData);
    }

    public void ShootBigSpread(Vector2 target)
    {
        SpreadShot(target, _largeSpreadShotWeaponData);
    }

    private void SpreadShot(Vector2 target, SpreadShotWeaponData weaponData)
    {
        var direction = (target - (Vector2)_firePoint.position).normalized;
        var bulletSpreadSpacing = (weaponData.SpreadAngle * 2) / (weaponData.BulletCount - 1);

        for (int i = 0; i < weaponData.BulletCount; i++)
        {
            var curSpreadAngle = weaponData.SpreadAngle - (bulletSpreadSpacing * i);
            var curDirection = Quaternion.Euler(0, 0, curSpreadAngle) * direction;

            var bulletParams = new BulletFlightParams(
                _firePoint.position,
                curDirection,
                weaponData.Damage,
                weaponData.Speed,
                weaponData.Range,
                _owner);

            var bullet = _magazine.GetBullet();
            bullet.StartFlight(bulletParams);
        }
    }
}