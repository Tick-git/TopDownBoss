using Gameplay.Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private Movement _movement;
    [SerializeField] private AssaultRifle _weapon;
    [SerializeField] private WeaponAnimator _weaponAnimator;
    [SerializeField] private HitboxScaler _hitboxScaler;
    [SerializeField] private Health _health;
    [SerializeField] private PlayerStaminaController _playerStaminaController;
    [SerializeField] private PlayerAudio _playerAudio;

    private StateMachine _movementSm;
    private StateMachine _weaponSm;
    private RollBuffer _rollBuffer;

    private bool IsMoving => _input.MoveDirection != Vector2.zero;
    private bool _weaponEquipped = true;

    public PlayerAnimator PlayerAnimator => _playerAnimator;
    public Movement Movement => _movement;
    public InputReader Input => _input;
    public HitboxScaler HitboxScaler => _hitboxScaler;
    public WeaponAnimator WeaponAnimator => _weaponAnimator;
    public AssaultRifle Weapon => _weapon;
    public PlayerStaminaController PlayerStaminaController => _playerStaminaController;
    public PlayerAudio PlayerAudio => _playerAudio;

    private void Awake()
    {
        _playerAnimator.Initialize();
        _movement.Initialize();
        _weapon.Initialize(_health);
        _weaponAnimator.Initialize();
        _hitboxScaler.Initialize();

        _rollBuffer = new RollBuffer(0.125f);

        InitMovementStateMachine();
        InitWeaponStateMachine();
    }

    private void Update()
    {
        _playerAnimator.SetAimDirection(Input.AimDirection);
        _playerAnimator.SetIsMoving(IsMoving);

        _movementSm.Update();
        _weaponSm.Update();

        if (_input.RollWasPressed)
            _rollBuffer.Trigger();

        _rollBuffer.Tick(Time.deltaTime);
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
        AtMovement(idleState, rollState, new FuncPredicate(() => _rollBuffer.IsBuffered));

        AtMovement(walkState, idleState, new FuncPredicate(() => !IsMoving));
        AtMovement(walkState, rollState,
            new FuncPredicate(() => _rollBuffer.IsBuffered && PlayerStaminaController.CanRoll));

        AtMovement(rollState, idleState, new FuncPredicate(() => !PlayerAnimator.RollAnimationRunning && !IsMoving));
        AtMovement(rollState, walkState, new FuncPredicate(() => !PlayerAnimator.RollAnimationRunning && IsMoving));
        AtMovement(rollState, rollState,
            new FuncPredicate(() => !PlayerAnimator.RollAnimationRunning && _rollBuffer.IsBuffered));

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
        Weapon.PointInDirection(Input.AimDirection);
    }

    public void HolsterWeapon()
    {
        _weaponEquipped = false;
    }
}