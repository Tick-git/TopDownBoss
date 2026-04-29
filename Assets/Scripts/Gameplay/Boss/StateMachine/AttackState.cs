namespace Gameplay.Boss
{
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

        public override void Enter() => _currentState = State.None;

        public override void Exit()
        {
            _currentState = State.None;
        }

        public override void Update()
        {
            TryChangeState();
            
            Context.Weapon.ApplyAim(Context.TargetTracker.GetTargetPosition());
            
            switch (_currentState)
            {
                case State.Aim:
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
                Context.Weapon.Shoot(Context.TargetTracker.GetTargetPosition());
            } 
            else if (_currentState == State.Shoot && !Context.Animator.ShootRunning)
            {
                _currentState = State.Holster;
            }
        }
    }
}