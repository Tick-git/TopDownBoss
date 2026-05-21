using System;
using Gameplay.Boss;

public class LargeSpreadShotState : BaseBossAttackState
{
    public LargeSpreadShotState(BossController context) : base(context)
    {
    }

    public override void Enter()
    {
        base.Enter();

        var attackSequence = new AttackAnimationSequence()
            .AddStep(AttackAnimationType.HipAim, 0.75f)
            .AddStep(AttackAnimationType.HipShot)
            .AddStep(AttackAnimationType.HipHolster);

        for (int i = 0; i < 1; i++)
        {
            attackSequence
                .AddStep(AttackAnimationType.HipAim)
                .AddStep(AttackAnimationType.HipShot)
                .AddStep(AttackAnimationType.HipHolster);
        }

        Context.AttackSequenceRunner.Run(attackSequence);
    }

    public override void Update()
    {
        Context.Weapon.ApplyAim(Context.TargetTracker.GetTargetPosition());
    }

    protected override void OnAnimationEnter(AttackAnimationType animationType)
    {
        switch (animationType)
        {
            case AttackAnimationType.HipAim:
                break;
            case AttackAnimationType.HipShot:
                Context.Weapon.ShootBigSpread(Context.TargetTracker.GetTargetPosition());
                Context.Audio.PlayShootSound();
                break;
            case AttackAnimationType.HipHolster:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null);
        }
    }
}