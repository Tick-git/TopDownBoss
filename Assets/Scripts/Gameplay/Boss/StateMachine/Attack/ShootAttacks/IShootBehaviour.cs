using Gameplay.Boss;

public interface IShootBehaviour
{
    public int ShotsCount { get; }
    public void ApplyAim(BossController context);
    public void TriggerAim(BossController context);
    public void Shoot(BossController context);
    public bool AimRunning(BossController context);
    public bool ShootRunning(BossController context);
    public bool HolsterRunning(BossController context);
}