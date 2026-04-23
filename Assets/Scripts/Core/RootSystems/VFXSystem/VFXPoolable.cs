using UnityEngine;

public class VFXSystem : MonoBehaviour, IPoolable<VFXSystem>, IPoolReturner<VFXSystem>
{
    private ObjectPool<VFXSystem> _vfxPool;
    private ParticleSystem _particleSystem;
    private bool _isReturned;

    public void Play(Vector3 position)
    {
        transform.position = position;
        _particleSystem.Play();
    }
    
    public void OnGetFromPool()
    {
        _isReturned = false;
        _particleSystem.Clear();
    }
    
    public void OnReturnToPool()
    {
        _isReturned = true;
        _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private void OnParticleSystemStopped()
    {
        if (_isReturned) return;
        
        _vfxPool.Return(this);
    }

    public void SetPool(ObjectPool<VFXSystem> pool)
    {
        _vfxPool = pool;
        _particleSystem = GetComponent<ParticleSystem>();

        if (_particleSystem == null)
            Debug.LogError($"{nameof(VFXSystem)} requires a ParticleSystem", this);
        
        var main = _particleSystem.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }
}