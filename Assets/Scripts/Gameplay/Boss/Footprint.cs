using System.Collections;
using UnityEngine;

public class Footprint : MonoBehaviour, IPoolable<Footprint>, IPoolReturner<Footprint>
{
    [SerializeField] private int _secondsUntilFadeout = 10;
    [SerializeField] private float _fadeOutTime = 2;

    private float _visibleAlpha;
    private float _invisibleAlpha;
    
    private SpriteRenderer _spriteRenderer;
    private ObjectPool<Footprint> _pool;
    private Coroutine _lifecycleRoutine;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _visibleAlpha  = _spriteRenderer.color.a;
        _invisibleAlpha = 0;
    }

    public void Enable()
    {
        if (_lifecycleRoutine != null)
            StopCoroutine(_lifecycleRoutine);
        
        _lifecycleRoutine = StartCoroutine(HandleLifecycle());
    }

    private IEnumerator HandleLifecycle()
    {
        SetAlpha(_visibleAlpha);
        
        yield return new WaitForSeconds(_secondsUntilFadeout);

        var t = 0f;
        
        while (t < 1)
        {
            SetAlpha(Mathf.Lerp(_visibleAlpha, _invisibleAlpha, t));
            
            t += Time.deltaTime / _fadeOutTime;
            
            yield return null;
        }

        SetAlpha(0);
        
        _pool.Return(this);
    }

    private void SetAlpha(float alpha)
    {
        var color = _spriteRenderer.color;
        
        color.a = alpha;
        
        _spriteRenderer.color = color;
    }
    
    public void OnGetFromPool()
    {
    }
    
    public void OnReturnToPool()
    {
    }

    public void SetPool(ObjectPool<Footprint> pool)
    {
        _pool = pool;
    }
}