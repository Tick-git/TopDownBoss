using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.GameplayData.BossPaths + "Teleport")]
internal class TeleportData : ScriptableObject
{
    [SerializeField] private float _orbitRadius = 6;
    [SerializeField] private float _areaAngle = 180;
    [SerializeField] private float _positionsCount = 8;

    public float AreaAngle => _areaAngle;
    public float OrbitRadius => _orbitRadius;
    public float PositionsCount => _positionsCount;
}