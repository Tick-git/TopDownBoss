using UnityEngine;

namespace Gameplay.Boss
{
    public class AttackDecider : MonoBehaviour
    {
        [SerializeField] private bool _forceAttack1;
        [SerializeField] private bool _forceAttack2;
        [SerializeField] private bool _forceAttack3;
        [SerializeField] private bool _forceAttack4;

        private Timer _attackTimer;

        public bool IsAttacking { get; private set; }
        public bool Attack { get; private set; }
        public bool Attack2 { get; private set; }
        public bool Attack3 { get; private set; }

        public bool Attack4 { get; private set; }

        public void NotifyAttackStarted()
        {
            IsAttacking = true;
        }

        public void NotifyAttackEnded()
        {
            IsAttacking = false;
            Attack = false;
            Attack2 = false;
            Attack3 = false;
            Attack4 = false;

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
            if (_forceAttack1)
            {
                Attack = true;
                return;
            }

            if (_forceAttack2)
            {
                Attack2 = true;
                return;
            }

            if (_forceAttack3)
            {
                Attack3 = true;
                return;
            }

            if (_forceAttack4)
            {
                Attack4 = true;
                return;
            }

            var random = Random.Range(0, 4);

            if (random == 0)
            {
                Attack = true;
                Attack2 = false;
                Attack3 = false;
                Attack4 = false;
            }
            else if (random == 1)
            {
                Attack = false;
                Attack2 = true;
                Attack3 = false;
                Attack4 = false;
            }
            else if (random == 2)
            {
                Attack = false;
                Attack2 = false;
                Attack3 = true;
                Attack4 = false;
            }
            else
            {
                Attack = false;
                Attack2 = false;
                Attack3 = false;
                Attack4 = true;
            }
        }

        private void Update()
        {
            _attackTimer.Tick(Time.deltaTime);
        }
    }
}