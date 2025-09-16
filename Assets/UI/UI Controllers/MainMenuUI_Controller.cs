using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUI_Controller : MonoBehaviour
{
    UIDocument mainMenuUI => GetComponent<UIDocument>();

    GameManager gameManager => GameManager.Instance;
    LevelManager levelManager => GameManager.Instance.LevelManager;

    Button playButton;
    Button optionsButton;
    Button quitButton;

    #region Setup Button references and Listeners
    private void OnEnable()
    {

        // Button References
        playButton = mainMenuUI.rootVisualElement.Q<Button>("PlayButton");
        optionsButton = mainMenuUI.rootVisualElement.Q<Button>("OptionsButton");
        quitButton = mainMenuUI.rootVisualElement.Q<Button>("QuitButton");

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
    }

    private void OnOptionsButtonClicked()
    {
        Debug.Log("Options Button Clicked");
    }

    private void OnQuitButtonClicked()
    {
        Debug.Log("Quit Button Clicked");
    }

    #endregion
}


