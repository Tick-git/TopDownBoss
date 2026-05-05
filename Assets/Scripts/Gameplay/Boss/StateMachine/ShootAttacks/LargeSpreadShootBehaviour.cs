using Gameplay.Boss;

public class LargeSpreadShootBehaviour : IShootBehaviour
{
    public int ShotsCount => 2;
    
    public void TriggerAim(BossController context)
    {
        context.Animator.SetAim2Trigger();
    }

    public void Shoot(BossController context)
    {
        context.Weapon.ShootBigSpread(context.TargetTracker.GetTargetPosition());
    }

    public bool AimRunning(BossController context)
    {
        return context.Animator.AimingRunning2;
    }

    public bool ShootRunning(BossController context)
    {
        return context.Animator.ShootRunning2;
    }

    public bool HolsterRunning(BossController context)
    {
        return context.Animator.HolsterRunning2;
    }
}