using UnityEngine;

public interface IPoolable<T> where T : MonoBehaviour,  IPoolable<T>
{
    public void Initialize(ObjectPool<T> pool);
    public void OnReturnToPool();
    public void OnGetFromPool();
}