using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerWeapon _weapon;
    
    private InputAction _attackAction;
    private StateMachine _movementSm;
    
    public PlayerAnimator Animator => _animator;
    public PlayerMovement Movement => _movement;
    public InputReader Input => _input;

    private void Awake()
    {
        _movementSm = new StateMachine();
        
        var idleState = new IdleState(this);
        var walkState = new WalkState(this);
        
        AtMovement(idleState, walkState, new FuncPredicate(() => _input.MoveDirection != Vector2.zero));
        AtMovement(walkState, idleState, new FuncPredicate(() => _input.MoveDirection == Vector2.zero));
        
        _movementSm.SetState(idleState);
    }

    private void AtMovement(IState from, IState to, IPredicate condition)
    {
        _movementSm.AddTransition(from, to, condition);
    }
    
    private void Update()
    {
        _weapon.Aim(Input.AimPosition);

        if (_input.AttackPressed)
            _weapon.Shoot();
        
        _animator.SetLookDirection(Input.AimPosition.x < transform.position.x);
        
        _movementSm.Update();
    }
}