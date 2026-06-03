using System.Collections;
using Gameplay.Boss;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossPhaseController : MonoBehaviour
{
    [SerializeField] private BossPhaseAttacksData _data;
    [SerializeField] private EyeVisuals _eyeVisuals;
    
    private Health _health;
    private AttackDecider _attackDecider;
    private BossAnimator _bossAnimator;

    private float _phaseSwitchHealth;
    private BossController _bossController;

    public void Initialize(Health health, AttackDecider attackDecider, BossController bossController, BossAnimator bossAnimator)
    {
        _health = health;
        _attackDecider = attackDecider;
        _bossAnimator = bossAnimator;
        _bossController = bossController;;
        
        _health.HealthChanged += OnHealthChanged;

        _phaseSwitchHealth = _health.MaxHealth * 0.99f;

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
            StartCoroutine(TransitionToSecondPhase());
            _health.HealthChanged -= OnHealthChanged;
        }
    }

    private IEnumerator TransitionToSecondPhase()
    {
        while(_attackDecider.IsAttacking)
            yield return null;
        
        _bossController.DisableBoss();
        
        _bossAnimator.PlayPhaseTransition();
        
        foreach (var attack in _data.Phase2Attacks)
        {
            _attackDecider.AddAttack(attack);
        }

        yield return new WaitForSeconds(_bossAnimator.GetPhaseTransitionTime());

        _eyeVisuals.SetEyeColorToRed();
        
        _bossController.EnableBoss();
    }
}