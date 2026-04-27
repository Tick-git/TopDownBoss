using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]  private AssaultRifle _assaultRifle;
    
    public void Shoot()
    {
        _assaultRifle.Shoot();
    }

    public void Aim(Vector2 target)
    {
        _assaultRifle.ApplyAim(target);
    }
}