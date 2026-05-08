using UnityEngine;

public class PlayerStaminaController : MonoBehaviour
{
    [SerializeField] private StaminaCosts _staminaCosts;
    [SerializeField] private Stamina _stamina;

    public bool CanRoll => _stamina.CurrentStamina >= _staminaCosts.RollCost;

    public void StartRoll()
    {
        _stamina.DisableRegeneration();
        _stamina.Consume(_staminaCosts.RollCost);
    }

    public void StopRoll()
    {
        _stamina.EnableRegeneration();
    }

    public void StartShoot()
    {
        _stamina.SetRegenerationMultiplier(0.1f);
    }

    public void StopShoot()
    {
        _stamina.ResetRegenerationMultiplier();
    }
}