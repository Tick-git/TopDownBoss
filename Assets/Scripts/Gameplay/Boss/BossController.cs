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

        [Header("Data")] [SerializeField] private SpreadShotAttackData _smallSpreadShotAttackData;
        [SerializeField] private SpreadShotAttackData _largeSpreadShotAttackData;
        [SerializeField] private TeleportAttackData _teleportAttackData;
        [SerializeField] private GroundExplodeAnimationData _groundExplodeAnimationData;

        private StateMachine _attackSm;
        private StateMachine _movementSm;

        public BossAnimator Animator => _animator;
        public BossWeapon Weapon { get; private set; }
        public TargetTracker TargetTracker { get; private set; }
        public Movement Movement { get; private set; }
        public AttackDecider AttackDecider { get; private set; }

        public BossTeleport Teleport { get; private set; }
        public Hitbox Hitbox => _hitbox;
        public BossAudio Audio => _audio;
        public AnimationSequenceRunner AttackSequenceRunner { get; private set; }
        public BossMagic BossMagic { get; private set; }

        private void Awake()
        {
            TargetTracker = GetComponent<TargetTracker>();
            Weapon = GetComponent<BossWeapon>();
            Movement = GetComponent<Movement>();
            AttackDecider = GetComponent<AttackDecider>();
            Teleport = GetComponent<BossTeleport>();
            BossMagic = GetComponent<BossMagic>();
            AttackSequenceRunner = new AnimationSequenceRunner(Animator);

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
            _movementSm = new StateMachine();

            var idleState = new IdleState(this);
            var walkState = new WalkState(this);

            AddMoveTransition(idleState, walkState,
                new FuncPredicate(() => TargetTracker.DistanceToTarget() > 17.5f && !AttackDecider.IsAttacking));
            AddMoveTransition(walkState, idleState, new FuncPredicate(() => TargetTracker.DistanceToTarget() < 15f));
            AddMoveTransition(walkState, idleState, new FuncPredicate(() => AttackDecider.IsAttacking));

            _movementSm.SetState(idleState);
        }

        private void AddMoveTransition(IState fromState, IState toState, IPredicate condition)
        {
            _movementSm.AddTransition(fromState, toState, condition);
        }

        private void InitBossAttackStateMachine()
        {
            _attackSm = new StateMachine();

            var decisionState = new AttackDecisionState(this);
            var attackState = new LargeSpreadShotState(this, _largeSpreadShotAttackData);
            var attackState2 = new SmallSpreadShotState(this, _smallSpreadShotAttackData);
            var teleportAttack = new TeleportAttackState(this, _teleportAttackData);
            var groundAttack = new GroundAttackState(this, _groundExplodeAnimationData);

            _attackSm.AddTransition(decisionState, attackState, NextAttackIs(BossAttack.SmallSpreadShot));
            _attackSm.AddTransition(decisionState, attackState2, NextAttackIs(BossAttack.LargeSpreadShot));
            _attackSm.AddTransition(decisionState, teleportAttack, NextAttackIs(BossAttack.TeleportShot));
            _attackSm.AddTransition(decisionState, groundAttack, NextAttackIs(BossAttack.GroundExplode));

            _attackSm.AddTransition(attackState, decisionState, new FuncPredicate(() => !attackState.IsRunning));
            _attackSm.AddTransition(attackState2, decisionState, new FuncPredicate(() => !attackState2.IsRunning));
            _attackSm.AddTransition(teleportAttack, decisionState, new FuncPredicate(() => !teleportAttack.IsRunning));
            _attackSm.AddTransition(groundAttack, decisionState, new FuncPredicate(() => !groundAttack.IsRunning));

            _attackSm.SetState(decisionState);
        }

        private FuncPredicate NextAttackIs(BossAttack bossAttack)
        {
            return new FuncPredicate(() => AttackDecider.NextAttackIs(bossAttack));
        }

        private void Update()
        {
            _visuals.Rotate(TargetTracker.IsRightSideOf(transform.position));

            _attackSm.Update();
            _movementSm.Update();
        }

        private void FixedUpdate()
        {
            _attackSm.FixedUpdate();
            _movementSm.FixedUpdate();
        }
    }
}