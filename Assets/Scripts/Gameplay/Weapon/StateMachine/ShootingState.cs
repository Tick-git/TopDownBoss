public class ShootingState : BaseState<PlayerController>
{
    public ShootingState(PlayerController context) : base(context)
    {
    }

    public override void Update()
    {
        base.Update();

        Context.Weapon.Aim(Context.Input.AimDirection);
        Context.Weapon.TryShoot();
    }
}