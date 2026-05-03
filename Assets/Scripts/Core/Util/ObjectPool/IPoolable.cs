using UnityEngine;

public interface IPoolable<T> where T : MonoBehaviour, IPoolable<T>
{
    public void OnReturnToPool();
    public void OnGetFromPool();
}