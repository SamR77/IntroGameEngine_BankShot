using UnityEngine;
using UnityEngine.UIElements;

public class LevelFailedUIController : MonoBehaviour
{
    private UIDocument levelFailedUIDoc => GetComponent<UIDocument>();

    GameManager gameManager => GameManager.Instance;

    BallManager ballManager => GameManager.Instance.BallManager;
    CameraManager cameraManager => GameManager.Instance.CameraManager;
    UIManager uIManager => GameManager.Instance.UIManager;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;
    LevelManager levelManager => GameManager.Instance.LevelManager;


    Button retryButton;
    Button mainMenuButton;

    #region Setup Button references and Listeners
    private void OnEnable()
    {
        // Button References
        retryButton = levelFailedUIDoc.rootVisualElement.Q<Button>("RetryButton");
        mainMenuButton = levelFailedUIDoc.rootVisualElement.Q<Button>("MainMenuButton");

        retryButton.clicked += OnRetryButtonClicked;
        mainMenuButton.clicked += OnMainMenuButtonClicked;

        // Check to make sure buttons are found
        if (retryButton == null) Debug.LogError("Retry Button not found in LevelFailed_UIDoc");
        if (mainMenuButton == null) Debug.LogError("Main Menu Button not found in LevelFailed_UIDoc");
    }



    private void OnDestroy()
    {
        retryButton.clicked -= OnRetryButtonClicked;
        mainMenuButton.clicked -= OnMainMenuButtonClicked;
    }
    #endregion

    #region Button Actions
    private void OnRetryButtonClicked()
    {
        Debug.Log("Retry Button Clicked");
        levelManager.ReloadCurrentScene();
    }

    private void OnMainMenuButtonClicked()
    {
        levelManager.LoadMainMenuScene();
    }
    #endregion

}
