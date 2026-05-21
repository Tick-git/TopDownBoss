using Gameplay.Boss;

public class TeleportAttackState : BossAttackState
{
    public TeleportAttackState(BossController context) : base(context)
    {
    }

    public override void Enter()
    {
        base.Enter();

        var animationSequence = new AttackAnimationSequence()
            .AddStep(AttackAnimationType.Disappear)
            .AddStep(AttackAnimationType.Teleport)
            .AddStep(AttackAnimationType.Appear)
            .AddStep(AttackAnimationType.TeleportAim)
            .AddStep(AttackAnimationType.TeleportShot);

        Context.AttackSequenceRunner.Run(animationSequence);
    }

    public override void Update()
    {
        Context.Weapon.ApplyAim(Context.TargetTracker.GetTargetPosition());
    }

    protected override void OnAnimationChanged(AttackAnimationType animation)
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