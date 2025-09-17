using UnityEngine;
using UnityEngine.UIElements;

public class PausedUIController : MonoBehaviour
{
    UIDocument mainMenuUI => GetComponent<UIDocument>();

    GameManager gameManager => GameManager.Instance;
    BallManager ballManager => GameManager.Instance.BallManager;
    CameraManager cameraManager => GameManager.Instance.CameraManager;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;
    InputManager inputManager => GameManager.Instance.InputManager;
    LevelManager levelManager => GameManager.Instance.LevelManager;
    UIManager uIManager => GameManager.Instance.UIManager;

    Button resumeButton;
    Button restartButton;
    Button optionsButton;
    Button mainMenuButton;

    #region Setup Button references and Listeners
    private void OnEnable()
    {

        // Button References
        resumeButton = mainMenuUI.rootVisualElement.Q<Button>("ResumeButton");
        restartButton = mainMenuUI.rootVisualElement.Q<Button>("RestartButton");
        optionsButton = mainMenuUI.rootVisualElement.Q<Button>("OptionsButton");
        mainMenuButton = mainMenuUI.rootVisualElement.Q<Button>("MainMenuButton");

        resumeButton.clicked += OnResumeButtonClicked;
        restartButton.clicked += OnRestartButtonClicked;
        optionsButton.clicked += OnOptionsButtonClicked;
        mainMenuButton.clicked += OnMainMenuButtonClicked;

        // Check to make sure buttons are found        
        if(resumeButton == null) Debug.LogError("ResumeButton not found in Paused_UIDoc");
        if(restartButton == null) Debug.LogError("RestartButton not found in Paused_UIDoc");
        if(optionsButton == null) Debug.LogError("OptionsButton not found in Paused_UIDoc");
        if (mainMenuButton == null) Debug.LogError("MainMenuButton not found in Paused_UIDoc");

    }

    private void OnDestroy()
    {
        resumeButton.clicked -= OnResumeButtonClicked;
        restartButton.clicked -= OnRestartButtonClicked;
        optionsButton.clicked -= OnOptionsButtonClicked;
        mainMenuButton.clicked -= OnMainMenuButtonClicked;
    }
    #endregion

    #region Button Actions

    private void OnResumeButtonClicked()
    {
        Debug.Log("Resume Button Clicked");
        gameStateManager.Resume();
    }

    private void OnRestartButtonClicked()
    {
        levelManager.ReloadCurrentScene();
    }

    private void OnOptionsButtonClicked()
    {
        Debug.Log("Options Button Clicked");
    }

    private void OnMainMenuButtonClicked()
    {
        Debug.Log("MainMenu Button Clicked");
        levelManager.LoadMainMenuScene();
    }

    #endregion
}


