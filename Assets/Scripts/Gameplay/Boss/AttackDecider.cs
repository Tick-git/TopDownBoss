using UnityEngine;

namespace Gameplay.Boss
{
    public class AttackDecider : MonoBehaviour
    {
        private int _count;

        private Timer _attackTimer;

        public bool IsAttacking { get; private set; }
        public bool Attack { get; private set; }
        public bool Attack2 { get; private set; }

        public void NotifyAttackStarted()
        {
            IsAttacking = true;
        }

        public void NotifyAttackEnded()
        {
            IsAttacking = false;

            _attackTimer.Reset(Random.Range(1, 3));
            _attackTimer.Start();
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