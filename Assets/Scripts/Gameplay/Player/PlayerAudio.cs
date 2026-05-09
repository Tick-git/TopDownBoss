using System;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerAudio : MonoBehaviour
    {
        [SerializeField] private AudioData _shotSound;
        [SerializeField] private AudioData _rollSound;
        [SerializeField] private AudioData _stepSound;

        private AudioManager _audioManager;

        private void Awake()
        {
            _audioManager = AudioManager.Instance;
        }

        public void PlayShootSfx() => Play(_shotSound);

        public void PlayRollSfx() => Play(_rollSound);

        public void PlayStepSfx() => Play(_stepSound);

        private void Play(AudioData data)
        {
            if (_audioManager == null || data == null) return;

            _audioManager.PlaySfx(_shotSound);
        }
    }
}