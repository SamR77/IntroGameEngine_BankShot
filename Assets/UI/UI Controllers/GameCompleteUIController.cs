using UnityEngine;
using UnityEngine.UIElements;

public class GameCompleteUIController : MonoBehaviour
{
    UIDocument gameCompleteUIDoc => GetComponent<UIDocument>();

    GameManager gameManager => GameManager.Instance;

    BallManager ballManager => GameManager.Instance.BallManager;
    CameraManager cameraManager => GameManager.Instance.CameraManager;
    UIManager uIManager => GameManager.Instance.UIManager;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;
    LevelManager levelManager => GameManager.Instance.LevelManager;

    Button mainMenuButton;

    #region Setup Button references and Listeners
    private void OnEnable()
    {
        // Button References
        mainMenuButton = gameCompleteUIDoc.rootVisualElement.Q<Button>("MainMenuButton");

        mainMenuButton.clicked += OnMainMenuButtonClicked;

        // Check to make sure buttons are found
        if (mainMenuButton == null) Debug.LogError("Main Menu Button not found in gameCompleteUIDoc");
    }

    private void OnDestroy()
    {
        mainMenuButton.clicked -= OnMainMenuButtonClicked;
    }
    #endregion

    #region Button Actions
    private void OnMainMenuButtonClicked()
    {
        levelManager.LoadScene(0);
    }
    #endregion
}
