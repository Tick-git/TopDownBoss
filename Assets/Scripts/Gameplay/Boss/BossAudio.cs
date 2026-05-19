using System;
using UnityEngine;

public class BossAudio : MonoBehaviour
{
    [SerializeField] private BossAnimatorOld _animatorOld;

    [Header("Audio Data")] [SerializeField]
    private AudioData _walkData;

    [SerializeField] private AudioData _shootData;

    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = AudioManager.Instance;
    }

    private void OnEnable()
    {
        _animatorOld.FootGrounded += PlayWalkSound;
    }

    private void OnDisable()
    {
        _animatorOld.FootGrounded -= PlayWalkSound;
    }

    private void PlayWalkSound() => Play(_walkData);

    public void PlayShootSound() => Play(_shootData);


    private void Play(AudioData data)
    {
        if (_audioManager == null || data == null) return;

        _audioManager.PlaySfx(data);
    }
}