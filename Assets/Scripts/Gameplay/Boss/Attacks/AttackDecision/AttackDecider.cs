using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Boss
{
    public class AttackDecider : MonoBehaviour
    {
        [SerializeField] private AttackDecisionData[] _attackDecisionData;

        [Header("Debug Force Attack")] [SerializeField]
        private bool _shouldForceAttack = false;

        [SerializeField] private BossAttack _forceAttack = BossAttack.GroundExplode;

        private Timer _attackTimer;
        private BossAttack _nextAttack;

        private List<AttackDecision> _attackDecisions;

        public bool IsAttacking { get; private set; }

        public void Initialize()
        {
            _attackDecisions = new List<AttackDecision>();

            foreach (var data in _attackDecisionData)
            {
                _attackDecisions.Add(new AttackDecision(data.Attack, data.BaseWeight));
            }

            IsAttacking = false;
            _attackTimer = new Timer(2);
            _attackTimer.Completed += OnAttackTimerCompleted;
            _attackTimer.Start();
        }

        public bool NextAttackIs(BossAttack attack)
        {
            return _nextAttack == attack && !IsAttacking && !_attackTimer.IsRunning;
        }

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

        private void OnAttackTimerCompleted()
        {
            if (_shouldForceAttack)
                _nextAttack = _forceAttack;

            _nextAttack = GetNextAttack();
        }

        private BossAttack GetNextAttack()
        {
            float totalWeight = 0;
            AttackDecision nextDecision = null;

            foreach (var decision in _attackDecisions)
            {
                totalWeight += decision.Weight;
            }

            var roll = Random.Range(0, totalWeight);

            foreach (var decision in _attackDecisions)
            {
                roll -= decision.Weight;

                if (roll <= 0)
                {
                    nextDecision = decision;
                    break;
                }
            }

            foreach (var decision in _attackDecisions)
            {
                decision.Modifier += (1 - decision.Modifier) * 0.5f;
            }

            nextDecision!.Modifier = 0;

            return nextDecision.Type;
        }

        private void Update()
        {
            _attackTimer.Tick(Time.deltaTime);
        }
    }
}

public enum BossAttack
{
    SmallSpreadShot = 0,
    LargeSpreadShot = 1,
    TeleportShot = 2,
    GroundExplode = 3,
}