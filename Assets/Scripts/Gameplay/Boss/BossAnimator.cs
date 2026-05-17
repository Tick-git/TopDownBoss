using System;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimator : MonoBehaviour
{
    private static readonly int Aim = Animator.StringToHash("Aim");
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private static readonly int Holster = Animator.StringToHash("Holster");

    private static readonly int Aim2 = Animator.StringToHash("Aim2");
    private static readonly int Shoot2 = Animator.StringToHash("Shoot2");
    private static readonly int Holster2 = Animator.StringToHash("Holster2");

    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int AttackSpeed = Animator.StringToHash("AttackSpeed");

    private static readonly int Teleport = Animator.StringToHash("Teleport");
    private static readonly int Appear = Animator.StringToHash("Appear");
    private static readonly int Disappear = Animator.StringToHash("Disappear");
    private static readonly int Invisible = Animator.StringToHash("Invisible");
    private static readonly int TeleportAim = Animator.StringToHash("TeleportAim");
    private static readonly int TeleportShoot = Animator.StringToHash("TeleportShoot");


    private readonly Dictionary<int, bool> _animationsRunning = new();

    private Animator _animator;

    public bool AimingRunning => _animationsRunning[Aim];
    public bool ShootRunning => _animationsRunning[Shoot];
    public bool HolsterRunning => _animationsRunning[Holster];

    public bool AimingRunning2 => _animationsRunning[Aim2];
    public bool ShootRunning2 => _animationsRunning[Shoot2];
    public bool HolsterRunning2 => _animationsRunning[Holster2];
    public bool DisappearingRunning => _animationsRunning[Disappear];
    public bool InvisibleRunning => _animationsRunning[Invisible];
    public bool AppearingRunning => _animationsRunning[Appear];
    public bool TeleportAimRunning => _animationsRunning[TeleportAim];
    public bool TeleportShootRunning => _animationsRunning[TeleportShoot];

    public event Action FootGrounded;

    public void Initialize()
    {
        _animator = GetComponent<Animator>();

        foreach (var behaviour in _animator.GetBehaviours<AnimatorStateEventBehaviour>())
        {
            behaviour.StateEnter += OnStateEnter;
            behaviour.StateExit += OnStateExit;
        }

        _animationsRunning.Add(Aim, false);
        _animationsRunning.Add(Shoot, false);
        _animationsRunning.Add(Holster, false);

        _animationsRunning.Add(Aim2, false);
        _animationsRunning.Add(Shoot2, false);
        _animationsRunning.Add(Holster2, false);

        _animationsRunning.Add(Disappear, false);
        _animationsRunning.Add(Invisible, false);
        _animationsRunning.Add(Appear, false);
        _animationsRunning.Add(TeleportAim, false);
        _animationsRunning.Add(TeleportShoot, false);
    }

    public void OnDestroy()
    {
        foreach (var behaviour in _animator.GetBehaviours<AnimatorStateEventBehaviour>())
        {
            behaviour.StateEnter -= OnStateEnter;
            behaviour.StateExit -= OnStateExit;
        }
    }

    public void SetAimTrigger() => _animator.SetTrigger(Aim);

    public void SetAim2Trigger() => _animator.SetTrigger(Aim2);

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