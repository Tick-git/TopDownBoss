using UnityEngine;

namespace Gameplay.Boss
{
    public class BossController : MonoBehaviour
    {
        [SerializeField] private BossVisuals _visuals;
        [SerializeField] private BossAnimator _animator;

        private StateMachine _bossStateMachine;
        public BossAnimator Animator => _animator;
        public BossWeapon Weapon { get; private set; }
        public TargetTracker TargetTracker { get; private set; }

        private void Awake()
        {
            TargetTracker = GetComponent<TargetTracker>();
            Weapon = GetComponent<BossWeapon>();
            
            Weapon.Initialize();
            _animator.Initialize();
            
            _bossStateMachine = new StateMachine();

            var attackState = new AttackState(this);
            var idleState = new IdleState(this);
        
            _bossStateMachine.AddTransition(idleState, attackState, new FuncPredicate(() => true));
            _bossStateMachine.AddTransition(attackState, idleState, new FuncPredicate(() => !Animator.AttackRunning));
            
            _bossStateMachine.SetState(attackState);
        }

        private void Update()
        {
            _visuals.Rotate(TargetTracker.IsRightSideOf(transform.position));
        
            _bossStateMachine.Update();
        }

        private void FixedUpdate()
        {
            _bossStateMachine.FixedUpdate();
        }
    }

    public class AttackState : BaseState<BossController>
    {
        private State _currentState; 
        
        enum State
        {
            None,
            Aim,
            Shoot,
            Holster
        }
        
        public AttackState(BossController context) : base(context) { }

        public override void Enter()  => _currentState = State.None;

        public override void Exit()
        {
            _currentState = State.None;
        }

        public override void Update()
        {
            TryChangeState();

            switch (_currentState)
            {
                case State.Aim:
                    Context.Weapon.ApplyAim(Context.TargetTracker.GetTargetPosition());
                    break;
                case State.Shoot:
                    break;
                case State.Holster:
                    break;
            }
        }

        private void TryChangeState()
        {
            if (_currentState == State.None)
            {
                _currentState = State.Aim;
                Context.Animator.SetAimTrigger();
            }
            else if (_currentState == State.Aim && !Context.Animator.AimingRunning)
            {
                _currentState = State.Shoot;
            } 
            else if (_currentState == State.Shoot && !Context.Animator.ShootRunning)
            {
                _currentState = State.Holster;
            }
        }
    }

    public class IdleState : BaseState<BossController>
    {
        public IdleState(BossController context) : base(context) { }
    }
}