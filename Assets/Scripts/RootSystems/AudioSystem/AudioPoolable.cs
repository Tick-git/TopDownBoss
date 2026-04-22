using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPoolable : MonoBehaviour, IPoolable<AudioPoolable>
{
    private ObjectPool<AudioPoolable> _pool;
    private Coroutine _playRoutine;
    private bool _isReturned = true;

    private AudioSource _audioSource;

    public void Initialize(ObjectPool<AudioPoolable> pool)
    {
        _pool = pool;
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnGetFromPool()
    {
        _isReturned = false;
    }

    public void OnReturnToPool()
    {
        _isReturned = true;

        if (_playRoutine != null)
        {
            StopCoroutine(_playRoutine);
            _playRoutine = null;
        }

        _audioSource.Stop();
        _audioSource.clip = null;
    }

    public void Play(AudioData audio)
    {
        _audioSource.clip = audio.GetClip();
        _audioSource.volume = audio.Volume;
        _audioSource.pitch = audio.Pitch;
        _audioSource.outputAudioMixerGroup = audio.MixerGroup;
        
        _audioSource.Play();

        if (_playRoutine != null)
            StopCoroutine(_playRoutine);

        _playRoutine = StartCoroutine(WaitForFinish());
    }

    private IEnumerator WaitForFinish()
    {
        while (_audioSource.isPlaying)
            yield return null;

        if (!_isReturned)
            _pool.Return(this);
    }
}