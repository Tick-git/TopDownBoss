using UnityEngine;

public interface IDamageable
{
    public void ApplyDamage(DamageContext damageContext);
}

public struct DamageContext
{
    public readonly float Damage;

    public DamageContext(float damage)
    {
        Damage = damage;
    }
}