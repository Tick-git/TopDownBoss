using System;
using Gameplay.Boss;
using UnityEngine;

public class LargeSpreadShotState : BaseBossAttackState
{
    private readonly SpreadShotAttackData _data;

    public LargeSpreadShotState(BossController context, SpreadShotAttackData data) : base(context)
    {
        _data = data;
    }

    public override void Enter()
    {
        base.Enter();

        var attackSequence = new AttackAnimationSequence()
            .AddStep(AttackAnimationType.HipAim, _data.SetupAnimationTime)
            .AddStep(AttackAnimationType.HipShot, _data.ShootAnimationTime)
            .AddStep(AttackAnimationType.HipHolster, _data.HolsterAnimationTime);

        for (int i = 0; i < 1; i++)
        {
            attackSequence
                .AddStep(AttackAnimationType.HipAim, _data.AimAnimationTime)
                .AddStep(AttackAnimationType.HipShot, _data.ShootAnimationTime)
                .AddStep(AttackAnimationType.HipHolster, _data.HolsterAnimationTime);
        }

        Context.AttackSequenceRunner.Run(attackSequence);
    }

    public override void Update()
    {
        Context.Weapon.ApplyAim(Context.TargetTracker.GetTargetPosition(), Time.deltaTime);
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