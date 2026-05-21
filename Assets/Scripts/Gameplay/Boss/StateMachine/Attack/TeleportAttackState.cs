using Gameplay.Boss;

public class TeleportAttackState : BaseBossAttackState
{
    private readonly TeleportAttackData _data;

    public TeleportAttackState(BossController context, TeleportAttackData data) : base(context)
    {
        _data = data;
    }

    public override void Enter()
    {
        base.Enter();

        var animationSequence = new AttackAnimationSequence()
            .AddStep(AttackAnimationType.Disappear, _data.DisappearAnimationTime)
            .AddStep(AttackAnimationType.Teleport, _data.TeleportAnimationTime)
            .AddStep(AttackAnimationType.Appear, _data.AppearAnimationTime)
            .AddStep(AttackAnimationType.TeleportAim, _data.TeleportAimAnimationTime)
            .AddStep(AttackAnimationType.TeleportShot, _data.TeleportShotAnimationTime);

        Context.AttackSequenceRunner.Run(animationSequence);
    }

    public override void Update()
    {
        Context.Weapon.ApplyAim(Context.TargetTracker.GetTargetPosition());
    }

    protected override void OnAnimationEnter(AttackAnimationType animation)
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
}