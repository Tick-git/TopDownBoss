using Gameplay.Boss;

public class TeleportAttackState : BaseState<BossController>
{
    private readonly AnimationSequenceRunner _attackSequenceRunner;
    public bool IsRunning { get; private set; }

    public TeleportAttackState(BossController context) : base(context)
    {
        _attackSequenceRunner = new AnimationSequenceRunner(context.Animator);
    }

    public override void Enter()
    {
        IsRunning = true;
        Context.AttackDecider.NotifyAttackStarted();

        _attackSequenceRunner.Prepare();
        _attackSequenceRunner.AnimationChanged += OnAnimationChanged;
        _attackSequenceRunner.SequenceFinished += OnSequenceFinished;

        _attackSequenceRunner.AddAnimationStep(AttackAnimationType.Disappear);
        _attackSequenceRunner.AddAnimationStep(AttackAnimationType.Teleport);
        _attackSequenceRunner.AddAnimationStep(AttackAnimationType.Appear);
        _attackSequenceRunner.AddAnimationStep(AttackAnimationType.TeleportAim);
        _attackSequenceRunner.AddAnimationStep(AttackAnimationType.TeleportShot);

        _attackSequenceRunner.StartSequence();
    }

    public override void Update()
    {
        Context.Weapon.ApplyAim(Context.TargetTracker.GetTargetPosition());
    }

    private void OnSequenceFinished()
    {
        _attackSequenceRunner.CleanUp();
        _attackSequenceRunner.AnimationChanged -= OnAnimationChanged;
        _attackSequenceRunner.SequenceFinished -= OnSequenceFinished;
        
        IsRunning = false;
    }

    private void OnAnimationChanged(AttackAnimationType animation)
    {
        switch (animation)
        {
            case AttackAnimationType.Disappear:
                Context.Hitbox.DisableHitbox();
                break;
            case AttackAnimationType.Teleport:
                break;
            case AttackAnimationType.Appear:
                Context.Teleport.TeleportIntoOrbit(Context.TargetTracker.GetTargetPosition());
                break;
            case AttackAnimationType.TeleportAim:
                Context.Hitbox.EnableHitbox();
                break;
            case AttackAnimationType.TeleportShot:
                Context.Audio.PlayShootSound();
                Context.Weapon.ShootSmallSpread(Context.TargetTracker.GetTargetPosition());
                break;
        }
    }

    public override void Exit()
    {
        Context.Animator.PlayIdle();
        Context.AttackDecider.NotifyAttackEnded();
    }
}