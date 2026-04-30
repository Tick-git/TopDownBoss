using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] HealthData _healthData;
    
    private float _currentHealth;
    public event Action<float> HealthChanged;
    public event Action Died;

    public float MaxHealth => _healthData.MaxHealth;
    
    private void Awake()
    {
        _currentHealth = _healthData.MaxHealth;
    }

    public void ApplyDamage(float damage)
    {
        if (_currentHealth <= _healthData.MinHealth) return;
        
        _currentHealth -= damage;

        if (_currentHealth <= _healthData.MinHealth)
        {
            _currentHealth = _healthData.MinHealth;
            Died?.Invoke();
        }

        HealthChanged?.Invoke(_currentHealth);
    }
}