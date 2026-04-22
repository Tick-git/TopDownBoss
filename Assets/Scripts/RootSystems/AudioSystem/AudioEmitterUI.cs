public class AudioEmitterUI
{
    private readonly IAudioService _audioService;
    private readonly UIAudioLibrary _uiAudioLibrary;
    
    public AudioEmitterUI(IAudioService audioService, UIAudioLibrary uiAudioLibrary)
    {
        _audioService = audioService;
        _uiAudioLibrary = uiAudioLibrary;
    }

    public void PlayHoverSound()
    {
        _audioService.PlayUI(_uiAudioLibrary.HoverAudioData);
    }

    public void PlayClickedSound()
    {
        _audioService.PlayUI(_uiAudioLibrary.ClickAudioData);
    }
}