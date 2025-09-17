// Sam Robichaud 
// NSCC Truro 2025
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

using UnityEngine;
using UnityEngine.UIElements;

public class LevelCompleteUIController : MonoBehaviour
{
    UIDocument levelCompleteUI => GetComponent<UIDocument>();

    GameManager gameManager => GameManager.Instance;
       
    BallManager ballManager => GameManager.Instance.BallManager;
    CameraManager cameraManager => GameManager.Instance.CameraManager;
    UIManager uIManager => GameManager.Instance.UIManager;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;
    LevelManager levelManager => GameManager.Instance.LevelManager;

    Button nextLevelButton;

    private void OnEnable()
    {
        // Button References
        nextLevelButton = levelCompleteUI.rootVisualElement.Q<Button>("NextLevelButton");

        nextLevelButton.clicked += OnNextLevelButtonClicked;
    }

    private void OnDestroy()
    { 
        nextLevelButton.clicked -= OnNextLevelButtonClicked;
    }


    void OnNextLevelButtonClicked()
    { 
        levelManager.LoadNextLevel();
    }

}
