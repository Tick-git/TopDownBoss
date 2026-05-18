using Gameplay.Boss;
using UnityEngine;

public class LargeSpreadShootBehaviour : IShootBehaviour
{
    public int ShotsCount => 2;

    public void ApplyAim(BossController context)
    {
        context.Weapon.ApplyAim(context.TargetTracker.GetTargetPosition());
    }

    public void TriggerAim(BossController context)
    {
        context.Animator.SetHipAttackTrigger();
    }

    public void Shoot(BossController context)
    {
        context.Weapon.ShootBigSpread(context.TargetTracker.GetTargetPosition());
    }

    public bool AimRunning(BossController context)
    {
        return context.Animator.HipAimRunning;
    }

    public bool ShootRunning(BossController context)
    {
        return context.Animator.HipShotRunning;
    }

    public bool HolsterRunning(BossController context)
    {
        return context.Animator.HipHolsterRunning;
    }
}