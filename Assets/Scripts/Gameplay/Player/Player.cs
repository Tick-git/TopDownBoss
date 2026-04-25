using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private AssaultRifle _assaultRifle;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private Camera _cam;
    private InputAction _attackAction;
    private StateMachine _movementSm;
    
    public PlayerAnimator Animator => _animator;
    public PlayerMovement Movement => _movement;
    public InputReader Input => _input;

    private void Awake()
    {
        _cam = Camera.main;
        _attackAction = InputSystem.actions.FindAction("Attack");
        _attackAction.performed += Attack;

        _movementSm = new StateMachine();
        
        var idleState = new IdleState(this);
        var walkState = new WalkState(this);
        
        AtMovement(idleState, walkState, new FuncPredicate(() => _input.MoveInput != Vector2.zero));
        AtMovement(walkState, idleState, new FuncPredicate(() => _input.MoveInput == Vector2.zero));
        
        _movementSm.SetState(idleState);
    }

    private void AtMovement(IState from, IState to, IPredicate condition)
    {
        _movementSm.AddTransition(from, to, condition);
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        _assaultRifle.Shoot();
    }

    private void Update()
    {
        Vector3 target = _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        target.z = 0;
        
        _assaultRifle.ApplyAim(target);
        
        _spriteRenderer.flipX = target.x < transform.position.x;
        
        _movementSm.Update();
    }
}