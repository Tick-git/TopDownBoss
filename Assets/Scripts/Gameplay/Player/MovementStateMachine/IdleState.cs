public class IdleState : BaseState<Player>
{
    public IdleState(Player context) : base(context) {}

    public override void Enter()
    {
        Context.Animator.PlayIdle();
    }
}