using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerAudio : MonoBehaviour
    {
        [SerializeField] private PlayerAnimator _animator;
        
        [Header("Audio Data")]
        [SerializeField] private AudioData _shotSound;
        [SerializeField] private AudioData _rollSound;
        [SerializeField] private AudioData _stepSound;

        private AudioManager _audioManager;

        private void Awake()
        {
            _audioManager = AudioManager.Instance;
        }

        private void OnEnable()
        {
            _animator.FootOnGround += PlayStepSfx;
        }

        private void OnDisable()
        {
            _animator.FootOnGround -= PlayStepSfx;
        }

        public void PlayShootSfx() => Play(_shotSound);

        public void PlayRollSfx() => Play(_rollSound);

        public void PlayStepSfx() => Play(_stepSound);

        private void Play(AudioData data)
        {
            if (_audioManager == null || data == null) return;

            _audioManager.PlaySfx(data);
        }
    }
}