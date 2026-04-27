using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.GameplayData.PlayerPaths + "AssaultRifle")]
public class AssaultRifleData : ScriptableObject
{
    [SerializeField] private float _orbitRadius = 0.3f;
    [SerializeField] private float _damage = 10;
    [SerializeField] private float _fireRatePerSecond = 2;
    [SerializeField] private float _range = 10;
    [SerializeField] private float _bulletVelocity = 3;

    public float OrbitRadius => _orbitRadius;
    public float Damage => _damage;
    public float FireRatePerSecond => _fireRatePerSecond;
    public float Range => _range;
    public float BulletVelocity => _bulletVelocity;
}