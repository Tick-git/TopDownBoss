using System;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] StaminaData _staminaData;

    private float _regenerationMultiplier = DefaultRegenerationMultiplier;

    public float CurrentStamina { get; private set; }
    public float MaxStamina => _staminaData.MaxStamina;

    private const float DefaultRegenerationMultiplier = 1.0f;
    public bool RegenerationEnabled { get; private set; } = true;

    private void Awake()
    {
        CurrentStamina = MaxStamina;
    }

    public void SetRegenerationMultiplier(float value)
    {
        _regenerationMultiplier = value;
    }

    public void ResetRegenerationMultiplier()
    {
        _regenerationMultiplier = DefaultRegenerationMultiplier;
    }

    public void EnableRegeneration() => RegenerationEnabled = true;

    public void DisableRegeneration() => RegenerationEnabled = false;

    public void Consume(float amount)
    {
        SetStamina(CurrentStamina - amount);
    }

    private void SetStamina(float stamina)
    {
        CurrentStamina = Mathf.Clamp(stamina, 0, _staminaData.MaxStamina);
    }

    private void Update()
    {
        if (!RegenerationEnabled) return;

        var nextStamina = CurrentStamina + _staminaData.RegenerationRate * _regenerationMultiplier * Time.deltaTime;

        SetStamina(nextStamina);
    }
}