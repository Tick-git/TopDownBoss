using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    private Animator _animator;

    private static readonly int Equipped = Animator.StringToHash("Equipped");

    public void Initialize()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetEquipped()
    {
        _animator.SetBool(Equipped, true);
    }

    public void SetHolstered()
    {
        _animator.SetBool(Equipped, false);
    }
}