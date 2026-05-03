using System;
using UnityEngine;

public class SettingsManager : IDisposable
{
    public Setting<float> MasterVolume { get; } = new();
    public Setting<float> MusicVolume { get; } = new();
    public Setting<float> SfxVolume { get; } = new();
    public Setting<float> UIVolume { get; } = new();

    private readonly AudioManager _audioManager;

    public SettingsManager(AudioManager audioManager)
    {
        MasterVolume.ValueChanged += audioManager.SetMasterVolume;
        MusicVolume.ValueChanged += audioManager.SetMusicVolume;
        SfxVolume.ValueChanged += audioManager.SetSfxVolume;
        UIVolume.ValueChanged += audioManager.SetUIVolume;

        LoadPlayerPrefs();

        _audioManager = audioManager;
    }

    public void Dispose()
    {
        MasterVolume.ValueChanged -= _audioManager.SetMasterVolume;
        MusicVolume.ValueChanged -= _audioManager.SetMusicVolume;
        SfxVolume.ValueChanged -= _audioManager.SetSfxVolume;
        UIVolume.ValueChanged -= _audioManager.SetUIVolume;
    }

    private void LoadPlayerPrefs()
    {
        MasterVolume.Value = PlayerPrefs.GetFloat("Master", 1f);
        MusicVolume.Value = PlayerPrefs.GetFloat("Music", 0.8f);
        SfxVolume.Value = PlayerPrefs.GetFloat("Sfx", 0.8f);
        UIVolume.Value = PlayerPrefs.GetFloat("UI", 0.8f);
    }

    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetFloat("Master", MasterVolume.Value);
        PlayerPrefs.SetFloat("Music", MusicVolume.Value);
        PlayerPrefs.SetFloat("Sfx", SfxVolume.Value);
        PlayerPrefs.SetFloat("UI", UIVolume.Value);
        PlayerPrefs.Save();
    }
}