public class EquippedState : BaseState<PlayerController>
{
    public EquippedState(PlayerController context) : base(context) {}

    public override void Enter()
    {
        base.Enter();
        
        Context.WeaponAnimator.SetEquipped();
    }
}