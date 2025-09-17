// Sam Robichaud 
// NSCC Truro 2025
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

using UnityEngine;
using UnityEngine.SceneManagement;

// GameManager must load first to initialize its references before sub-managers
[DefaultExecutionOrder(-100)]

public class GameManager : MonoBehaviour
{
    // Singleton instance of GameManager for global access
    public static GameManager Instance { get; private set; }

    [Header("Manager References (Auto-Assigned)")]
    // These fields are visible in the Inspector
    // Scripts will be auto-assigned in Awake() if left null
    [SerializeField] private BallManager ballManager;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private UIManager uiManager;

    // Public read-only accessors for other scripts to use the managers
    public BallManager BallManager => ballManager;
    public CameraManager CameraManager => cameraManager;
    public GameStateManager GameStateManager => gameStateManager;
    public InputManager InputManager => inputManager;
    public LevelManager LevelManager => levelManager;
    public UIManager UIManager => uiManager;

    








    [Header("Gameplay Info")]
    public int shotsRemaining = 99;

    [Header("Per Level Info")]
    public LevelInfo _levelInfo;
    public GameObject startPosition;


    private void Awake()
    {
        #region Singleton
        // Singleton pattern to ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion



        // Auto-assign manager references from child objects if not manually assigned
        ballManager ??= GetComponentInChildren<BallManager>();
        cameraManager ??= GetComponentInChildren<CameraManager>();
        gameStateManager ??= GetComponentInChildren<GameStateManager>();
        inputManager ??= GetComponentInChildren<InputManager>();
        levelManager ??= GetComponentInChildren<LevelManager>();
        uiManager ??= GetComponentInChildren<UIManager>();
    }

    public void CheckForRemainingShots()
    {
        if (shotsRemaining > 0)
        {
            // this means the goal has not been reached and there are still shots left
            // thought... would it make sense to have a bool for LevelComplete when the goal is reached that could be checked against here? It may not be needed as the logic already in place should interrupt this state before it gets here.
            Debug.Log("Level not complete, Shots remaining switching to GameState_Aim");
            gameStateManager.SwitchToState(GameState_Aim.Instance);
        }
        else if(shotsRemaining <= 0)
        {
            //no shots left, Level failed... trigger the level failed state

            // TODO: Implement Level Failed State
            gameStateManager.SwitchToState(GameState_LevelFailed.Instance);

            Debug.Log("Level Failed - No Shots Remaining");
        }


    }

    public void CheckForGameWin()
    {
        // check to see if there are remaing levels after this one
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            Debug.Log("Level Complete!");
            gameStateManager.SwitchToState(GameState_LevelComplete.Instance);
            return;
        }
        else if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            Debug.Log("Game Complete - All Levels Finished");
            // All levels complete, trigger game complete state
            gameStateManager.SwitchToState(GameState_GameComplete.Instance);
        }        
    }


}
