using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.GameplayData.PlayerPaths + "AssaultRifle")]
public class AssaultRifleData : ScriptableObject
{
    [SerializeField] private float _orbitRadius = 0.3f;
    [SerializeField] private float _damage = 10;
    [SerializeField] private float _fireRate = 1;
    [SerializeField] private float _range = 10;
    [SerializeField] private float _bulletVelocity = 3;

    public float OrbitRadius => _orbitRadius;
    public float Damage => _damage;
    public float FireRate => _fireRate;
    public float Range => _range;
    public float BulletVelocity => _bulletVelocity;
}