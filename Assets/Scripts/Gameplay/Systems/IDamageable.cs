using UnityEngine;

public interface IDamageable
{
    public void ApplyDamage(DamageContext damageContext);
}

public struct DamageContext
{
    public readonly float Damage;
    public readonly Vector2 HitPoint;
    public readonly Vector2 FeedbackDirection;
    public readonly Transform HitTransform;

    public DamageContext(float damage, Vector2 hitPoint = default, Vector2 feedbackDirection = default,
        Transform hitTransform = null)
    {
        Damage = damage;
        HitPoint = hitPoint;
        FeedbackDirection = feedbackDirection;
        HitTransform = hitTransform;
    }
}