using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectPool<T> where T : MonoBehaviour, IPoolable<T>
{
    private readonly Queue<T> _pool = new();
    private readonly HashSet<T> _active = new();
    private readonly GameObject _prefab;
    private readonly Transform _parent;

    public int InactiveSize => _pool.Count;

    public ObjectPool(GameObject prefab, int size, Transform parent)
    {
        if (prefab == null)
            throw new ArgumentNullException($"{typeof(ObjectPool<T>)} is missing Prefab");

        if (!prefab.TryGetComponent<T>(out _))
            throw new InvalidOperationException($"Prefab {prefab.name} is missing component {typeof(T).Name}");

        if (size < 0)
            throw new ArgumentOutOfRangeException($"{typeof(ObjectPool<T>)} size is less than 0");

        _prefab = prefab;
        _parent = parent;

        for (int i = 0; i < size; i++)
        {
            _pool.Enqueue(Create());
        }
    }

    public T Get()
    {
        if (!_pool.TryDequeue(out T pooledBehaviour))
        {
            pooledBehaviour = Create();
        }

        _active.Add(pooledBehaviour);
        pooledBehaviour.OnGetFromPool();
        pooledBehaviour.gameObject.SetActive(true);

        return pooledBehaviour;
    }

    public void Return(T pooledBehaviour)
    {
        if (!_active.Remove(pooledBehaviour))
        {
            Debug.LogWarning("Trying to return object that is not active or already returned", pooledBehaviour);
            return;
        }

        pooledBehaviour.OnReturnToPool();
        pooledBehaviour.gameObject.SetActive(false);

        _pool.Enqueue(pooledBehaviour);
    }

    private T Create()
    {
        var obj = Object.Instantiate(_prefab, _parent, true);

        T pooledBehaviour = obj.GetComponent<T>();

        if (pooledBehaviour is IPoolReturner<T> returner)
            returner.SetPool(this);

        obj.gameObject.SetActive(false);

        return pooledBehaviour;
    }
}