namespace Gameplay.Boss
{
    public class ShootState : BaseState<BossController>
    {
        private State _currentState;
        private int _shotsFiredCount;
        private readonly IShootBehaviour _shootBehaviour;

        public bool IsRunning { get; private set; }

        enum State
        {
            None,
            Aim,
            Shoot,
            Holster
        }

        public ShootState(BossController context, IShootBehaviour behaviour) : base(context)
        {
            _shootBehaviour = behaviour;
        }

        public override void Enter()
        {
            IsRunning = true;
            _currentState = State.None;
            _shotsFiredCount = 0;
            Context.Animator.SetSpeed(0.25f);
            Context.AttackDecider.NotifyAttackStarted();
        }

        public override void Exit()
        {
            Context.Animator.ResetSpeed();
            Context.AttackDecider.NotifyAttackEnded();
        }

        public override void Update()
        {
            TryChangeState();

            Context.Weapon.ApplyAim(Context.TargetTracker.GetTargetPosition());
        }

        private void TryChangeState()
        {
            if (_currentState == State.None && IsRunning)
            {
                _currentState = State.Aim;
                _shootBehaviour.TriggerAim(Context);
            }
            else if (_currentState == State.Aim && !_shootBehaviour.AimRunning(Context))
            {
                _currentState = State.Shoot;
                Context.Animator.SetSpeed(1.5f);
                _shootBehaviour.Shoot(Context);
                _shotsFiredCount++;
            }
            else if (_currentState == State.Shoot && !_shootBehaviour.ShootRunning(Context))
            {
                _currentState = State.Holster;
            }
            else if (_currentState == State.Holster && !_shootBehaviour.HolsterRunning(Context))
            {
                _currentState = State.None;

                if (_shotsFiredCount >= _shootBehaviour.ShotsCount)
                {
                    IsRunning = false;
                }
            }
        }
    }
}