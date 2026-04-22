using System;
using System.Collections;
using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ObjectPoolTests
{
    private ObjectPool<TestPoolable> CreatePool(int size = 1)
    {
        var prefab = CreatePoolable();

        return new ObjectPool<TestPoolable>(prefab, size, null);
    }

    private GameObject CreatePoolable()
    {
        var prefab = new GameObject("Test");
        prefab.AddComponent<TestPoolable>();
        return prefab;
    }

    [UnityTest]
    public IEnumerator Get_Return_Get_Reuses_Instance()
    {
        var pool = CreatePool();

        var first = pool.Get();
        pool.Return(first);

        var second = pool.Get();

        Assert.AreSame(first, second);

        yield return null;
    }

    [UnityTest]
    public IEnumerator Calls_Lifecycle_Methods()
    {
        var pool = CreatePool();

        var obj = pool.Get();
        Assert.IsTrue(obj.Got);

        pool.Return(obj);
        Assert.IsTrue(obj.Returned);

        yield return null;
    }

    [UnityTest]
    public IEnumerator Double_Return_Does_Not_Duplicate_Instance()
    {
        var pool = CreatePool();

        var obj = pool.Get();

        pool.Return(obj);
        
        LogAssert.Expect(LogType.Warning, new Regex(".*"));
        pool.Return(obj);

        Assert.AreEqual(1, pool.InactiveSize);

        var first = pool.Get();
        var second = pool.Get();

        Assert.AreNotSame(first, second);

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Create_Pool_Without_Correctly_Configured_Poolable()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            var objectPool = new ObjectPool<TestPoolable>(null, 1, null);
        });
        
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var objectPool = new ObjectPool<TestPoolable>(CreatePoolable(), -1, null);
        });
        
        Assert.Throws<InvalidOperationException>(() =>
        {
            var objectPool = new ObjectPool<TestPoolable>(new GameObject(), 1, null);
        });
        
        yield return null;
    }
    
    private class TestPoolable : MonoBehaviour, IPoolable<TestPoolable>
    {
        public bool Got { get; private set; }
        public bool Returned { get; private set; }

        public void Initialize(ObjectPool<TestPoolable> pool) { }

        public void OnGetFromPool()
        {
            Got = true;
        }

        public void OnReturnToPool()
        {
            Returned = true;
        }
    }
}