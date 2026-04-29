using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    [SerializeField] private Transform _weapon;
    [SerializeField] private SpriteRenderer _renderer;
    
    private WeaponRotator _weaponRotator;

    public void Initialize()
    {
        _weaponRotator = new WeaponRotator(_renderer, _weapon);
    }
    
    public void ApplyAim(Vector2 target)
    {
        _weaponRotator.Rotate((target - (Vector2) _weapon.transform.position).normalized);
    }
}