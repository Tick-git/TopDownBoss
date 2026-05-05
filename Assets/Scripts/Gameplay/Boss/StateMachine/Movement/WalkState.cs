using UnityEngine;

namespace Gameplay.Boss
{
    public class WalkState : BaseState<BossController>
    {
        public WalkState(BossController context) : base(context)
        {
        }

        public override void Enter()
        {
            Context.Animator.StartMoving();
        }

        public override void Exit()
        {
            Context.Animator.StopMoving();
        }

        public override void FixedUpdate()
        {
            Context.Movement.Move(Context.TargetTracker.GetTargetDirection(), Time.fixedDeltaTime);
        }
    }
}