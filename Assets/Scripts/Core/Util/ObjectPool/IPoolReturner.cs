using UnityEngine;

public interface IPoolReturner<T> where T : MonoBehaviour, IPoolable<T>
{
    public void SetPool(ObjectPool<T> pool);
}