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
}