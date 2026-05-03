using UnityEngine;

public class Magazine : MonoBehaviour
{
    [SerializeField] private PoolData _bulletPoolData;
    
    private ObjectPool<Bullet> _bulletPool;
    private Transform _bulletParent;

    private void Awake()
    {
        _bulletParent = new GameObject("BulletPool").transform;
        _bulletPool = new ObjectPool<Bullet>(_bulletPoolData.Prefab, _bulletPoolData.PoolSize, _bulletParent);
    }

    public bool TryGetBullet(out Bullet bullet)
    {
        bullet = GetBullet();
        
        return true;
    }

    private void OnBulletHit(Bullet bullet)
    {
        bullet.Hit -= OnBulletHit;
        _bulletPool.Return(bullet);
    }
    
    public Bullet GetBullet()
    {
        var bullet = _bulletPool.Get();
        bullet.Hit += OnBulletHit;
        return bullet;
    }
}