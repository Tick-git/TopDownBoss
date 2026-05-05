using Gameplay.Boss;

public class SmallSpreadShootBehaviour : IShootBehaviour
{
    public int ShotsCount => 3;
    
    public void TriggerAim(BossController context)
    {
        context.Animator.SetAimTrigger();
    }

    public void Shoot(BossController context)
    {
        context.Weapon.ShootSmallSpread(context.TargetTracker.GetNextPositionPrediction());
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
}