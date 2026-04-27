using UnityEngine;

public class WalkState : BaseState<PlayerController>
{
    public WalkState(PlayerController context) : base(context) {}
    
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        
        Context.Movement.Move(Context.Input.MoveDirection, Time.fixedDeltaTime);
    }
}