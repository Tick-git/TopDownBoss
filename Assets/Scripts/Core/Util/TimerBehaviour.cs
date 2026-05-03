using System;
using UnityEngine;

namespace Core.Util
{
    public class TimerBehaviour : MonoBehaviour
    {
        [SerializeField] private float _duration = 1f;
        [SerializeField] private bool _playOnAwake = true;

        private Timer _timer;

        public event Action Completed;

        private void Awake()
        {
            _timer = new Timer(_duration);
            _timer.Completed += OnTimerComplete;
        }

        private void OnDestroy()
        {
            _timer.Completed -= OnTimerComplete;
        }
        
        private void OnTimerComplete()
        {
            Completed?.Invoke();
        }

        private void Update()
        {
            _timer.Tick(Time.deltaTime);
        }

        public void StartTimer(float? newDuration = null)
        {
            _timer.Reset(newDuration, true);
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

    }
}