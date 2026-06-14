using UnityEngine;

public class WalkState : BaseState<PlayerController>
{
    public WalkState(PlayerController context) : base(context)
    {
    }

    public override void FixedUpdate()
    {
        Context.Movement.Move(Context.Input.MoveDirection, Time.fixedDeltaTime);
    }

    public override void Exit()
    {
        Context.Movement.SetMoveSpeedVelocityToZero();
    }
}