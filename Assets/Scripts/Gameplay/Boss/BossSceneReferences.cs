using System;
using UnityEngine;

public class BossSceneReferences : MonoBehaviour
{
    [SerializeField] private CameraShake _cameraShake;
    [SerializeField] private GameObject _player;

    private void Awake()
    {
        if (_cameraShake == null)
            Debug.LogError($"BossSceneReferences on gameObject {gameObject.name}: _cameraShake is null");

        if (_player == null)
            Debug.LogError($"BossSceneReferences on gameObject {gameObject.name}: _player is null");
    }

    public CameraShake CameraShake => _cameraShake;
    public GameObject Player => _player;
}