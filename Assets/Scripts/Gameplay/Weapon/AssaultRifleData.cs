using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapons/AssaultRifle")]
public class AssaultRifleData : ScriptableObject
{
    [SerializeField] private float _orbitRadius;
    [SerializeField] private float _damage;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _range;

    public float OrbitRadius => _orbitRadius;
    public float Damage => _damage;
    public float FireRate => _fireRate;
    public float Range => _range;
}