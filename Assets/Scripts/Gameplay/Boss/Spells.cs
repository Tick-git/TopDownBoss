using UnityEngine;

public class Spells : MonoBehaviour
{
    [SerializeField] private PoolData _groundExplodeSpellPoolData;

    private ObjectPool<GroundExplodeSpell> _groundExplodeSpellPool;

    private void Awake()
    {
        _groundExplodeSpellPool = new ObjectPool<GroundExplodeSpell>(
            _groundExplodeSpellPoolData.Prefab,
            _groundExplodeSpellPoolData.PoolSize, 
            transform);
    }

    public GroundExplodeSpell GetGroundExplodeSpell()
    {
        return _groundExplodeSpellPool.Get();
    }
}