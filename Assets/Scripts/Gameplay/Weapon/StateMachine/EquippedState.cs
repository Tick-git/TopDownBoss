using UnityEngine;

public class EquippedState : BaseState<PlayerController>
{
    public EquippedState(PlayerController context) : base(context)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Context.WeaponAnimator.SetEquipped();
    }

    public override void Update()
    {
        base.Update();

        Context.Weapon.ApplyAim(Context.Input.AimDirection);
    }
}