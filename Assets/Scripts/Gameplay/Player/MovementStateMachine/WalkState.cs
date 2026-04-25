public class WalkState : BaseState<Player>
{
    public WalkState(Player context) : base(context) {}

    public override void Enter()
    {
        Context.Animator.PlayWalk();
    }

    public override void Update()
    {
        Context.Movement.Move(Context.Input.MoveInput);
    }
}