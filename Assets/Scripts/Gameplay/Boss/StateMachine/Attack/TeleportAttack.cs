using Gameplay.Boss;

public class TeleportAttack : BaseState<BossController>
{
    private TeleportState _currentTeleportState;

    public bool IsRunning { get; private set; }

    public TeleportAttack(BossController context) : base(context)
    {
    }

    public override void Enter()
    {
        IsRunning = true;
        _currentTeleportState = TeleportState.None;
        Context.AttackDecider.NotifyAttackStarted();
    }

    public override void Exit()
    {
        Context.AttackDecider.NotifyAttackEnded();
        Context.Weapon.AimToDefault(Context.TargetTracker.GetTargetPosition());
    }

    public override void Update()
    {
        Context.Weapon.ApplyAim(Context.TargetTracker.GetTargetPosition());

        if (!IsRunning) return;

        TryToChangeState();
    }

    private void TryToChangeState()
    {
        switch (_currentTeleportState)
        {
            case TeleportState.None:
                ChangeState(TeleportState.Disappearing);
                break;
            case TeleportState.Disappearing when !Context.Animator.DisappearingRunning:
                ChangeState(TeleportState.Invisible);
                break;
            case TeleportState.Invisible when !Context.Animator.InvisibleRunning:
                ChangeState(TeleportState.Appearing);
                break;
            case TeleportState.Appearing when !Context.Animator.AppearingRunning:
                ChangeState(TeleportState.Aiming);
                break;
            case TeleportState.Aiming when !Context.Animator.TeleportAimRunning:
                ChangeState(TeleportState.Shooting);
                break;
            case TeleportState.Shooting when !Context.Animator.TeleportShootRunning:
                IsRunning = false;
                break;
        }
    }

    private void ChangeState(TeleportState newState)
    {
        switch (newState)
        {
            case TeleportState.None:
                break;
            case TeleportState.Disappearing:
                Context.Animator.SetTeleportTrigger();
                // Disable Hitbox
                break;
            case TeleportState.Invisible:
                break;
            case TeleportState.Appearing:
                Context.Teleport.TeleportIntoOrbit(Context.TargetTracker.GetTargetPosition());
                break;
            case TeleportState.Aiming:
                // Enable Hitbox
                break;
            case TeleportState.Shooting:
                Context.Audio.PlayShootSound();
                Context.Weapon.ShootSmallSpread(Context.TargetTracker.GetTargetPosition());
                break;
        }

        _currentTeleportState = newState;
    }

    private enum TeleportState
    {
        None,
        Disappearing,
        Invisible,
        Appearing,
        Aiming,
        Shooting
    }
}