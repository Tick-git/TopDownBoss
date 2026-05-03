using UnityEngine;

namespace Gameplay.Boss
{
    public class BossController : MonoBehaviour
    {
        [SerializeField] private BossVisuals _visuals;
        [SerializeField] private BossAnimator _animator;

        private StateMachine _bossStateMachine;
        private float _timer;
        private float _maxTimer = 1;
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

            _bossStateMachine.AddTransition(idleState, attackState, new FuncPredicate(() =>
            {
                _timer += Time.deltaTime;

                if (_timer > _maxTimer)
                {
                    _timer = 0;
                    _maxTimer = Random.Range(1, 3);
                    return true;
                }

                return false;
            }));

            _bossStateMachine.AddTransition(attackState, idleState, new FuncPredicate(() => !attackState.IsRunning));

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
}