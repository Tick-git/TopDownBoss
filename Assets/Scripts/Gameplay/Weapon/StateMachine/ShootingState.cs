public class ShootingState : BaseState<PlayerController>
{
    public ShootingState(PlayerController context) : base(context)
    {
    }

    public override void Enter()
    {
        Context.Movement.SetMoveSpeedMultiplicator(0.3f);
        Context.PlayerStaminaController.StartShoot();
    }

    public override void Exit()
    {
        Context.Movement.ResetMoveSpeedMultiplicator();
        Context.PlayerStaminaController.StopShoot();
    }

    public override void Update()
    {
        Context.Weapon.ApplyAim(Context.Input.AimDirection);

        if (Context.Weapon.TryShoot())
        {
            Context.PlayerAudio.PlayShootSfx();
        }
    }
}