using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    [SerializeField] private BossSpreadShotData _spreadShotData;
    [SerializeField] private Transform _weapon;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Magazine _magazine;
    [SerializeField] private Transform _firePoint;

    private Vector3 _smoothedRotation;

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

    public void SpreadShot(Vector2 target)
    {
        var direction = (target - (Vector2)_firePoint.position).normalized;
        
        for (int i = 0; i < _spreadShotData.BulletCount; i++)
        {
            var bulletSpreadSpacing = (_spreadShotData.SpreadAngle * 2) / (_spreadShotData.BulletCount - 1);
            var curSpreadAngle = _spreadShotData.SpreadAngle - (bulletSpreadSpacing * i);
            var curDirection = Quaternion.Euler(0, 0, curSpreadAngle) * direction;
            
            var bullet = _magazine.GetBullet();
            
            bullet.transform.position = _firePoint.position;
            bullet.StartFlight(curDirection, _spreadShotData.Damage, _spreadShotData.Speed);
        }
    }
}