using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.GameplayData.BossPaths + "Attack Decision Data")]
public class AttackDecisionData : ScriptableObject
{
    [SerializeField] private BossAttack _attack;
    [SerializeField] private float _baseWeight;

    public BossAttack Attack => _attack;
    public float BaseWeight => _baseWeight;
}