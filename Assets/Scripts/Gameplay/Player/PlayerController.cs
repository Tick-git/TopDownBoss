using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private WeaponAnimator _weaponAnimator;
    
    private StateMachine _movementSm;
    private StateMachine _weaponSm;

    private bool IsMoving => _input.MoveDirection != Vector2.zero;
    private bool _weaponEquipped = true;

    public PlayerAnimator PlayerAnimator => _playerAnimator;
    public PlayerMovement Movement => _movement;
    public InputReader Input => _input;

    public WeaponAnimator WeaponAnimator => _weaponAnimator;
    public Weapon Weapon => _weapon;


    private void Awake()
    {
        InitMovementStateMachine();
        InitWeaponStateMachine();
    }

    private void Update()
    {
        _playerAnimator.SetAimDirection(Input.AimPosition);
        _playerAnimator.SetIsMoving(IsMoving);

        _movementSm.Update();
        _weaponSm.Update();
    }

    private void FixedUpdate()
    {
        _movementSm.FixedUpdate();
        _weaponSm.FixedUpdate();
    }
    
    private void InitMovementStateMachine()
    {
        _movementSm = new StateMachine();

        var idleState = new IdleState(this);
        var walkState = new WalkState(this);
        var rollState = new RollState(this);

        AtMovement(idleState, walkState, new FuncPredicate(() => IsMoving));
        AtMovement(idleState, rollState, new FuncPredicate(() => _input.RollWasPressed));

        AtMovement(walkState, idleState, new FuncPredicate(() => !IsMoving));
        AtMovement(walkState, rollState, new FuncPredicate(() => _input.RollWasPressed));

        AtMovement(rollState, idleState, new FuncPredicate(() => !PlayerAnimator.RollAnimationRunning && !IsMoving));
        AtMovement(rollState, walkState, new FuncPredicate(() => !PlayerAnimator.RollAnimationRunning && IsMoving));

        _movementSm.SetState(idleState);
    }
    
    private void InitWeaponStateMachine()
    {
        _weaponSm = new StateMachine();

        var equippedState = new EquippedState(this);
        var holsteredState = new HolsteredState(this);
        var shootingState = new ShootingState(this);
        
        AtWeapon(equippedState, holsteredState, new FuncPredicate(() => !_weaponEquipped));
        AtWeapon(equippedState, shootingState, new FuncPredicate(() => Input.AttackIsPressed));
        
        AtWeapon(holsteredState, equippedState, new FuncPredicate(() => _weaponEquipped));
        
        AtWeapon(shootingState, equippedState, new FuncPredicate(() => !Input.AttackIsPressed));
        AtWeapon(shootingState, holsteredState, new FuncPredicate(() => !_weaponEquipped));

        _weaponSm.SetState(equippedState);
    }


    private void AtWeapon(IState from, IState to, IPredicate condition)
    {
        _weaponSm.AddTransition(from, to, condition);
    }
    
    private void AtMovement(IState from, IState to, IPredicate condition)
    {
        _movementSm.AddTransition(from, to, condition);
    }
    
    public void EquipWeapon()
    {
        _weaponEquipped = true;
    }

    public void HolsterWeapon()
    {
        _weaponEquipped = false;
    }
}