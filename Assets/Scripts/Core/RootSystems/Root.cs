public class Root : Singleton<Root>
{
    public GameFlowService GameFlowService => _rootBootstrap.GameFlowService;
    public SettingsManager SettingsManager => _rootBootstrap.SettingsManager;
    public ViewStack ViewStack => _rootBootstrap.ViewStack;
    public SceneController SceneController => _rootBootstrap.SceneController;
    public InputManager InputManager => _rootBootstrap.InputManager;
    public AudioManager AudioManager => _rootBootstrap.AudioManager;
    public UIInputReader UIInputReader => _rootBootstrap.UIInputReader;

    public AudioEmitterUI AudioEmitterUI => _rootBootstrap.AudioEmitterUI;

    public bool Initialized => _rootBootstrap.Initialized;
    
    private RootBootstrap _rootBootstrap;

    public void Initialize(RootBootstrap rootBootstrap)
    {
        _rootBootstrap = rootBootstrap;
    }
}