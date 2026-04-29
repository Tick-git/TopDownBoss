using System.Collections.Generic;
using UnityEngine;

public class BossAnimator : MonoBehaviour
{
    private static readonly int Aim = Animator.StringToHash("Aim");
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private static readonly int Holster = Animator.StringToHash("Holster");

    private readonly Dictionary<int, bool> _animationsRunning = new Dictionary<int, bool>();
    
    private Animator _animator;

    public bool AimingRunning => _animationsRunning[Aim];
    public bool ShootRunning => _animationsRunning[Shoot];
    public bool HolsterRunning => _animationsRunning[Holster];
    public bool AttackRunning  => AimingRunning || ShootRunning || HolsterRunning;

    public void Initialize()
    {
        _animator = GetComponent<Animator>();
        
        foreach (var behaviour in _animator.GetBehaviours<AnimatorStateEventBehaviour>())
        {
            behaviour.StateEnter += OnStateEnter;
            behaviour.StateExit += OnStateExit;
        }
        
        _animationsRunning.Add(Aim, true);
        _animationsRunning.Add(Shoot, true);
        _animationsRunning.Add(Holster, true);
    }

    public void OnDestroy()
    {
        foreach (var behaviour in _animator.GetBehaviours<AnimatorStateEventBehaviour>())
        {
            behaviour.StateEnter -= OnStateEnter;
            behaviour.StateExit -= OnStateExit;
        }
    }

    private void OnStateExit(int shortNameHash)
    {
        SetAnimationRunningEntry(shortNameHash, false);
    }

    private void OnStateEnter(int shortNameHash)
    {
        SetAnimationRunningEntry(shortNameHash, true);
    }

    private void SetAnimationRunningEntry(int shortNameHash, bool running)
    {
        if (!_animationsRunning.ContainsKey(shortNameHash))
        {
            Debug.LogError("Key Not Found: " + shortNameHash);
            return;
        }
        
        _animationsRunning[shortNameHash] = running;
    }

    public void SetAimTrigger()
    {
        _animator.SetTrigger(Aim);
    }
}