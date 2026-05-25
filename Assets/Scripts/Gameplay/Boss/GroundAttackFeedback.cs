using System;
using UnityEngine;

public class GroundAttackFeedback : MonoBehaviour
{
    [SerializeField] private GroundAttackVFX _groundAttackVFX;
    [SerializeField] private BossSceneReferences _sceneReferences;

    private CameraShake CameraShake => _sceneReferences.CameraShake;


    public void PlayHandFire()
    {
        _groundAttackVFX.PlayHandFireVFX();
    }

    public void PlayGroundImpact()
    {
        _groundAttackVFX.StopHandFireVFX();
        _groundAttackVFX.PlayGroundImpact();

        CameraShake.Shake(4, 0.1f, 0.3f);
    }
}