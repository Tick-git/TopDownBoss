using Gameplay.Boss;
using UnityEngine;

public class SmallSpreadShootBehaviour : IShootBehaviour
{
    public int ShotsCount => 3;

    public void ApplyAim(BossController context)
    {
        context.Weapon.ApplyAim(GetTargetMovePredictionForBullet(context));
    }

    public void TriggerAim(BossController context)
    {
        context.Animator.SetAimTrigger();
    }

    public void Shoot(BossController context)
    {
        var tracker = context.TargetTracker;
        context.Weapon.ShootSmallSpread(GetTargetMovePredictionForBullet(context));
    }

    public bool AimRunning(BossController context)
    {
        return context.Animator.AimingRunning;
    }

    public bool ShootRunning(BossController context)
    {
        return context.Animator.ShootRunning;
    }

    public bool HolsterRunning(BossController context)
    {
        return context.Animator.HolsterRunning;
    }

    private Vector2 GetTargetMovePredictionForBullet(BossController context)
    {
        var from = context.Weapon.FirePointPosition;
        var to = context.TargetTracker.GetTargetPosition();
        var bulletSpeed = context.Weapon.SmallSpreadShotBulletSpeed;

        var shootDistance = (to - from).magnitude;

        return context.TargetTracker.GetTargetMovePrediction(shootDistance / bulletSpeed);
    }
}