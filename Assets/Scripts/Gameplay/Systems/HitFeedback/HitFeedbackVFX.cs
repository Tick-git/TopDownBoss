using System;
using UnityEngine;

public class HitFeedbackVFX : HitFeedback
{
    private VFXManager _vfxManager;

    private void Start()
    {
        _vfxManager = VFXManager.Instance;
    }

    protected override void OnHit(DamageContext damageContext)
    {
        if (_vfxManager == null) return;

        var zRotation = Mathf.Atan2(damageContext.HitNormal.y, damageContext.HitNormal.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.Euler(0, 0, zRotation);
        
        VFXManager.Instance.SpawnVfx(VFXType.BloodSplash, damageContext.HitPoint, rotation);
    }
}
