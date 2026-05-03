using UnityEngine;

namespace Gameplay.Boss
{
    public class AttackState : BaseState<BossController>
    {
        private State _currentState;
        private int _shotsFiredCount;
        private const int ShotsTotal = 3;

        public bool IsRunning { get; private set; }

        enum State
        {
            None,
            Aim,
            Shoot,
            Holster
        }

        public AttackState(BossController context) : base(context)
        {
        }

        public override void Enter()
        {
            IsRunning = true;
            Context.Animator.SetSpeed(1.5f);
            _currentState = State.None;
            _shotsFiredCount = 0;
        }

        public override void Exit()
        {
            Context.Animator.ResetSpeed();
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
                Context.Animator.SetAimTrigger();
            }
            else if (_currentState == State.Aim && !Context.Animator.AimingRunning)
            {
                _currentState = State.Shoot;
                Context.Weapon.SpreadShot(GetTargetMoveDirection());
                _shotsFiredCount++;
            }
            else if (_currentState == State.Shoot && !Context.Animator.ShootRunning)
            {
                _currentState = State.Holster;
            }
            else if (_currentState == State.Holster && !Context.Animator.HolsterRunning)
            {
                _currentState = State.None;
                
                if (_shotsFiredCount >= ShotsTotal)
                {
                    IsRunning = false;
                }
            }
        }

        private Vector2 GetTargetMoveDirection()
        {
            return Context.TargetTracker.GetTargetPosition() + Context.TargetTracker.GetTargetMoveDirection();
        }
    }
}