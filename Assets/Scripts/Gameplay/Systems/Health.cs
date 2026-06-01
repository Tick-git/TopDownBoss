using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] HealthData _healthData;

    private bool _vulnerable;
    private float _currentHealth;
    public event Action<float> HealthChanged;
    public event Action Died;
    public event Action<DamageContext> Hit;

    public float MaxHealth => _healthData.MaxHealth;

    private void Awake()
    {
        _currentHealth = _healthData.MaxHealth;
        _vulnerable = true;
    }

    public void ApplyDamage(DamageContext damageContext)
    {
        if (!_vulnerable) return;

        if (_currentHealth <= _healthData.MinHealth) return;

        _currentHealth -= damageContext.Damage;

        if (_currentHealth <= _healthData.MinHealth)
        {
            _currentHealth = _healthData.MinHealth;
            Died?.Invoke();
        }

        HealthChanged?.Invoke(_currentHealth);
        Hit?.Invoke(damageContext);
    }

    public void SetVulnerability(bool vulnerable)
    {
        _vulnerable = vulnerable;
    }
}