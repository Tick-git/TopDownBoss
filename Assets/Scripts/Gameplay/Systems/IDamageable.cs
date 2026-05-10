using UnityEngine;

public interface IDamageable
{
    public void ApplyDamage(DamageContext damageContext);
}

public struct DamageContext
{
    public readonly float Damage;
    public readonly Vector2 HitPoint;
    public readonly Vector2 HitNormal;

    public DamageContext(float damage, Vector2 hitPoint = default, Vector2 hitNormal = default)
    {
        Damage = damage;
        HitPoint = hitPoint;
        HitNormal = hitNormal;
    }
}