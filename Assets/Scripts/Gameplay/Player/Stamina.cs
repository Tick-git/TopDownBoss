using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] StaminaData _staminaData;

    private bool _regenerationEnabled; 
    
    public float CurrentStamina { get; private set; }
    
    public void EnableRegeneration() => _regenerationEnabled = true;
    
    public void DisableRegeneration() => _regenerationEnabled = false;
    
    private void Update()
    {
        if (!_regenerationEnabled) return;

        var nextStamina = CurrentStamina + _staminaData.RegenerationRate * Time.deltaTime;

        CurrentStamina = Mathf.Clamp(nextStamina, 0, _staminaData.MaxStamina);
    }
}