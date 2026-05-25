using UnityEngine;

public class GroundAttackAudio : MonoBehaviour
{
    [SerializeField] private AudioData _groundImpact;

    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = AudioManager.Instance;
    }

    public void PlayGroundImpact() => Play(_groundImpact);

    private void Play(AudioData audioData)
    {
        if (_audioManager == null) return;

        _audioManager.PlaySfx(audioData);
    }
}