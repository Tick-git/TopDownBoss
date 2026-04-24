using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    private readonly float _maxHealth = 100f;
    private readonly float _minHealth = 0f;
    
    private float _currentHealth;
    
    public event Action<float> HealthChanged;
    public event Action Died;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        if (_currentHealth <= _minHealth) return;
        
        _currentHealth -= damage;

        if (_currentHealth <= _minHealth)
        {
            _currentHealth = _minHealth;
            Died?.Invoke();
        }

        HealthChanged?.Invoke(_currentHealth);
    }
}