using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.CoreData.AudioPath + "UILibrary")]
public class UIAudioLibrary : ScriptableObject
{
    [SerializeField] private AudioData _hoverAudioData;
    [SerializeField] private AudioData _clickAudioData;
    
    public AudioData HoverAudioData => _hoverAudioData;

    public AudioData ClickAudioData => _clickAudioData;
}