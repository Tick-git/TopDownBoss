public class RollState : BaseState<PlayerController>
{
    public RollState(PlayerController context) : base(context)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Context.Animator.SetRollTrigger();
    }

    public override void Update()
    {
        base.Update();
        
        Context.Movement.Roll(Context.Input.MoveDirection);
    }
}