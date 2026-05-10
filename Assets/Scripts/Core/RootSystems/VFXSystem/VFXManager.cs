using System.Collections.Generic;
using UnityEngine;

public class VFXManager : Singleton<VFXManager>
{
    [SerializeField] private VFXLibrary _vfxLibrary;

    private readonly Dictionary<VFXType, ObjectPool<VFXSystem>> _vfxPools = new();

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    public void Initialize()
    {
        InstantiatePools();
    }

    public VFXSystem SpawnVfx(VFXType type, Vector3 position)
    {
        VFXSystem vfxSystem = _vfxPools[type].Get();
        vfxSystem.Play(position);

        return vfxSystem;
    }

    private void InstantiatePools()
    {
        foreach (var data in _vfxLibrary.VFXDataCollection)
        {
            _vfxPools[data.VFXType] = new ObjectPool<VFXSystem>(data.VFXPrefab, data.PoolSize, transform);
        }
    }

    public void SetVFXLibrary(VFXLibrary library)
    {
        _vfxLibrary = library;
    }
}

public enum VFXType
{
    Smoke,
    Fire,
    Explosion,
    ShortCircuit,
    BloodSplash
}