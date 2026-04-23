using UnityEngine;

[CreateAssetMenu(menuName = "Data/Util/Pool")]
public class PoolData : ScriptableObject
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolSize;
    
    public GameObject Prefab => _prefab;

    public int PoolSize => _poolSize;
}