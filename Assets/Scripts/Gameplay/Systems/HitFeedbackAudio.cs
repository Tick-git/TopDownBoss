using UnityEngine;

public class HitFeedbackAudio : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private AudioData _hitAudioData;

    AudioManager _audioManager;

    private void Start()
    {
        _audioManager = AudioManager.Instance;
    }

    private void OnEnable()
    {
        _health.Hit += OnHit;
    }

    private void OnDisable()
    {
        _health.Hit -= OnHit;
    }

    private void OnHit(DamageContext damageContext)
    {
        if (_audioManager == null) return;

        _audioManager.PlaySfx(_hitAudioData);
    }
}