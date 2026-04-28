using System;
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
            
            _animator.Initialize();
            
            _bossStateMachine = new StateMachine();

            var attackState = new AttackState(this);
            var idleState = new IdleState(this);
        
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
        private readonly StateMachine _attackSm;
        private readonly AimState _aimState;

        public AttackState(BossController context) : base(context)
        {
            _attackSm =  new StateMachine();
            
            _aimState = new AimState(context);
            var shootState = new ShootState(context);
            
            _attackSm.AddTransition(_aimState, shootState, new FuncPredicate(() => !Context.Animator.AimingRunning));
        }

        public override void Enter()
        {
            _attackSm.SetState(_aimState);
        }

        public override void Update()
        {
            _attackSm.Update();
        }

        class AimState : BaseState<BossController>
        {
            public AimState(BossController context) : base(context) {}

            public override void Enter() => Context.Animator.SetAimTrigger();

            public override void Update() => Context.Weapon.ApplyAim(Context.TargetTracker.GetTargetPosition());
        }
        
        class ShootState : BaseState<BossController>
        {
            public ShootState(BossController context) : base(context) {}

            public override void Enter() => Debug.Log("Entered");
            //
            // public override void Update() => Context.Weapon.ApplyAim(Context.TargetTracker.GetTargetPosition());
        }
    }

    public class IdleState : BaseState<BossController>
    {
        public IdleState(BossController context) : base(context) { }
    }
}