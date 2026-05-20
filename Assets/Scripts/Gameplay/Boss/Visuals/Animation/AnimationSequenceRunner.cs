using System;
using UnityEngine;

public class AnimationSequenceRunner : IDisposable
{
    private AttackAnimationType _currentAnimation;
    private AttackAnimationSequence _currentSequence;
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
        if (_currentAnimation != type)
        {
            Debug.LogWarning("Unexpected Animation Finished " + GetType());
        }

        PlayNextAnimation();
    }

    private void PlayNextAnimation()
    {
        if (_currentSequence.Count <= 0)
        {
            SequenceFinished?.Invoke();
            return;
        }
        
        _currentAnimation = _currentSequence.GetNextStep();
        
        _animator.PlayAttack(_currentAnimation);
        AnimationChanged?.Invoke(_currentAnimation);
    }

    public void Run(AttackAnimationSequence sequence)
    {
        _currentSequence = sequence;
        
        PlayNextAnimation();
    }
}