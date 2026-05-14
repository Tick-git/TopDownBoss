using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.GameplayData.PlayerPaths + "PlayerStaminaData")]
public class PlayerStaminaData : StaminaData
{
    [Header("Player specific stamina")] [SerializeField]
    private float _rollCost = 20;

    [SerializeField] private float _regenerationEnableDelayTime = 0.5f;
    [SerializeField] private float _shootRegenerationMultiplier = 0.2f;

    public float ShootRegenerationMultiplier => _shootRegenerationMultiplier;
    public float RollCost => _rollCost;
    public float RegenerationEnableDelayTime => _regenerationEnableDelayTime;
}