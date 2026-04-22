using UnityEngine;

public class AssaultRifle : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] Transform _weaponCenter;
    
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
}
