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

    public override void Update()
    {
        base.Update();
        
        Context.Movement.Roll(Context.Input.MoveDirection);
    }

    public override void Exit()
    {
        base.Exit();
        
        Context.EquipWeapon();
    }
}