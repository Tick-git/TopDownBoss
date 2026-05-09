using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.GameplayData.PlayerPaths + "StaminaCosts")]
public class StaminaCosts : ScriptableObject
{
    [SerializeField] private float _rollCost;

    public float RollCost => _rollCost;
}