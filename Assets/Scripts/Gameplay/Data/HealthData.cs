using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.GameplayData.ComponentPath + "Health")]
public class HealthData : ScriptableObject
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _minHealth = 0f;

    public float MaxHealth => _maxHealth;
    public float MinHealth => _minHealth;
}