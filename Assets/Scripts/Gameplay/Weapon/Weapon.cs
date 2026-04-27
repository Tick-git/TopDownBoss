using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]  private AssaultRifle _assaultRifle;
    
    public void TryShoot()
    {
        _assaultRifle.TryShoot();
    }

    public void Aim(Vector2 target)
    {
        _assaultRifle.ApplyAim(target);
    }
}