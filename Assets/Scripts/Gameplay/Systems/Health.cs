using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    private readonly float _maxHealth = 100f;
    private float _currentHealth;
    
    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            OnDeath?.Invoke();
        else
            OnHealthChanged?.Invoke(_currentHealth);
    }
}