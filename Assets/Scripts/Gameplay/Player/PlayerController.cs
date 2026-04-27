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
    private bool IsMoving => _input.MoveDirection != Vector2.zero;

    public PlayerAnimator Animator => _animator;
    public PlayerMovement Movement => _movement;
    public InputReader Input => _input;

    private void Awake()
    {
        _movementSm = new StateMachine();

        var idleState = new IdleState(this);
        var walkState = new WalkState(this);
        var rollState = new RollState(this);

        AtMovement(idleState, walkState, new FuncPredicate(() => IsMoving));
        AtMovement(idleState, rollState, new FuncPredicate(() => _input.RollPressed));

        AtMovement(walkState, idleState, new FuncPredicate(() => !IsMoving));
        AtMovement(walkState, rollState, new FuncPredicate(() => _input.RollPressed));

        AtMovement(rollState, idleState, new FuncPredicate(() => !Animator.RollAnimationRunning && !IsMoving));
        AtMovement(rollState, walkState, new FuncPredicate(() => !Animator.RollAnimationRunning && IsMoving));

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
        _animator.SetIsMoving(IsMoving);

        _movementSm.Update();
    }
}