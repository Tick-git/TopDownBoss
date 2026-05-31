using UnityEngine;

public class RollState : BaseState<PlayerController>
{
    public RollState(PlayerController context) : base(context)
    {
    }

    public override void Enter()
    {
        Context.PlayerAnimator.SetRollTrigger();
        Context.PlayerAudio.PlayRollSfx();

        Context.HolsterWeapon();
        Context.HitboxScaler.SetRolling();
        Context.PlayerStaminaController.StartRoll();
        Context.Invulnerability.StartInvulnerability();
    }

    public override void FixedUpdate()
    {
        Context.Movement.Roll(Context.Input.MoveDirection, Time.fixedDeltaTime);
    }

    public override void Exit()
    {
        Context.EquipWeapon();
        Context.HitboxScaler.SetStanding();
        Context.PlayerStaminaController.StopRoll();
        Context.Movement.SetMoveSpeedVelocityToZero();
        Context.Invulnerability.StopInvulnerability();
    }
}