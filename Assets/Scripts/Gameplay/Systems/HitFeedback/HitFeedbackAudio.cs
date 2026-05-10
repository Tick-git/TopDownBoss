using UnityEngine;

public class HitFeedbackAudio : HitFeedback
{
    [SerializeField] private AudioData _hitAudioData;

    AudioManager _audioManager;

    private void Start()
    {
        _audioManager = AudioManager.Instance;
    }

    protected override void OnHit(DamageContext damageContext)
    {
        if (_audioManager == null) return;

        _audioManager.PlaySfx(_hitAudioData);
    }
}