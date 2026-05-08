using System;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] StaminaData _staminaData;

    private float _regenerationMultiplier = DefaultRegenerationMultiplier;
    private bool _regenerationEnabled = true;

    public float CurrentStamina { get; private set; }
    public float MaxStamina => _staminaData.MaxStamina;

    private const float DefaultRegenerationMultiplier = 1.0f;

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

    public void EnableRegeneration() => _regenerationEnabled = true;

    public void DisableRegeneration() => _regenerationEnabled = false;

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
        if (!_regenerationEnabled) return;

        var nextStamina = CurrentStamina + _staminaData.RegenerationRate * _regenerationMultiplier * Time.deltaTime;

        SetStamina(nextStamina);
    }
}