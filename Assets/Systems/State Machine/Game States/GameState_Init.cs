using Unity.VisualScripting;
using UnityEngine;

public class GameState_Init : IGameState
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
    private static readonly GameState_Init _instance = new GameState_Init();

    // Provides global access to the singleton instance of this state.
    // Uses an expression-bodied property to return the static _instance variable.
    public static GameState_Init Instance => _instance;
    #endregion

    public void EnterState()
    {
        // Hide cursor and lock it to the center of the screen
        Cursor.visible = false;

        // Set timescale to 0f;
        Time.timeScale = 0f;
        
        // disable all Cinemachine cameras
        cameraManager.DisableAllCameras();

        // Switch over to default starting state
        gameStateManager.SwitchToState(GameState_MainMenu.Instance);
    }

  
    public void FixedUpdateState() {}
    public void UpdateState() {}
    public void LateUpdateState() {}

    public void ExitState() { }


}
