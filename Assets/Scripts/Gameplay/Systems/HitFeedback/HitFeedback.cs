using UnityEngine;

public abstract class HitFeedback : MonoBehaviour
{
    [SerializeField] Health _health;

    private void OnEnable()
    {
        _health.Hit += OnHit;
    }

    private void OnDisable()
    {
        _health.Hit -= OnHit;
    }

    protected abstract void OnHit(DamageContext damageContext);
}