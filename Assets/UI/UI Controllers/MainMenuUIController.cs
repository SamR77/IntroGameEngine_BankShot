// Sam Robichaud 
// NSCC Truro 2025
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUIController : MonoBehaviour
{
    private UIDocument mainMenuUIDoc => GetComponent<UIDocument>();

    GameManager gameManager => GameManager.Instance;

    BallManager ballManager => GameManager.Instance.BallManager;
    CameraManager cameraManager => GameManager.Instance.CameraManager;
    UIManager uIManager => GameManager.Instance.UIManager;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;
    LevelManager levelManager => GameManager.Instance.LevelManager;

    Button playButton;
    Button optionsButton;
    Button quitButton;

    #region Setup Button references and Listeners
    private void OnEnable()
    {
        // Button References
        playButton = mainMenuUIDoc.rootVisualElement.Q<Button>("PlayButton");
        optionsButton = mainMenuUIDoc.rootVisualElement.Q<Button>("OptionsButton");
        quitButton = mainMenuUIDoc.rootVisualElement.Q<Button>("QuitButton");

        playButton.clicked += OnPlayButtonClicked;
        optionsButton.clicked += OnOptionsButtonClicked;
        quitButton.clicked += OnQuitButtonClicked;

        // Check to make sure buttons are found
        if (playButton == null) Debug.LogError("Play Button not found in MainMenu_UIDoc");
        if (optionsButton == null) Debug.LogError("Options Button not found in MainMenu_UIDoc");
        if (quitButton == null) Debug.LogError("Quit Button not found in MainMenu_UIDoc");
    }

    private void OnDestroy()
    {
        playButton.clicked -= OnPlayButtonClicked;
        optionsButton.clicked -= OnOptionsButtonClicked;
        quitButton.clicked -= OnQuitButtonClicked;
    }
    #endregion

    #region Button Actions
    private void OnPlayButtonClicked()
    {
        Debug.Log("Play Button Clicked");
        levelManager.LoadScene(1);
        //gameStateManager.SwitchToState(GameState_Aim.Instance);
    }

    private void OnOptionsButtonClicked()
    {
        Debug.Log("Options Button Clicked");
    }

    private void OnQuitButtonClicked()
    {
        Debug.Log("Quit Button Clicked");
        Application.Quit();        
    }
    #endregion
}


