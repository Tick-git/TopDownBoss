using Gameplay.Boss;

public class SmallSpreadShootBehaviour : IShootBehaviour
{
    public int ShotsCount => 3;
    private const float ShootOffsetFactor = 2.0f;

    public void TriggerAim(BossController context)
    {
        context.Animator.SetAimTrigger();
    }

    public void Shoot(BossController context)
    {
        var tracker = context.TargetTracker;
        var shootPos = tracker.GetTargetPosition() + (tracker.GetTargetMoveDirection() * ShootOffsetFactor);
        context.Weapon.ShootSmallSpread(shootPos);
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