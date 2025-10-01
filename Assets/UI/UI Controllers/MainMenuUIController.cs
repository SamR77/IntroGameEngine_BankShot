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

    InputManager inputManager => GameManager.Instance.InputManager;

    Button playButton;
    Button optionsButton;
    Button quitButton;

    private Button[] menuButtons;
    private int focusedIndex = 0;


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

        inputManager.NavigateEvent += OnNavigate;
        inputManager.SubmitEvent += OnSubmit;

        // Check to make sure buttons are found
        if (playButton == null) Debug.LogError("Play Button not found in MainMenu_UIDoc");
        if (optionsButton == null) Debug.LogError("Options Button not found in MainMenu_UIDoc");
        if (quitButton == null) Debug.LogError("Quit Button not found in MainMenu_UIDoc");

        // Set initial focus to Play button for better UX
        playButton.Focus();

        menuButtons = new[] { playButton, optionsButton, quitButton };
        focusedIndex = 0;
        menuButtons[focusedIndex].Focus();


    }

    private void OnDisable()
    {
        playButton.clicked -= OnPlayButtonClicked;
        optionsButton.clicked -= OnOptionsButtonClicked;
        quitButton.clicked -= OnQuitButtonClicked;

        inputManager.NavigateEvent -= OnNavigate;
        inputManager.SubmitEvent -= OnSubmit;

    }
    #endregion

    #region Button Actions
    private void OnPlayButtonClicked()
    {
        // Debug.Log("Play Button Clicked");
        levelManager.LoadFirstLevel();

        //levelManager.LoadScene(2);
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

    #region UI Navigation Actions

    private void OnNavigate(Vector2 direction)
    {
        // Only allow navigation if the main menu is active
        if (uIManager.MainMenuUI.rootVisualElement.style.display == DisplayStyle.Flex)
        {
            Debug.Log($"Navigate: {direction}");

        }
    }

    private void OnSubmit()
    {
        // Only allow submit if the main menu is active
        if (uIManager.MainMenuUI.rootVisualElement.style.display == DisplayStyle.Flex)
        {
            Debug.Log("Submit");
        }
       
    }


    #endregion
}


