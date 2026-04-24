using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = DataPaths.CoreData.AudioPath + "AudioData")]
public class AudioData : ScriptableObject
{
    [SerializeField] private AudioClip[] _clips;
    
    [Range(0f, 1f)] 
    [SerializeField] private float _volume = 1f;
    
    [Range(0.1f, 3f)]
    [SerializeField] private float _pitch = 1f;
    
    [SerializeField] private AudioMixerGroup _mixerGroup;

    public float Volume => _volume;
    public float Pitch => _pitch;
    public AudioMixerGroup MixerGroup => _mixerGroup;
    
    public AudioClip GetClip()
    {
        if (_clips == null || _clips.Length == 0) return null;
        return _clips[Random.Range(0, _clips.Length)];
    }
    
    public void Initialize(AudioClip[] clips, float volume, float pitch, AudioMixerGroup mixerGroup)
    {
        _clips = clips;
        _volume = volume;
        _pitch = pitch;
        _mixerGroup = mixerGroup;
    }
}