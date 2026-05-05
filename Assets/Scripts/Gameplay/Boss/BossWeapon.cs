using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    [SerializeField] private BossSpreadShotData _smallSpreadShotData;
    [SerializeField] private BossSpreadShotData _largeSpreadShotData;
    [SerializeField] private Transform _weapon;
    [SerializeField] private Magazine _magazine;
    [SerializeField] private Transform _firePoint;

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

    public void ShootSmallSpread(Vector2 target)
    {
        SpreadShot(target, _smallSpreadShotData);
    }

    public void ShootBigSpread(Vector2 target)
    {
        SpreadShot(target, _largeSpreadShotData);
    }

    private void SpreadShot(Vector2 target, BossSpreadShotData data)
    {
        var direction = (target - (Vector2)_firePoint.position).normalized;

        for (int i = 0; i < data.BulletCount; i++)
        {
            var bulletSpreadSpacing = (data.SpreadAngle * 2) / (data.BulletCount - 1);
            var curSpreadAngle = data.SpreadAngle - (bulletSpreadSpacing * i);
            var curDirection = Quaternion.Euler(0, 0, curSpreadAngle) * direction;

            var bulletParams = new BulletFlightParams(
                _firePoint.position,
                curDirection,
                data.Damage,
                data.Speed,
                data.Range);

            var bullet = _magazine.GetBullet();
            bullet.StartFlight(bulletParams);
        }
    }
}