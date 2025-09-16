using UnityEngine;

public class GameState_GameComplete : IGameState
{
    GameManager gameManager => GameManager.Instance;
    BallManager ballManager => GameManager.Instance.BallManager;
    CameraManager cameraManager => GameManager.Instance.CameraManager;
    UIManager uIManager => GameManager.Instance.UIManager;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;

    #region Singleton Instance
    // A single, readonly instance of the atate class is created.
    // The 'readonly' keyword ensures this instance cannot be modified after initialization.
    private static readonly GameState_GameComplete _instance = new GameState_GameComplete();

    // Provides global access to the singleton instance of this state.
    // Uses an expression-bodied property to return the static _instance variable.
    public static GameState_GameComplete Instance => _instance;
    #endregion

    public void EnterState()
    {
        Cursor.visible = true;

        Time.timeScale = 0f;

        cameraManager.DisableCameraOrbit();
        

        // hide all UI Menus
        uIManager.ShowGameCompleteUI();


    }

  
    public void FixedUpdateState() {}
    public void UpdateState() {}
    public void LateUpdateState() {}

    public void ExitState() { }


}
