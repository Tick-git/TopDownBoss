using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.GameplayData.Boss.Attack + "Ground Explode Animation Data")]
public class GroundExplodeAnimationData : ScriptableObject
{
    [SerializeField] private float _handsUpAnimationTime = 0.25f;
    [SerializeField] private float _handsHoldAnimationTime = 0.25f;
    [SerializeField] private float _handsDownAnimationTime = 0.25f;
    [SerializeField] private float _attackAnimationTime = 0.25f;
    [SerializeField] private float _recoverAnimationTime = 0.25f;

    public float HandsUpAnimationTime => _handsUpAnimationTime;
    public float HandsDownAnimationTime => _handsDownAnimationTime;
    public float AttackAnimationTime => _attackAnimationTime;
    public float RecoverAnimationTime => _recoverAnimationTime;
    public float HandsHoldAnimationTime => _handsHoldAnimationTime;
}