using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class VFXSystemTests
{
    private GameObject CreateVfxPrefab()
    {
        var go = new GameObject("VFX");
        go.AddComponent<ParticleSystem>();
        go.AddComponent<VFXSystem>();
        return go;
    }

    private ObjectPool<VFXSystem> CreatePool()
    {
        var prefab = CreateVfxPrefab();
        return new ObjectPool<VFXSystem>(prefab, 1, null);
    }

    [UnityTest]
    public IEnumerator VFXManager_Spawns_Correct_Type()
    {
        var managerGo = new GameObject("VFXManager");
        var manager = managerGo.AddComponent<VFXManager>();

        var prefab = CreateVfxPrefab();

        var data = ScriptableObject.CreateInstance<VFXData>();
        data.Initialize(VFXType.Smoke, prefab, 1);
        
        var library = ScriptableObject.CreateInstance<VFXLibrary>();
        
        library.Initialize(new List<VFXData>()
        {
            data
        });
        
        manager.SetVFXLibrary(library);
        manager.Initialize();

        var vfx = manager.SpawnVfx(VFXType.Smoke, Vector3.zero);

        Assert.IsNotNull(vfx);

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Returns_To_Pool_When_ParticleSystem_Stops()
    {
        var pool = CreatePool();

        var vfx = pool.Get();
        vfx.Play(Vector3.zero, Quaternion.identity);

        var ps = vfx.GetComponent<ParticleSystem>();

        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        yield return new WaitForSeconds(0.1f);

        var next = pool.Get();

        Assert.AreSame(vfx, next);
    }
}