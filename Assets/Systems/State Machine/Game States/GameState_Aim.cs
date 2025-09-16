using Unity.VisualScripting;
using UnityEngine;

public class GameState_Aim : IGameState
{
    GameManager gameManager => GameManager.Instance;
    InputManager inputManager => GameManager.Instance.InputManager;

    BallManager ballManager => gameManager.BallManager;
    CameraManager cameraManager => gameManager.CameraManager;
    //UIManager uIManager => gameManager.uIManager;

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

        ballManager.aimGuide.SetActive(true); 

        inputManager.ShootEvent += ballManager.ShootBall;
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

        ballManager.aimGuide.SetActive(false); // disable aim guide while ball is rolling

        inputManager.ShootEvent -= ballManager.ShootBall;
    }

}
