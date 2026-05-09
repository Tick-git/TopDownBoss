using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AudioSystemTests
{
    private ObjectPool<AudioPoolable> CreatePool()
    {
        var obj = new GameObject("testAudioPooledPrefab");
        obj.AddComponent<AudioPoolable>();
        obj.AddComponent<AudioSource>();
        return new ObjectPool<AudioPoolable>(obj, 1, obj.transform);
    }
    
    private AudioData CreateSoundData()
    {
        var clip = AudioClip.Create("testClip", 44100, 1, 44100, false);
        var soundData = ScriptableObject.CreateInstance<AudioData>();
        soundData.InitializeForTests(new[]{clip}, 1, 1, null);
        return soundData;
    }
    
    [UnityTest]
    public IEnumerator Returns_To_Pool_When_Audio_Source_Stop()
    {
        var pool = CreatePool();
        var soundData = CreateSoundData();

        var audioPooled = pool.Get();
        audioPooled.Play(soundData);

        var audioSource = audioPooled.GetComponent<AudioSource>();

        audioSource.Stop();

        yield return new WaitForSeconds(0.1f);

        var next = pool.Get();

        Assert.AreSame(audioPooled, next);
    }
}
