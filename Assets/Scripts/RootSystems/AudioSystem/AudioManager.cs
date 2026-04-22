using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>, IAudioService
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer _mainMixer;
    [SerializeField] private AudioMixerGroup _uiMixerGroup;
    [SerializeField] private AudioMixerGroup _sfxMixerGroup;
    [SerializeField] private AudioMixerGroup _musicMixerGroup;

    [Header("Music Sources")]
    [SerializeField] private AudioSource _musicSource1;
    [SerializeField] private AudioSource _musicSource2;
    [SerializeField] private float _musicCrossfadeDuration = 1.5f;

    [Header("SFX Pool")]
    [SerializeField] private int _initialPoolSize = 10;
    [SerializeField] private GameObject _audioPoolablePrefab;
    private ObjectPool<AudioPoolable> _sfxPool;
    
    [Header("Audio Library")]
    [SerializeField] private AudioLibrary _audioLibrary;
    public AudioLibrary Library => _audioLibrary;

    private bool _isMusicSource1Active = true;
    
    protected override void Awake()
    {
       base.Awake();
        _sfxPool = new ObjectPool<AudioPoolable>(_audioPoolablePrefab, _initialPoolSize, transform);
    }
    
    #region VolumeAPI

    public float GetMasterVolume() => GetVolume(AudioMixerParams.MasterVolume);
    public float GetMusicVolume() => GetVolume(AudioMixerParams.MusicVolume);
    public float GetSfxVolume() => GetVolume(AudioMixerParams.SfxVolume);
    public float GetUIVolume() => GetVolume(AudioMixerParams.UIVolume);
    public void SetMasterVolume(float percent) => SetVolume(AudioMixerParams.MasterVolume, percent);
    public void SetMusicVolume(float percent) => SetVolume(AudioMixerParams.MusicVolume, percent);
    public void SetSfxVolume(float percent) => SetVolume(AudioMixerParams.SfxVolume, percent);
    public void SetUIVolume(float percent) => SetVolume(AudioMixerParams.UIVolume, percent);

    #endregion
    
    private void PlaySound(AudioData audio)
    {
        if (audio == null || audio.GetClip() == null || audio.MixerGroup == null)
        {
            Debug.LogWarning($"{typeof(AudioPoolable)} requires sound data, clip and mixer group to play");
            return;
        }

        AudioPoolable audioPoolable = _sfxPool.Get();
        audioPoolable.Play(audio);
    }
    
    public void PlayMusic(AudioData audio)
    {
        if (audio == null || audio.GetClip() == null || audio.MixerGroup == null)
        {
            Debug.LogWarning($"Playing music requires sound data, clip and mixer group to play");
            return;
        }

        AudioSource activeSource = _isMusicSource1Active ? _musicSource1 : _musicSource2;
        AudioSource newSource = _isMusicSource1Active ? _musicSource2 : _musicSource1;

        newSource.clip = audio.GetClip();
        newSource.volume = 0;
        newSource.outputAudioMixerGroup = audio.MixerGroup;
        newSource.loop = true;

        newSource.Play();

        StartCoroutine(CrossfadeMusic(activeSource, newSource, audio.Volume));
        _isMusicSource1Active = !_isMusicSource1Active;
    }

    private IEnumerator CrossfadeMusic(AudioSource oldSource, AudioSource newSource, float targetVolume)
    {
        float timeElapsed = 0;
        newSource.volume = 0;
        float oldSourceStartVolume = oldSource.volume;

        while (timeElapsed < _musicCrossfadeDuration)
        {
            newSource.volume = Mathf.Lerp(0, targetVolume, timeElapsed / _musicCrossfadeDuration);
            oldSource.volume = Mathf.Lerp(oldSourceStartVolume, 0, timeElapsed / _musicCrossfadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        newSource.volume = targetVolume;
        oldSource.volume = 0;
        oldSource.Stop();
    }
    
    private void SetVolume(string exposedParam, float linearValue)
    {
        float dB = linearValue > 0.0001f ? Mathf.Log10(linearValue) * 20f : -80f;
        _mainMixer.SetFloat(exposedParam, dB);
    }

    private float GetVolume(string exposedParam)
    {
        if (_mainMixer.GetFloat(exposedParam, out float dB))
        {
            float linear = Mathf.Pow(10f, dB / 20f);

            return Mathf.Clamp01(linear);
        }

        Debug.LogWarning($"Parameter not found: {exposedParam}");
        return 1f;
    }

    public void PlayUI(AudioData data)
    {
        PlaySound(data);
    }

    public void PlaySfx(AudioData data)
    {
        PlaySound(data);
    }
}