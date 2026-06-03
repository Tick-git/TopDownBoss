using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.GameplayData.BossPaths + "Boss Phase Attacks")]
public class BossPhaseAttacksData : ScriptableObject
{
    [SerializeField] private BossAttack[] _phase1Attacks;
    [SerializeField] private BossAttack[] _phase2Attacks;

    public BossAttack[] Phase1Attacks => _phase1Attacks;
    public BossAttack[] Phase2Attacks => _phase2Attacks;
}