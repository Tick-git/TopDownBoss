using System.Collections;
using UnityEngine;

public class PlayerStaminaController : MonoBehaviour
{
    [SerializeField] private PlayerStaminaData _playerStaminaData;
    [SerializeField] private Stamina _stamina;

    private Coroutine _enableRegenerationRoutine;

    public bool CanRoll => _stamina.CurrentStamina >= _playerStaminaData.RollCost;

    public void StartRoll()
    {
        DisableRegeneration();
        _stamina.Consume(_playerStaminaData.RollCost);
    }

    public void StopRoll()
    {
        EnableRegenerationDelayed();
    }

    public void StartShoot()
    {
        _stamina.SetRegenerationMultiplier(_playerStaminaData.ShootRegenerationMultiplier);
    }

    public void StopShoot()
    {
        _stamina.ResetRegenerationMultiplier();
        _stamina.DisableRegeneration();
        EnableRegenerationDelayed();
    }

    private void DisableRegeneration()
    {
        if (_enableRegenerationRoutine != null)
        {
            StopCoroutine(_enableRegenerationRoutine);
            _enableRegenerationRoutine = null;
        }

        _stamina.DisableRegeneration();
    }

    private void EnableRegenerationDelayed()
    {
        if (_stamina.RegenerationEnabled) return;

        if (_enableRegenerationRoutine != null)
            StopCoroutine(_enableRegenerationRoutine);

        _enableRegenerationRoutine = StartCoroutine(EnableRegenerationDelayedRoutine());
    }

    private IEnumerator EnableRegenerationDelayedRoutine()
    {
        yield return new WaitForSeconds(_playerStaminaData.RegenerationEnableDelayTime);

        _stamina.EnableRegeneration();
    }
}