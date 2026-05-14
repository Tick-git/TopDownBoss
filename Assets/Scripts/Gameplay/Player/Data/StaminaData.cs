using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.GameplayData.ComponentPath + "StaminaData")]
public class StaminaData : ScriptableObject
{
    [SerializeField] private int _maxStamina = 100;

    [SerializeField] [Tooltip("Regeneration Rate Per Second")]
    private float _regenerationRate = 20;

    public int MaxStamina => _maxStamina;
    public float RegenerationRate => _regenerationRate;
}