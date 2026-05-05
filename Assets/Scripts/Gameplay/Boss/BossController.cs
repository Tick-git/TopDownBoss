using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Boss
{
    public class BossController : MonoBehaviour
    {
        [SerializeField] private BossVisuals _visuals;
        [SerializeField] private BossAnimator _animator;

        private StateMachine _bossAttackSm;
        private StateMachine _bossMovementSm;

        private Timer _attackTimer;

        private bool _attack;
        private bool _attack2;
        private int _count;
        public BossAnimator Animator => _animator;
        public BossWeapon Weapon { get; private set; }
        public TargetTracker TargetTracker { get; private set; }
        public Movement Movement { get; private set; }

        private void Awake()
        {
            TargetTracker = GetComponent<TargetTracker>();
            Weapon = GetComponent<BossWeapon>();
            Movement = GetComponent<Movement>();

            Movement.Initialize();
            _animator.Initialize();

            _attackTimer = new Timer(2);
            _attackTimer.Completed += OnAttackTimerCompleted;
            _attackTimer.Start();

            InitBossMovementStateMachine();
            InitBossAttackStateMachine();
        }

        private void InitBossMovementStateMachine()
        {
            _bossMovementSm = new StateMachine();

            var idleState = new IdleState(this);
            var walkState = new WalkState(this);

            AddMoveTransition(idleState, walkState, new FuncPredicate(() => TargetTracker.DistanceToTarget() > 17.5f));
            AddMoveTransition(walkState, idleState, new FuncPredicate(() => TargetTracker.DistanceToTarget() < 15f));

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
            var attackState = new ShootState(this, new LargeSpreadShootBehaviour());
            var attackState2 = new ShootState(this, new SmallSpreadShootBehaviour());

            _bossAttackSm.AddTransition(decisionState, attackState, new FuncPredicate(() => _attack));
            _bossAttackSm.AddTransition(decisionState, attackState2, new FuncPredicate(() => _attack2));
            _bossAttackSm.AddTransition(attackState, decisionState, new FuncPredicate(() => !attackState.IsRunning));
            _bossAttackSm.AddTransition(attackState2, decisionState, new FuncPredicate(() => !attackState2.IsRunning));

            _bossAttackSm.SetState(decisionState);
        }

        private void OnAttackTimerCompleted()
        {
            var random = Random.Range(0, 2);

            if (random == 0)
            {
                _attack = true;
                _attack2 = false;
            }
            else
            {
                _attack = false;
                _attack2 = true;
            }

            _attackTimer.Reset(Random.Range(1, 5));
            _attackTimer.Start();
        }

        private void Update()
        {
            _visuals.Rotate(TargetTracker.IsRightSideOf(transform.position));

            _bossAttackSm.Update();
            _bossMovementSm.Update();
            _attackTimer.Tick(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _bossAttackSm.FixedUpdate();
            _bossMovementSm.FixedUpdate();
        }

        private void LateUpdate()
        {
            if (_attack || _attack2)
                _count++;

            if (_count >= 5)
            {
                _attack = false;
                _attack2 = false;
                _count = 0;
            }
        }
    }
}