using UnityEngine;

namespace Gameplay.Boss
{
    public class BossController : MonoBehaviour
    {
        [SerializeField] private BossVisuals _visuals;
        [SerializeField] private Health _health;
        [SerializeField] private BossAudio _audio;
        [SerializeField] private Hitbox _hitbox;
        [SerializeField] private BossAnimator _animator;

        private StateMachine _bossAttackSm;
        private StateMachine _bossMovementSm;

        public BossAnimator Animator => _animator;
        public BossWeapon Weapon { get; private set; }
        public TargetTracker TargetTracker { get; private set; }
        public Movement Movement { get; private set; }
        public AttackDecider AttackDecider { get; private set; }

        public BossTeleport Teleport { get; private set; }
        public Hitbox Hitbox => _hitbox;
        public BossAudio Audio => _audio;
        public AnimationSequenceRunner AttackSequenceRunner { get; private set; }

        private void Awake()
        {
            TargetTracker = GetComponent<TargetTracker>();
            Weapon = GetComponent<BossWeapon>();
            Movement = GetComponent<Movement>();
            AttackDecider = GetComponent<AttackDecider>();
            Teleport = GetComponent<BossTeleport>();
            AttackSequenceRunner =  new AnimationSequenceRunner(Animator);
            
            Movement.Initialize();
            Weapon.Initialize(_health);
            AttackDecider.Initialize();
            Animator.Initialize();

            InitBossMovementStateMachine();
            InitBossAttackStateMachine();
        }

        private void OnDestroy()
        {
            AttackSequenceRunner.Dispose();
        }
        
        private void InitBossMovementStateMachine()
        {
            _bossMovementSm = new StateMachine();

            var idleState = new IdleState(this);
            var walkState = new WalkState(this);

            AddMoveTransition(idleState, walkState,
                new FuncPredicate(() => TargetTracker.DistanceToTarget() > 17.5f && !AttackDecider.IsAttacking));
            AddMoveTransition(walkState, idleState, new FuncPredicate(() => TargetTracker.DistanceToTarget() < 15f));
            AddMoveTransition(walkState, idleState, new FuncPredicate(() => AttackDecider.IsAttacking));

            _bossMovementSm.SetState(idleState);
        }

        private void AddMoveTransition(IState fromState, IState toState, IPredicate condition)
        {
            _bossMovementSm.AddTransition(fromState, toState, condition);
        }

        private void InitBossAttackStateMachine()
        {
            _bossAttackSm = new StateMachine();

            var decisionState = new AttackDecisionState(this);
            var attackState = new LargeSpreadShotState(this);
            var attackState2 = new SmallSpreadShotState(this);
            var teleportAttack = new TeleportAttackState(this);
            var groundAttack = new GroundAttackState(this);

            _bossAttackSm.AddTransition(decisionState, attackState, new FuncPredicate(() => AttackDecider.Attack));
            _bossAttackSm.AddTransition(decisionState, attackState2, new FuncPredicate(() => AttackDecider.Attack2));
            _bossAttackSm.AddTransition(decisionState, teleportAttack, new FuncPredicate(() => AttackDecider.Attack3));
            _bossAttackSm.AddTransition(decisionState, groundAttack, new FuncPredicate(() => AttackDecider.Attack4));

            _bossAttackSm.AddTransition(attackState, decisionState, new FuncPredicate(() => !attackState.IsRunning));
            _bossAttackSm.AddTransition(attackState2, decisionState, new FuncPredicate(() => !attackState2.IsRunning));
            _bossAttackSm.AddTransition(teleportAttack, decisionState, new FuncPredicate(() => !teleportAttack.IsRunning));
            _bossAttackSm.AddTransition(groundAttack, decisionState, new FuncPredicate(() => !groundAttack.IsRunning));


            _bossAttackSm.SetState(decisionState);
        }

        private void Update()
        {
            _visuals.Rotate(TargetTracker.IsRightSideOf(transform.position));

            _bossAttackSm.Update();
            _bossMovementSm.Update();
        }

        private void FixedUpdate()
        {
            _bossAttackSm.FixedUpdate();
            _bossMovementSm.FixedUpdate();
        }
    }
}