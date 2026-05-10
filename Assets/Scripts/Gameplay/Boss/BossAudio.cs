using System;
using UnityEngine;

public class BossAudio : MonoBehaviour
{
    [SerializeField] private BossAnimator _animator;

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
        _animator.FootGrounded += PlayWalkSound;
    }

    private void OnDisable()
    {
        _animator.FootGrounded -= PlayWalkSound;
    }

    private void PlayWalkSound() => Play(_walkData);

    public void PlayShootSound() => Play(_shootData);


    private void Play(AudioData data)
    {
        if (_audioManager == null || data == null) return;

        _audioManager.PlaySfx(data);
    }
}