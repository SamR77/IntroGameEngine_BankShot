using UnityEngine;
using UnityEngine.SceneManagement;
public class GameState_BootLoad : IGameState
{
    GameManager gameManager => GameManager.Instance;
    BallManager ballManager => GameManager.Instance.BallManager;
    CameraManager cameraManager => GameManager.Instance.CameraManager;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;
    InputManager inputManager => GameManager.Instance.InputManager;
    UIManager uIManager => GameManager.Instance.UIManager;

    #region Singleton Instance
    // A single, readonly instance of the atate class is created.
    // The 'readonly' keyword ensures this instance cannot be modified after initialization.
    private static readonly GameState_BootLoad _instance = new GameState_BootLoad();

    // Provides global access to the singleton instance of this state.
    // Uses an expression-bodied property to return the static _instance variable.
    public static GameState_BootLoad Instance => _instance;
    #endregion

    public void EnterState()
    {
        // Hide cursor and lock it to the center of the screen
        Cursor.visible = false;

        // Set timescale to 0f;
        Time.timeScale = 0f;
        
        // disable all Cinemachine cameras
        cameraManager.DisableAllCameras();

        // Detect Current Scene Type and set GameState accordingly

        // if BootLoader is the only active scene, redirect to MainMenu
        if (SceneManager.sceneCount == 1 && SceneManager.GetActiveScene().name == "BootLoader")
        {
            Debug.Log("BootLoader is the only active scene. Loading MainMenu...");
            GameManager.Instance.LevelManager.LoadMainMenu();
            return;
        }

        // if the Bootloader is Initialized while in the MainMenu Scene
        else if (SceneManager.sceneCount > 1 && SceneManager.GetActiveScene().name == "MainMenu")
        {
            Debug.Log("BootLoader initialized in MainMenu Scene, Switching to GameState_MainMenu");
            gameManager.GameStateManager.SwitchToState(GameState_MainMenu.Instance);
            return;
        }

        // if all the above fails the assumption is that we are in a Gameplay Scene
        else if (SceneManager.sceneCount > 1)
        {  
            ballManager.SetBallToStartPosition();

            Debug.Log("BootLostrap initialized in Gameplay Scenede, Switching to GameState_Aim");
            gameManager.GameStateManager.SwitchToState(GameState_Aim.Instance);
        }


    }

  
    public void FixedUpdateState() {}
    public void UpdateState() {}
    public void LateUpdateState() {}

    public void ExitState() { }


}
