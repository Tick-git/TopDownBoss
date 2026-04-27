using UnityEngine;

public class RollState : BaseState<PlayerController>
{
    public RollState(PlayerController context) : base(context)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Context.PlayerAnimator.SetRollTrigger();
        Context.HolsterWeapon();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        
        Context.Movement.Roll(Context.Input.MoveDirection, Time.fixedDeltaTime);
    }

    public override void Exit()
    {
        base.Exit();
        
        Context.EquipWeapon();
    }
}