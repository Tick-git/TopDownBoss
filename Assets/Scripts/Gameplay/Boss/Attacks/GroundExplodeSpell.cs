using System.Collections;
using UnityEngine;

public class GroundExplodeSpell : MonoBehaviour, IPoolable<GroundExplodeSpell>, IPoolReturner<GroundExplodeSpell>
{
    [SerializeField] GroundExplodeSpellData _data;
    
    private bool _canDamage;

    private Collider2D _collider2D;
    private ParticleSystem _groundExplodeParticles;
    private Coroutine _delayRoutine;
    private ObjectPool<GroundExplodeSpell> _pool;

    private readonly Vector2 _pooledPosition = Vector2.right * 100f;

    private void Awake()
    {
        _groundExplodeParticles = GetComponent<ParticleSystem>();
        _collider2D = GetComponent<Collider2D>();
        
    }

    public void Cast(Vector2 position)
    {
        if (_delayRoutine != null)
            StopCoroutine(_delayRoutine);
        
        _delayRoutine = StartCoroutine(DelayedCastRoutine(position));
    }

    private IEnumerator DelayedCastRoutine(Vector2 position)
    {
        yield return new WaitForSeconds(_data.CastDelayTime);
        
        SetSpellInteraction(true);
        
        transform.position = position;
        _groundExplodeParticles.Play();

        yield return new WaitForSeconds(_data.SpellActiveTime);
        
        SetSpellInteraction(false);        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_canDamage) return;

        if (other.TryGetComponent(out IDamageable damageable))
        {
            var damageContext = new DamageContext(_data.Damage);
            
            damageable.ApplyDamage(damageContext);

            SetSpellInteraction(false);
        }
    }

    private void OnParticleSystemStopped()
    {
        SetSpellInteraction(false);
        
        _groundExplodeParticles.Stop();
        _groundExplodeParticles.Clear(true);
        
        _pool.Return(this);
    }

    private void SetSpellInteraction(bool value)
    {
        _canDamage = value;
        _collider2D.enabled = value;
    }

    public void OnReturnToPool()
    {
        transform.position = _pooledPosition;
    }

    public void OnGetFromPool()
    {
        SetSpellInteraction(false);
    }

    public void SetPool(ObjectPool<GroundExplodeSpell> pool)
    {
        _pool = pool;
    }
}