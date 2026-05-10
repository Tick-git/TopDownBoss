using System;
using UnityEngine;

public class AnimationHitFeedback : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Health _health;

    private static readonly int Hit = Animator.StringToHash("Hit");

    private void OnEnable()
    {
        _health.Hit += OnHit;
    }

    private void OnDisable()
    {
        _health.Hit -= OnHit;
    }

    private void OnHit(DamageContext damageContext)
    {
        _animator.SetTrigger(Hit);
    }
}