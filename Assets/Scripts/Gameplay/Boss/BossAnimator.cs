using System;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimator : MonoBehaviour
{
    private static readonly int ShoulderTrigger = Animator.StringToHash("ShoulderTrigger");
    private static readonly int ShoulderAim = Animator.StringToHash("ShoulderAim");
    private static readonly int ShoulderShot = Animator.StringToHash("ShoulderShot");
    private static readonly int ShoulderHolster = Animator.StringToHash("ShoulderHolster");

    private static readonly int HipTrigger = Animator.StringToHash("HipTrigger");
    private static readonly int HipAim = Animator.StringToHash("HipAim");
    private static readonly int HipShot = Animator.StringToHash("HipShot");
    private static readonly int HipHolster = Animator.StringToHash("HipHolster");

    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int AttackSpeed = Animator.StringToHash("AttackSpeed");

    private static readonly int Teleport = Animator.StringToHash("Teleport");
    private static readonly int Appear = Animator.StringToHash("Appear");
    private static readonly int Disappear = Animator.StringToHash("Disappear");
    private static readonly int Invisible = Animator.StringToHash("Invisible");
    private static readonly int TeleportAim = Animator.StringToHash("TeleportAim");
    private static readonly int TeleportShot = Animator.StringToHash("TeleportShot");


    private readonly Dictionary<int, bool> _animationsRunning = new();

    private Animator _animator;

    public bool ShoulderAimRunning => _animationsRunning[ShoulderAim];
    public bool ShoulderShotRunning => _animationsRunning[ShoulderShot];
    public bool ShoulderHolsterRunning => _animationsRunning[ShoulderHolster];
    public bool HipAimRunning => _animationsRunning[HipAim];
    public bool HipShotRunning => _animationsRunning[HipShot];
    public bool HipHolsterRunning => _animationsRunning[HipHolster];
    public bool DisappearingRunning => _animationsRunning[Disappear];
    public bool InvisibleRunning => _animationsRunning[Invisible];
    public bool AppearingRunning => _animationsRunning[Appear];
    public bool TeleportAimRunning => _animationsRunning[TeleportAim];
    public bool TeleportShootRunning => _animationsRunning[TeleportShot];

    public event Action FootGrounded;

    public void Initialize()
    {
        _animator = GetComponent<Animator>();

        foreach (var behaviour in _animator.GetBehaviours<AnimatorStateEventBehaviour>())
        {
            behaviour.StateEnter += OnStateEnter;
            behaviour.StateExit += OnStateExit;
        }

        _animationsRunning.Add(ShoulderAim, false);
        _animationsRunning.Add(ShoulderShot, false);
        _animationsRunning.Add(ShoulderHolster, false);

        _animationsRunning.Add(HipAim, false);
        _animationsRunning.Add(HipShot, false);
        _animationsRunning.Add(HipHolster, false);

        _animationsRunning.Add(Disappear, false);
        _animationsRunning.Add(Invisible, false);
        _animationsRunning.Add(Appear, false);
        _animationsRunning.Add(TeleportAim, false);
        _animationsRunning.Add(TeleportShot, false);
    }

    public void OnDestroy()
    {
        foreach (var behaviour in _animator.GetBehaviours<AnimatorStateEventBehaviour>())
        {
            behaviour.StateEnter -= OnStateEnter;
            behaviour.StateExit -= OnStateExit;
        }
    }

    public void SetShoulderAttackTrigger() => _animator.SetTrigger(ShoulderTrigger);

    public void SetHipAttackTrigger() => _animator.SetTrigger(HipTrigger);

    public void ResetSpeed() => _animator.SetFloat(AttackSpeed, 1);

    public void SetSpeed(float value) => _animator.SetFloat(AttackSpeed, value);

    public void StartMoving() => _animator.SetBool(IsMoving, true);

    public void StopMoving() => _animator.SetBool(IsMoving, false);

    public void OnFootGrounded() => FootGrounded?.Invoke();

    private void OnStateExit(int shortNameHash) => SetAnimationRunningEntry(shortNameHash, false);

    private void OnStateEnter(int shortNameHash) => SetAnimationRunningEntry(shortNameHash, true);

    private void SetAnimationRunningEntry(int shortNameHash, bool running)
    {
        if (!_animationsRunning.ContainsKey(shortNameHash))
        {
            Debug.LogError("Key Not Found: " + shortNameHash);
            return;
        }

        _animationsRunning[shortNameHash] = running;
    }

    public void SetTeleportTrigger()
    {
        _animator.SetTrigger(Teleport);
    }
}