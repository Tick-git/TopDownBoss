using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Boss
{
    public class BossController : MonoBehaviour
    {
        [SerializeField] private BossVisuals _visuals;
        [SerializeField] private BossAnimator _animator;

        private StateMachine _bossStateMachine;
        private Timer _attackTimer;

        private bool _attack;
        private bool _attack2;
        private int _count;
        public BossAnimator Animator => _animator;
        public BossWeapon Weapon { get; private set; }
        public TargetTracker TargetTracker { get; private set; }

        private void Awake()
        {
            TargetTracker = GetComponent<TargetTracker>();
            Weapon = GetComponent<BossWeapon>();

            _animator.Initialize();

            _bossStateMachine = new StateMachine();

            _attackTimer = new Timer(2);
            _attackTimer.Completed += OnAttackTimerCompleted;
            _attackTimer.Start();

            var attackState = new ShootState(this, new LargeSpreadShootBehaviour());
            var attackState2 = new ShootState(this, new SmallSpreadShootBehaviour());
            var idleState = new IdleState(this);

            _bossStateMachine.AddTransition(idleState, attackState, new FuncPredicate(() => _attack));
            _bossStateMachine.AddTransition(idleState, attackState2, new FuncPredicate(() => _attack2));

            _bossStateMachine.AddTransition(attackState, idleState, new FuncPredicate(() => !attackState.IsRunning));
            _bossStateMachine.AddTransition(attackState2, idleState, new FuncPredicate(() => !attackState2.IsRunning));

            _bossStateMachine.SetState(idleState);
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

            _bossStateMachine.Update();
            _attackTimer.Tick(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _bossStateMachine.FixedUpdate();
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