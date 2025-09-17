// Sam Robichaud 
// NSCC Truro 2025
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

using UnityEngine;

public class GameState_LevelComplete : IGameState
{
    GameManager gameManager => GameManager.Instance;

    BallManager ballManager => GameManager.Instance.BallManager;
    CameraManager cameraManager => GameManager.Instance.CameraManager;
    UIManager uIManager => GameManager.Instance.UIManager;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;
    LevelManager levelManager => GameManager.Instance.LevelManager;

    #region Singleton Instance
    // A single, readonly instance of the atate class is created.
    // The 'readonly' keyword ensures this instance cannot be modified after initialization.
    private static readonly GameState_LevelComplete _instance = new GameState_LevelComplete();

    // Provides global access to the singleton instance of this state.
    // Uses an expression-bodied property to return the static _instance variable.
    public static GameState_LevelComplete Instance => _instance;
    #endregion

    public void EnterState()
    {
        Cursor.visible = true;        

        cameraManager.DisableCameraOrbit();        

        // hide all UI Menus
        uIManager.ShowLevelCompleteUI();


        // Check if coroutine CheckBallStoppedAfterDelay() in ball manager is still running and stop it 
        // Stop the coroutine if it's running
        ballManager.StopCheckBallStoppedAfterDelay();
    }


    public void FixedUpdateState() {}
    public void UpdateState() {}
    public void LateUpdateState() {}

    public void ExitState() { }


}
