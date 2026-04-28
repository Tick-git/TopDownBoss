using UnityEngine;

public class BossAnimator : MonoBehaviour
{
    private static readonly int Aim = Animator.StringToHash("Aim");
    
    private Animator _animator;

    public bool AimingRunning { get; private set; }
    
    public void Initialize()
    {
        _animator = GetComponent<Animator>();
        var beh = _animator.GetBehaviour<AnimatorStateEventBehaviour>();
        
        beh.StateEnter += (_) => AimingRunning = true;   
        beh.StateExit += (_) => AimingRunning = false;   
    }
    
    public void SetAimTrigger()
    {
        _animator.SetTrigger(Aim);
    }
}