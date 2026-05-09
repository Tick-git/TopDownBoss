using System;
using UnityEngine;

public class AnimationHitFeedback : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Health _health;

    private static readonly int Hit = Animator.StringToHash("Hit");

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float obj)
    {
        _animator.SetTrigger(Hit);
    }
}