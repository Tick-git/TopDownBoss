using Gameplay.Boss;

public class TeleportAttackState : BaseState<BossController>
{
    public bool IsRunning { get; private set; }

    public TeleportAttackState(BossController context) : base(context)
    {
    }

    public override void Enter()
    {
        IsRunning = true;
        Context.AttackDecider.NotifyAttackStarted();
        
        Context.AttackSequenceRunner.AnimationChanged += OnAnimationChanged;
        Context.AttackSequenceRunner.SequenceFinished += OnSequenceFinished;

        Context.AttackSequenceRunner.AddAnimationStep(AttackAnimationType.Disappear);
        Context.AttackSequenceRunner.AddAnimationStep(AttackAnimationType.Teleport);
        Context.AttackSequenceRunner.AddAnimationStep(AttackAnimationType.Appear);
        Context.AttackSequenceRunner.AddAnimationStep(AttackAnimationType.TeleportAim);
        Context.AttackSequenceRunner.AddAnimationStep(AttackAnimationType.TeleportShot);

        Context.AttackSequenceRunner.StartSequence();
    }

    public override void Update()
    {
        Context.Weapon.ApplyAim(Context.TargetTracker.GetTargetPosition());
    }

    private void OnSequenceFinished()
    {
        Context.AttackSequenceRunner.AnimationChanged -= OnAnimationChanged;
        Context.AttackSequenceRunner.SequenceFinished -= OnSequenceFinished;
        
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