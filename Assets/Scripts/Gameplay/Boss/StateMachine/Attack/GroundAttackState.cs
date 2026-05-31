using System;

namespace Gameplay.Boss
{
    public class GroundAttackState : BaseBossAttackState
    {
        private readonly GroundExplodeAnimationData _data;

        public GroundAttackState(BossController context, GroundExplodeAnimationData data) : base(context)
        {
            _data = data;
        }

        public override void Enter()
        {
            base.Enter();

            var attackSequence = new AttackAnimationSequence()
                .AddStep(AttackAnimationType.GroundExplodeHandUp, _data.HandsUpAnimationTime)
                .AddStep(AttackAnimationType.GroundExplodeHandDown, _data.HandsDownAnimationTime)
                .AddStep(AttackAnimationType.GroundExplodeAttack, _data.AttackAnimationTime)
                .AddStep(AttackAnimationType.GroundExplodeRecover, _data.RecoverAnimationTime);

            Context.AttackSequenceRunner.Run(attackSequence);
        }

        protected override void OnAnimationEnter(AttackAnimationType animationType)
        {
            switch (animationType)
            {
                case AttackAnimationType.GroundExplodeHandUp:
                    break;
                case AttackAnimationType.GroundExplodeHandDown:
                    break;
                case AttackAnimationType.GroundExplodeAttack:
                    Context.BossMagic.ExplodeGround(Context.TargetTracker.GetTargetPosition());
                    break;
                case AttackAnimationType.GroundExplodeRecover:
                    break;
            }
        }
    }
}