public class Root : Singleton<Root>
{
    public GameFlowService GameFlowService => _rootBootstrap.GameFlowService;
    public SettingsManager SettingsManager => _rootBootstrap.SettingsManager;
    public ViewStack ViewStack => _rootBootstrap.ViewStack;
    public SceneController SceneController => _rootBootstrap.SceneController;
    public InputManager InputManager => _rootBootstrap.InputManager;
    public AudioManager AudioManager => _rootBootstrap.AudioManager;
    public InputReader InputReader => _rootBootstrap.InputReader;

    public AudioEmitterUI AudioEmitterUI => _rootBootstrap.AudioEmitterUI;
    public VFXManager VFXManager => _rootBootstrap.VFXManager;

    private RootBootstrap _rootBootstrap;

    public void Initialize(RootBootstrap rootBootstrap)
    {
        _rootBootstrap = rootBootstrap;
    }
}