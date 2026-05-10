using System;
using UnityEngine;

public class HitFeedbackVFX : HitFeedback
{
    [SerializeField] private Vector2 _vfxScale = Vector2.one;
    
    private VFXManager _vfxManager;

    private void Start()
    {
        _vfxManager = VFXManager.Instance;
    }

    protected override void OnHit(DamageContext damageContext)
    {
        if (_vfxManager == null) return;

        var zRotation = Mathf.Atan2(damageContext.FeedbackDirection.y, damageContext.FeedbackDirection.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.Euler(0, 0, zRotation);

        var spawnParams = new VFXSpawnParams(damageContext.HitPoint, rotation, _vfxScale, damageContext.HitTransform);
        
        _vfxManager.SpawnVfx(VFXType.BloodSplash, spawnParams);
    }
}
