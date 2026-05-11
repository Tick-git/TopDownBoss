using UnityEngine;
using UnityEngine.UIElements;

public class RootBootstrap : MonoBehaviour
{
    [SerializeField] private Root _rootPrefab;
    [SerializeField] private UIDocument _uiRootPrefab;
    [SerializeField] private SceneController _sceneControllerPrefab;
    [SerializeField] private InputManager _inputManagerPrefab;
    [SerializeField] private UIAudioLibrary _uiAudioLibrary;
    [SerializeField] private AudioManager _audioManagerPrefab;
    [SerializeField] private VFXManager _vfxManagerPrefab;

    public ViewStack ViewStack { get; private set; }
    public GameFlowService GameFlowService { get; private set; }
    public SettingsManager SettingsManager { get; private set; }
    public SceneController SceneController { get; private set; }
    public InputManager InputManager { get; private set; }
    public AudioManager AudioManager { get; private set; }
    public AudioEmitterUI AudioEmitterUI { get; private set; }
    public UIInputReader UIInputReader { get; private set; }
    
    public VFXManager VFXManager { get; private set; }

    private MouseVisibilityController _mouseVisibilityController;
    private ViewInteractionController _viewInteractionController;
    private ViewFocusController _viewFocusController;
    private ViewAnimationController _viewAnimationController;

    private void Start()
    {
        Root root = Instantiate(_rootPrefab);
        root.Initialize(this);

        var uiRoot = Instantiate(_uiRootPrefab);
        ViewStack = new ViewStack(uiRoot.rootVisualElement);

        InputManager = Instantiate(_inputManagerPrefab);
        AudioManager = Instantiate(_audioManagerPrefab);
        SceneController = Instantiate(_sceneControllerPrefab);
        SettingsManager = new SettingsManager(AudioManager);
        AudioEmitterUI = new AudioEmitterUI(AudioManager, _uiAudioLibrary);
        UIInputReader = new UIInputReader(ViewStack);
        VFXManager = Instantiate(_vfxManagerPrefab);

        _mouseVisibilityController = new MouseVisibilityController(ViewStack, InputManager);
        _viewInteractionController = new ViewInteractionController(ViewStack, InputManager);
        _viewFocusController = new ViewFocusController(ViewStack, _viewInteractionController);
        _viewAnimationController = gameObject.AddComponent<ViewAnimationController>();
        _viewAnimationController.Initialize(ViewStack);

        GameFlowService = new GameFlowService(_viewInteractionController, SceneController);

        HandleEventSubscriptions();

        SceneController.CreateTransition()
            .Load(SceneDatabase.Slots.Content, SceneDatabase.Scenes.MainMenu, true)
            .Execute();
    }

    private void HandleEventSubscriptions()
    {
        UIInputReader.CancelPerformed += ViewStack.HandleCancel;
    }

    private void OnDestroy()
    {
        UIInputReader.CancelPerformed -= ViewStack.HandleCancel;

        _mouseVisibilityController.Dispose();
        _viewInteractionController.Dispose();
        _viewInteractionController.Dispose();
    }
}