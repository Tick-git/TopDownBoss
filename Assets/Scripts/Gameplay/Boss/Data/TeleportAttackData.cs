using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.GameplayData.Boss.Attack + "Teleport Attack")]
public class TeleportAttackData : ScriptableObject
{
    [SerializeField] private float _disappearAnimationTime = 0.25f;
    [SerializeField] private float _teleportAnimationTime = 0.25f;
    [SerializeField] private float _appearAnimationTime = 0.25f;
    [SerializeField] private float _teleportAimAnimationTime = 0.25f;
    [SerializeField] private float _teleportShotAnimationTime = 0.25f;

    public float DisappearAnimationTime => _disappearAnimationTime;
    public float TeleportAnimationTime => _teleportAnimationTime;
    public float AppearAnimationTime => _appearAnimationTime;
    public float TeleportAimAnimationTime => _teleportAimAnimationTime;
    public float TeleportShotAnimationTime => _teleportShotAnimationTime;
}