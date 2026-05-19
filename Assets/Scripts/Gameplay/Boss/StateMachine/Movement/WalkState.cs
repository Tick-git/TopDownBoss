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
            Context.AnimatorOld.StartMoving();
        }

        public override void Exit()
        {
            Context.AnimatorOld.StopMoving();
        }

        public override void FixedUpdate()
        {
            Context.Movement.Move(Context.TargetTracker.GetTargetDirection(), Time.fixedDeltaTime);
        }
    }
}