using System;

public class Timer
{
    public float Duration { get; private set; }
    public float Elapsed { get; private set; }
    public bool IsRunning { get; private set; }

    public event Action Completed;

    public Timer(float duration)
    {
        Duration = duration;
    }

    public void Start()
    {
        IsRunning = true;
    }

    public void Stop()
    {
        IsRunning = false;
    }

    public void Reset(float? newDuration = null)
    {
        if (newDuration.HasValue)
        {
            Duration = newDuration.Value;
        }

        Elapsed = 0f;
    }

    public void Tick(float deltaTime)
    {
        if (!IsRunning) return;

        Elapsed += deltaTime;

        if (Elapsed >= Duration)
        {
            Elapsed = Duration;
            IsRunning = false;
            Completed?.Invoke();
        }
    }
}