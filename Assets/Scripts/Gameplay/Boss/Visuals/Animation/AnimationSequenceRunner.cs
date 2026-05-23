using System;
using UnityEngine;

public class AnimationSequenceRunner : IDisposable
{
    private AttackAnimationStep _currentAnimationStep;
    private AttackAnimationSequence _currentSequence;

    private bool _running;
    public event Action<AttackAnimationType> AnimationChanged;
    public event Action SequenceFinished;

    private readonly BossAnimator _animator;

    public AnimationSequenceRunner(BossAnimator animator)
    {
        _animator = animator;
        _animator.AttackAnimationFinished += OnAttackAnimationFinished;
    }

    public void Dispose()
    {
        _animator.AttackAnimationFinished -= OnAttackAnimationFinished;
    }

    private void OnAttackAnimationFinished(AttackAnimationType type)
    {
        if (!_running) return;

        if (_currentSequence.Count <= 0)
        {
            _running = false;
            _animator.ResetAttackSpeedMultiplier();
            SequenceFinished?.Invoke();
            return;
        }

        if (_currentAnimationStep.AnimationType != type)
        {
            Debug.LogWarning("Unexpected Animation Finished " + GetType());
        }

        PlayNextAnimation();
    }

    private void PlayNextAnimation()
    {
        _currentAnimationStep = _currentSequence.GetNextStep();

        _animator.PlayAttack(_currentAnimationStep.AnimationType, _currentAnimationStep.AttackSpeedMultiplier);
        AnimationChanged?.Invoke(_currentAnimationStep.AnimationType);
    }

    public void Run(AttackAnimationSequence sequence)
    {
        _currentSequence = sequence;
        _running = true;

        PlayNextAnimation();
    }
}