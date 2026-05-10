using UnityEngine;

public interface IDamageable
{
    public void ApplyDamage(DamageContext damageContext);
}

public struct DamageContext
{
    public readonly float Damage;
    public readonly Vector2 HitPoint;

    public DamageContext(float damage, Vector2 hitPoint = default)
    {
        Damage = damage;
        HitPoint = hitPoint;
    }
}