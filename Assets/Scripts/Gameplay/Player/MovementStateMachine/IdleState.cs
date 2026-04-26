public class IdleState : BaseState<PlayerController>
{
    public IdleState(PlayerController context) : base(context) {}

    public override void Enter()
    {
        Context.Animator.PlayIdle();
    }
}