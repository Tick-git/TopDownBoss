namespace Gameplay.Boss
{
    public class GroundAttackState : BaseBossAttackState
    {
        public GroundAttackState(BossController context) : base(context)
        {
        }

        public override void Enter()
        {
            base.Enter();

            var attackSequence = new AttackAnimationSequence()
                .AddStep(AttackAnimationType.GroundExplodeHandUp, 0.5f)
                .AddStep(AttackAnimationType.GroundExplodeHandDown)
                .AddStep(AttackAnimationType.GroundExplodeAttack)
                .AddStep(AttackAnimationType.GroundExplodeRecover);
            
            Context.AttackSequenceRunner.Run(attackSequence);
        }

        protected override void OnAnimationEnter(AttackAnimationType animationType)
        {
            
        }
    }
}