using System;
using Gameplay.Boss;
using UnityEngine;

public class SmallSpreadShotState : BaseBossAttackState
{
    public SmallSpreadShotState(BossController context) : base(context)
    {
    }

    public override void Enter()
    {
        base.Enter();

        var attackSequence = new AttackAnimationSequence()
            .AddStep(AttackAnimationType.ShoulderAim, 0.25f)
            .AddStep(AttackAnimationType.ShoulderShot, 1.5f)
            .AddStep(AttackAnimationType.ShoulderHolster, 1.5f);

        for (int i = 0; i < 2; i++)
        {
            attackSequence
                .AddStep(AttackAnimationType.ShoulderAim, 1.5f)
                .AddStep(AttackAnimationType.ShoulderShot, 1.5f)
                .AddStep(AttackAnimationType.ShoulderHolster, 1.5f);
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