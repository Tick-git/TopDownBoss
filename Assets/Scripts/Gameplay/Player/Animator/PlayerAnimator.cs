using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public bool RollAnimationRunning = false;
    
    private static readonly int MoveInput = Animator.StringToHash("MoveInput");
    private static readonly int Roll = Animator.StringToHash("Roll");
    
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private RollStateBehaviour _rollState;

    private Vector2 _lastAimDirection;

    public void Initialize()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        _rollState = _animator.GetBehaviour<RollStateBehaviour>();
        _rollState.RollEnter += OnRollEnter;
        _rollState.RollExit += OnRollExit;
    }

    private void OnDestroy()
    {
        _rollState.RollEnter -= OnRollEnter;
        _rollState.RollExit -= OnRollExit;
    }
    
    private void OnRollExit() => RollAnimationRunning = false;

    private void OnRollEnter() => RollAnimationRunning = true;

    public void SetIsMoving(bool value)
    {
        _animator.SetBool(MoveInput, value);
    }

    public void SetRollTrigger()
    {
        _animator.SetTrigger(Roll);
    }

    public void SetAimDirection(Vector2 aimDirection)
    {
        if (aimDirection == Vector2.zero)
            return;
        
        _spriteRenderer.flipX  = aimDirection.x < 0;
    }
}