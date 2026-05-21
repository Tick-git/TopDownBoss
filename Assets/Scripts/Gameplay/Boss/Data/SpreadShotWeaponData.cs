using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.GameplayData.BossPaths + "SpreadShot")]
public class SpreadShotWeaponData : ScriptableObject
{
    [SerializeField] private float _damage = 10;
    [SerializeField] private float _spreadAngle = 7.5f;
    [SerializeField] private float _speed = 20;
    [SerializeField] private int _bulletCount = 3;
    [SerializeField] private int _range = 50;

    public float Damage => _damage;
    public float SpreadAngle => _spreadAngle;
    public float Speed => _speed;
    public int BulletCount => _bulletCount;
    public float Range => _range;
}