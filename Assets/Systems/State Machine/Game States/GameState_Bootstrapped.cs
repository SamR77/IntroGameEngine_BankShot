using UnityEngine;
using UnityEngine.SceneManagement;
public class GameState_Bootstrapped : IGameState
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
    private static readonly GameState_Bootstrapped _instance = new GameState_Bootstrapped();

    // Provides global access to the singleton instance of this state.
    // Uses an expression-bodied property to return the static _instance variable.
    public static GameState_Bootstrapped Instance => _instance;
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
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Debug.Log("Bootstrap detected Main Menu Level, Switching to GameState_MainMenu");
            gameManager.GameStateManager.SwitchToState(GameState_MainMenu.Instance);
        }
        else
        {
            Debug.Log("Bootstrap detected gameplay level Level, Switching to GameState_Aim");
            gameManager.GameStateManager.SwitchToState(GameState_Aim.Instance);
        }


        //uIManager.DetectAndSetUIForCurrentScene();


        // Switch over to default starting state
        // gameStateManager.SwitchToState(GameState_MainMenu.Instance);
    }

  
    public void FixedUpdateState() {}
    public void UpdateState() {}
    public void LateUpdateState() {}

    public void ExitState() { }


}
