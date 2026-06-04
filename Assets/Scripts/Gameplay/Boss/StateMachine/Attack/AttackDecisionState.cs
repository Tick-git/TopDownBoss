using UnityEngine;

namespace Gameplay.Boss
{
    public class AttackDecisionState : BaseState<BossController>
    {
        public AttackDecisionState(BossController context) : base(context)
        {
        }

        public override void Update()
        {
            Context.Weapon.AimToDefault(Context.TargetTracker.GetTargetPosition(), Time.deltaTime);
        }
    }
}