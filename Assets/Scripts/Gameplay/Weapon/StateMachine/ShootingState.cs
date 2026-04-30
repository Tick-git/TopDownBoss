public class ShootingState : BaseState<PlayerController>
{
    public ShootingState(PlayerController context) : base(context)
    {
    }

    public override void Enter()
    {
        Context.Movement.SetMoveSpeedMultiplicator(0.3f);
    }

    public override void Exit()
    {
        Context.Movement.ResetMoveSpeedMultiplicator();
    }

    public override void Update()
    {
        Context.Weapon.ApplyAim(Context.Input.AimDirection);
        Context.Weapon.TryShoot();
    }
}