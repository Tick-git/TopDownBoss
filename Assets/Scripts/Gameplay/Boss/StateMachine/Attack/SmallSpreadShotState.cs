using System;
using Gameplay.Boss;
using UnityEngine;

public class SmallSpreadShotState : BaseBossAttackState
{
    private readonly SpreadShotAttackData _data;

    public SmallSpreadShotState(BossController context, SpreadShotAttackData data) : base(context)
    {
        _data = data;
    }

    public override void Enter()
    {
        base.Enter();

        var attackSequence = new AttackAnimationSequence()
            .AddStep(AttackAnimationType.ShoulderAim, _data.SetupAnimationTime)
            .AddStep(AttackAnimationType.ShoulderShot, _data.ShootAnimationTime)
            .AddStep(AttackAnimationType.ShoulderHolster, _data.HolsterAnimationTime);

        for (int i = 0; i < _data.ShotCount - 1; i++)
        {
            attackSequence
                .AddStep(AttackAnimationType.ShoulderAim, _data.AimAnimationTime)
                .AddStep(AttackAnimationType.ShoulderShot, _data.ShootAnimationTime)
                .AddStep(AttackAnimationType.ShoulderHolster, _data.HolsterAnimationTime);
        }

        Context.AttackSequenceRunner.Run(attackSequence);
    }

    public override void Update()
    {
        Context.Weapon.ApplyAim(GetTargetMovePredictionForBullet());
    }

    protected override void OnAnimationEnter(AttackAnimationType animationType)
    {
        switch (animationType)
        {
            case AttackAnimationType.ShoulderAim:
                break;
            case AttackAnimationType.ShoulderShot:
                Context.Weapon.ShootSmallSpread(GetTargetMovePredictionForBullet());
                Context.Audio.PlayShootSound();
                break;
            case AttackAnimationType.ShoulderHolster:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null);
        }
    }

    private Vector2 GetTargetMovePredictionForBullet()
    {
        var from = Context.Weapon.FirePointPosition;
        var to = Context.TargetTracker.GetTargetPosition();
        var bulletSpeed = Context.Weapon.SmallSpreadShotBulletSpeed;

        var shootDistance = (to - from).magnitude;

        return Context.TargetTracker.GetTargetMovePrediction(shootDistance / bulletSpeed);
    }
}