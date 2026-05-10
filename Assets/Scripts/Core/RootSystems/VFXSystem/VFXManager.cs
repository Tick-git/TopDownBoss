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

    public VFXSystem SpawnVfx(VFXType type, VFXSpawnParams vfxSpawnParams)
    {
        VFXSystem vfxSystem = _vfxPools[type].Get();
        vfxSystem.Play(vfxSpawnParams);

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

public struct VFXSpawnParams
{
    public readonly Vector3? Position;
    public readonly Quaternion? Rotation;
    public readonly Transform Parent;
    public readonly Vector3? Scale;

    public VFXSpawnParams(Vector3? position = null, Quaternion? rotation = null, Vector3? scale = null, Transform parent = null)
    {
        Scale = scale;
        Position = position;
        Rotation = rotation;
        Parent = parent;
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