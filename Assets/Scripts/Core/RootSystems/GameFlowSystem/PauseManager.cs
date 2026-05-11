using System;
using UnityEngine;

public class PauseManager : IDisposable
{
    public event Action<bool> PauseStateChanged;

    public bool IsPaused { get; private set; }

    public void Pause()
    {
        Time.timeScale = 0f;
        IsPaused = true;
        PauseStateChanged?.Invoke(IsPaused);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        IsPaused = false;
        PauseStateChanged?.Invoke(IsPaused);
    }

    public void Dispose()
    {
        if (IsPaused)
            Resume();
    }
}