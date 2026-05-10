using UnityEngine;

public class AnimationHitFeedback : HitFeedback
{
    [SerializeField] Animator _animator;

    private static readonly int Hit = Animator.StringToHash("Hit");

    protected override void OnHit(DamageContext damageContext)
    {
        _animator.SetTrigger(Hit);
    }
}