using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = DataPaths.CoreData.AudioPath + "AudioData")]
public class AudioData : ScriptableObject
{
    [Header("Clip")] [SerializeField] private PickMode _pickMode;
    [SerializeField] private AudioClip[] _clips;

    [Header("Volume")] [Range(0f, 1f)] [SerializeField]
    private float _volume = 1f;

    [Header("Pitch")] [SerializeField] private PitchMode _pitchMode = PitchMode.FixedPitch;
    [Range(0.1f, 3f)] [SerializeField] private float _pitch = 1f;
    [SerializeField] private Vector2 _pitchRange = new(0.9f, 1.1f);

    [Header("Mixer")] [SerializeField] private AudioMixerGroup _mixerGroup;

    private int _index;

    public float Volume => _volume;

    public float Pitch
    {
        get
        {
            switch (_pitchMode)
            {
                case PitchMode.FixedPitch:
                    return _pitch;
                case PitchMode.RandomPitch:
                    return Random.Range(_pitchRange.x, _pitchRange.y);
                default:
                    return _pitch;
            }
        }
    }

    public AudioMixerGroup MixerGroup => _mixerGroup;

    public AudioClip GetClip()
    {
        if (_clips == null || _clips.Length == 0) return null;

        switch (_pickMode)
        {
            case PickMode.Random:
                return _clips[Random.Range(0, _clips.Length)];
            case PickMode.Sequential:
                var clip = _clips[_index];
                _index = (_index + 1) % _clips.Length;
                return clip;
            default:
                return _clips[Random.Range(0, _clips.Length)];
        }
    }

    private enum PitchMode
    {
        FixedPitch,
        RandomPitch
    }

    private enum PickMode
    {
        Sequential,
        Random
    }

    public void InitializeForTests(AudioClip[] clips, float volume, float pitch, AudioMixerGroup mixerGroup)
    {
        _clips = clips;
        _volume = volume;
        _pitch = pitch;
        _mixerGroup = mixerGroup;
    }
}