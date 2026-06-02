using Gameplay.Boss;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossPhaseController : MonoBehaviour
{
    [SerializeField] private BossPhaseAttacksData _data;
    
    private Health _health;
    private AttackDecider _attackDecider;

    private float _phaseSwitchHealth;
    
    public void Initialize(Health health, AttackDecider attackDecider)
    {
        _health = health;
        _health.HealthChanged += OnHealthChanged;
        
        _attackDecider = attackDecider;
        _phaseSwitchHealth = _health.MaxHealth * Random.Range(0.5f, 0.6f);
        
        foreach (var attack in _data.Phase1Attacks)
        {
            _attackDecider.AddAttack(attack);
        }
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float health)
    {
        if (health < _phaseSwitchHealth)
        {
            EnterSecondPhase();
        }
        
        _health.HealthChanged -= OnHealthChanged;
    }

    private void EnterSecondPhase()
    {
        foreach (var attack in _data.Phase2Attacks)
        {
            _attackDecider.AddAttack(attack);
        }
    }
}
