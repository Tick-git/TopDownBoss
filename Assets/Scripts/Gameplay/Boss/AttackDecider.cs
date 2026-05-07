using UnityEngine;

namespace Gameplay.Boss
{
    public class AttackDecider : MonoBehaviour
    {
        public bool Attack;
        public bool Attack2;
        private int _count;

        private Timer _attackTimer;

        public bool IsAttacking { get; private set; }

        public void NotifyAttackStarted()
        {
            IsAttacking = true;
        }

        public void NotifyAttackEnded()
        {
            IsAttacking = false;
        }

        public void Initialize()
        {
            IsAttacking = false;
            _attackTimer = new Timer(2);
            _attackTimer.Completed += OnAttackTimerCompleted;
            _attackTimer.Start();
        }

        private void OnAttackTimerCompleted()
        {
            var random = Random.Range(0, 2);

            if (random == 0)
            {
                Attack = true;
                Attack2 = false;
            }
            else
            {
                Attack = false;
                Attack2 = true;
            }

            _attackTimer.Reset(Random.Range(1, 5));
            _attackTimer.Start();
        }

        private void Update()
        {
            _attackTimer.Tick(Time.deltaTime);
        }

        private void LateUpdate()
        {
            if (Attack || Attack2)
                _count++;

            if (_count >= 5)
            {
                Attack = false;
                Attack2 = false;
                _count = 0;
            }
        }
    }
}