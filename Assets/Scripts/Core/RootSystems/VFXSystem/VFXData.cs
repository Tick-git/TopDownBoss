using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.CoreData.Vfx + "Vfx Data")]
public class VFXData : ScriptableObject
{
    [SerializeField] private GameObject _vfxPrefab;
    [SerializeField] private VFXType _vfxType;
    [SerializeField] private int _poolSize;
    
    public GameObject VFXPrefab => _vfxPrefab;
    public VFXType VFXType => _vfxType;
    public int PoolSize => _poolSize;
    
    public void Initialize(VFXType type, GameObject prefab, int poolSize)
    {
        _vfxType = type;
        _vfxPrefab = prefab;
        _poolSize = poolSize;
    }
}