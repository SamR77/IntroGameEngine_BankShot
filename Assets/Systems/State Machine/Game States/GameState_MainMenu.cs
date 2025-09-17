using UnityEngine;

public class GameState_MainMenu : IGameState
{
    GameManager gameManager => GameManager.Instance;
    BallManager ballManager => GameManager.Instance.BallManager;
    CameraManager cameraManager => GameManager.Instance.CameraManager;
    UIManager uIManager => GameManager.Instance.UIManager;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;

    #region Singleton Instance
    // A single, readonly instance of the atate class is created.
    // The 'readonly' keyword ensures this instance cannot be modified after initialization.
    private static readonly GameState_MainMenu _instance = new GameState_MainMenu();

    // Provides global access to the singleton instance of this state.
    // Uses an expression-bodied property to return the static _instance variable.
    public static GameState_MainMenu Instance => _instance;
    #endregion

    public void EnterState()
    {   
        Cursor.visible = true;
        Time.timeScale = 1f;

        uIManager.ShowMainMenuUI();

        cameraManager.DisableCameraOrbit();
        cameraManager.EnableMenuCamera();

        ballManager.ballMesh.SetActive(false);
        ballManager.aimGuide.SetActive(false);
        ballManager.rb_ball.isKinematic = true;

        // ADD THIS LINE - Stop any running ball check coroutines
        ballManager.StopCheckBallStoppedAfterDelay();
    
    }
  
    public void FixedUpdateState() {}
    public void UpdateState() {}
    public void LateUpdateState() {}

    public void ExitState() { }


}
