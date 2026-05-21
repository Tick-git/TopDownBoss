using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.GameplayData.Boss.Attack + "Spread Shot Attack")]
public class SpreadShotAttackData : ScriptableObject
{
    [SerializeField] private int _shotCount = 3;

    [Header("Animations")] [SerializeField]
    private float _setupAnimationTime = 0.25f;

    [SerializeField] private float _aimAnimationTime = 0.25f;
    [SerializeField] private float _shootAnimationTime = 0.25f;
    [SerializeField] private float _holsterAnimationTime = 0.25f;

    public float SetupAnimationTime => _setupAnimationTime;
    public float AimAnimationTime => _aimAnimationTime;
    public float ShootAnimationTime => _shootAnimationTime;
    public float HolsterAnimationTime => _holsterAnimationTime;
    public int ShotCount => _shotCount;
}