public class HolsteredState : BaseState<PlayerController>
{
    public HolsteredState(PlayerController context) : base(context) {}

    public override void Enter()
    {
        base.Enter();
        
        Context.WeaponAnimator.SetHolstered();
    }
}