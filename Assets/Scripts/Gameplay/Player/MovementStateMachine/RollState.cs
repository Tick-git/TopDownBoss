using UnityEngine;

public class RollState : BaseState<PlayerController>
{
    public RollState(PlayerController context) : base(context)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Context.PlayerAnimator.SetRollTrigger();
        Context.PlayerAudio.PlayRollSfx();

        Context.HolsterWeapon();
        Context.HitboxScaler.SetRolling();
        Context.PlayerStaminaController.StartRoll();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        Context.Movement.Roll(Context.Input.MoveDirection, Time.fixedDeltaTime);
    }

    public override void Exit()
    {
        base.Exit();

        Context.EquipWeapon();
        Context.HitboxScaler.SetStanding();
        Context.PlayerStaminaController.StopRoll();
        Context.Movement.SetMoveSpeedVelocityToZero();
    }
}