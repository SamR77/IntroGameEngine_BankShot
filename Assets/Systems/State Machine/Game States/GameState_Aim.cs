using UnityEngine;

public class GameState_Aim : IGameState
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
    private static readonly GameState_Aim _instance = new GameState_Aim();

    // Provides global access to the singleton instance of this state.
    // Uses an expression-bodied property to return the static _instance variable.
    public static GameState_Aim Instance => _instance;
    #endregion

    public void EnterState()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;

        cameraManager.EnableBallCamera();
        cameraManager.EnableCameraOrbit();
        
        ballManager.ballMesh.SetActive(true);     
        ballManager.aimGuide.SetActive(true);
        ballManager.rb_ball.isKinematic = false; // enable ball physics

        uIManager.ShowGameplayUI();

        inputManager.ShootEvent += ballManager.ShootBall;
        inputManager.PauseEvent += gameStateManager.Pause;

    }

   

    public void FixedUpdateState()
    {
        
    }

    public void UpdateState()
    {
        
        ballManager.HandleAimGuide();

    }

    public void LateUpdateState()
    {
        cameraManager.HandleRotation();
        cameraManager.HandleZoom();
    }

    public void ExitState()
    {

        //ballManager.aimGuide.SetActive(false); // disable aim guide while ball is rolling

        inputManager.ShootEvent -= ballManager.ShootBall;
        inputManager.PauseEvent -= gameStateManager.Pause;
    }

}
