using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] StaminaData _staminaData;

    private bool _regenerationEnabled = true; 
    
    public float CurrentStamina { get; private set; }
    public float MaxStamina => _staminaData.MaxStamina;
    
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

        var nextStamina = CurrentStamina + _staminaData.RegenerationRate * Time.deltaTime;

        SetStamina(nextStamina);
    }
}