using System;
using UnityEngine;

public class GroundAttackFeedback : MonoBehaviour
{
    [SerializeField] private GroundAttackAudio _groundAttackAudio;
    [SerializeField] private GroundAttackVFX _groundAttackVFX;
    [SerializeField] private BossSceneReferences _sceneReferences;

    private CameraShake CameraShake => _sceneReferences.CameraShake;


    public void PlayHandFire()
    {
        _groundAttackVFX.PlayHandFireVFX();
        _groundAttackAudio.PlayHandFire();
    }

    public void PlayGroundImpact()
    {
        _groundAttackVFX.StopHandFireVFX();
        _groundAttackVFX.PlayGroundImpact();
        _groundAttackAudio.PlayGroundImpact();

        CameraShake.Shake(4, 0.1f, 0.3f);
    }
}