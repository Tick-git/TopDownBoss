using UnityEngine;

public class AssaultRifle : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private Transform _weaponCenter;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Magazine _magazine;
    
    private SpriteRenderer _weaponSpriteRenderer;
    private Vector2 CenterPosition => _weaponCenter.position;
    
    private void Awake()
    {
        _weaponSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ApplyAim(Vector2 target)
    {
        Vector2 aimDirection = (target - CenterPosition).normalized;
        
        bool aimingLeft = aimDirection.x < 0;
        
        transform.position = CenterPosition + aimDirection * _radius;
        transform.right = aimDirection;
        
        _weaponSpriteRenderer.flipY = aimingLeft;
    }

    public void Shoot()
    {
        _magazine.TryGetBullet(out Bullet bullet);
        
        bullet.transform.position = _firePoint.position;
        bullet.StartFlight(transform.right);
    }
}