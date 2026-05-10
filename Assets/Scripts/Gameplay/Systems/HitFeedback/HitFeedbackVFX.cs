using System;

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
        
        VFXManager.Instance.SpawnVfx(VFXType.BloodSplash, damageContext.HitPoint);
    }
}
