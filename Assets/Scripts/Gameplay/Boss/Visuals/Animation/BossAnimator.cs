using System;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimator : MonoBehaviour
{
    private static readonly int AttackSpeedMultiplier = Animator.StringToHash("AttackSpeedMultiplier");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    private Animator _animator;

    private Dictionary<AttackAnimationType, int> _attackAnimationHashes;
    public event Action<AttackAnimationType> AttackAnimationFinished;
    public event Action FootGrounded;
    
    public void Initialize()
    {
        _animator = GetComponent<Animator>();

        _attackAnimationHashes = new Dictionary<AttackAnimationType, int>()
        {
            { AttackAnimationType.ShoulderAim, Animator.StringToHash("ShoulderAim") },
            { AttackAnimationType.ShoulderShot, Animator.StringToHash("ShoulderShot") },
            { AttackAnimationType.ShoulderHolster, Animator.StringToHash("ShoulderHolster") },
            { AttackAnimationType.HipAim, Animator.StringToHash("HipAim") },
            { AttackAnimationType.HipShot, Animator.StringToHash("HipShot") },
            { AttackAnimationType.HipHolster, Animator.StringToHash("HipHolster") },
            { AttackAnimationType.Teleport, Animator.StringToHash("Teleport") },
            { AttackAnimationType.Appear, Animator.StringToHash("Appear") },
            { AttackAnimationType.Disappear, Animator.StringToHash("Disappear") },
            { AttackAnimationType.TeleportAim, Animator.StringToHash("TeleportAim") },
            { AttackAnimationType.TeleportShot, Animator.StringToHash("TeleportShot")},
            { AttackAnimationType.GroundExplodeHandUp, Animator.StringToHash("GroundExplodeHandUp")},
            { AttackAnimationType.GroundExplodeHandDown, Animator.StringToHash("GroundExplodeHandDown")},
            { AttackAnimationType.GroundExplodeAttack, Animator.StringToHash("GroundExplodeAttack")},
            { AttackAnimationType.GroundExplodeRecover, Animator.StringToHash("GroundExplodeRecover")}
        };
        
        foreach (var behaviour in _animator.GetBehaviours<AttackStateBehaviour>())
        {
            behaviour.AnimationFinished += OnAnimationFinished;
        }
    }

    private void OnDestroy()
    {
        if (_animator == null) return;
        
        foreach (var behaviour in _animator.GetBehaviours<AttackStateBehaviour>())
        {
            behaviour.AnimationFinished -= OnAnimationFinished;
        }
    }

    private void OnAnimationFinished(AttackAnimationType type) => AttackAnimationFinished?.Invoke(type);
    
    public void PlayAttack(AttackAnimationType type, float animationTime)
    {
        if (_attackAnimationHashes.TryGetValue(type, out var hash))
        {
            _animator.SetFloat(AttackSpeedMultiplier, 1.0f / animationTime);
            _animator.Play(hash);
        }
        else
        {
            Debug.LogWarning("Animation doesn't exist: " + type);
        }
    }
    public void OnFootGroundedAnimationEvent() => FootGrounded?.Invoke();

    public void PlayIdle()
    {
        _animator.Play("Idle");
    }

    public void ResetAttackSpeedMultiplier()
    {
        _animator.SetFloat(AttackSpeedMultiplier, 1.0f);
    }
    
    public void StartMoving() => _animator.SetBool(IsMoving, true);

    public void StopMoving() => _animator.SetBool(IsMoving, false);
}

public enum AttackAnimationType
{
    ShoulderAim,
    ShoulderShot,
    ShoulderHolster,

    HipAim,
    HipShot,
    HipHolster,

    Disappear,
    Teleport,
    Appear,
    TeleportAim,
    TeleportShot,
    
    GroundExplodeHandUp,
    GroundExplodeHandDown,
    GroundExplodeAttack,
    GroundExplodeRecover
}