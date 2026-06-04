using System.Collections;
using Gameplay.Boss;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossPhaseController : MonoBehaviour
{
    [SerializeField] private BossPhaseAttacksData _data;
    [SerializeField] private ColorSetter _eyeSetter;
    [SerializeField] private ColorSetter _hornSetter;

    private Health _health;
    private AttackDecider _attackDecider;
    private BossAnimator _bossAnimator;

    private float _phaseTwoHealth;
    private float _phaseThreeHealth;
    private BossController _bossController;

    private int _currentPhase;

    public void Initialize(Health health, AttackDecider attackDecider, BossController bossController,
        BossAnimator bossAnimator)
    {
        _health = health;
        _attackDecider = attackDecider;
        _bossAnimator = bossAnimator;
        _bossController = bossController;
        ;

        _health.HealthChanged += OnHealthChanged;

        _phaseTwoHealth = _health.MaxHealth * 0.9f;
        _phaseThreeHealth = _health.MaxHealth * 0.7f;
        _currentPhase = 1;

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
        if (_currentPhase == 1 && health < _phaseTwoHealth)
        {
            StartCoroutine(TransitionToSecondPhase());
            _currentPhase = 2;
        }
        else if (_currentPhase == 2 && health < _phaseThreeHealth)
        {
            StartCoroutine(TransitionToThirdPhase());
            _currentPhase = 3;
        }
    }

    private IEnumerator TransitionToSecondPhase()
    {
        while (_attackDecider.IsAttacking)
            yield return null;

        _bossController.DisableBoss();
        
        _bossAnimator.PlayTransitionToThirdPhase();
        
        AdjustAttacks(_data.Phase2Attacks, AttackInterval.Normal);

        yield return new WaitForSeconds(_bossAnimator.GetThirdPhaseTransitionTime());
        _hornSetter.SetColor(Color.red);
        
        _bossController.EnableBoss();
    }
    
    private IEnumerator TransitionToThirdPhase()
    {
        while (_attackDecider.IsAttacking)
            yield return null;

        _bossController.DisableBoss();
        
        _bossAnimator.PlayTransitionToSecondPhase();
        
        AdjustAttacks(_data.Phase3Attacks, AttackInterval.Fast);

        yield return new WaitForSeconds(_bossAnimator.GetSecondPhaseTransitionTime());
        
        _eyeSetter.SetColor(Color.red);
        
        _bossController.EnableBoss();
    }

    private void AdjustAttacks(BossAttack[] attacks, AttackInterval interval)
    {
        foreach (var attack in attacks)
        {
            _attackDecider.AddAttack(attack);
        }

        _attackDecider.SetAttackInterval(interval);
    }
}

public enum BossPhase
{
    One,
    Two,
    Three
}