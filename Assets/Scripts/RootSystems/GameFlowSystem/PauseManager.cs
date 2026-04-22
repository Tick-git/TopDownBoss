using System;
using UnityEngine;

public class PauseManager
{
    public event Action<bool> OnPauseStateChanged;

    public bool IsPaused { get; private set; }
    
    public void Pause()
    {
        Time.timeScale = 0f;
        IsPaused = true;
        OnPauseStateChanged?.Invoke(IsPaused);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        IsPaused = false;
        OnPauseStateChanged?.Invoke(IsPaused);
    }
}
