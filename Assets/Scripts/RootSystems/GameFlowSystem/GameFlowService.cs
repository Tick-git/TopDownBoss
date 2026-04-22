using UnityEngine;

public class GameFlowService
{
    private readonly ViewInteractionController _viewInteractionController;
    private readonly SceneController _sceneController;
    
    public GameFlowService(ViewInteractionController viewInteractionController, SceneController sceneController)
    {
        _viewInteractionController = viewInteractionController;
        _sceneController = sceneController;
    }
    
    public void LoadGameplay()
    { 
        _viewInteractionController.LockInteraction();
        
        _sceneController.CreateTransition()
            .Load(SceneDatabase.Slots.Content, SceneDatabase.Scenes.Gameplay, true)
            .WithFade()
            .Execute();
    }

    public void LoadMainMenu()
    {
        _viewInteractionController.LockInteraction();
        
        _sceneController.CreateTransition()
            .Load(SceneDatabase.Slots.Content, SceneDatabase.Scenes.MainMenu, true)
            .WithFade()
            .Execute();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}