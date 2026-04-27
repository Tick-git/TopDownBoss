public class WalkState : BaseState<PlayerController>
{
    public WalkState(PlayerController context) : base(context) {}

    public override void Enter()
    {
    }

    public override void Update()
    {
        Context.Movement.Move(Context.Input.MoveDirection);
    }
}