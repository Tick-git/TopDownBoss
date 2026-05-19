using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSequenceRunner 
{
    private AttackAnimationType _currentAnimation;
    
    private readonly Queue<AttackAnimationType> _animationSequence = new();
    private readonly BossAnimator _animator;

    public event Action<AttackAnimationType> AnimationChanged;
    public event Action SequenceFinished;
    
    public AnimationSequenceRunner(BossAnimator animator)
    {
        _animator = animator;
    }

    public void Prepare()
    {
        _animationSequence.Clear();
        _animator.AttackAnimationFinished += OnAttackAnimationFinished;
    }

    public void CleanUp()
    {
        _animator.AttackAnimationFinished -= OnAttackAnimationFinished;
    }

    public void AddAnimationStep(AttackAnimationType type)
    {
        _animationSequence.Enqueue(type);
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
        if (_animationSequence.Count <= 0)
        {
            SequenceFinished?.Invoke();
            return;
        }
        
        _currentAnimation = _animationSequence.Dequeue();
        
        _animator.PlayAttack(_currentAnimation);
        AnimationChanged?.Invoke(_currentAnimation);
    }

    public void StartSequence()
    {
        PlayNextAnimation();
    }
}
