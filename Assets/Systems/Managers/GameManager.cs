using UnityEngine;


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
    [SerializeField] private UIManager uiManager;


    // Public read-only accessors for other scripts to use the managers
    public InputManager InputManager => inputManager;
    public CameraManager CameraManager => cameraManager;
    public GameStateManager GameStateManager => gameStateManager;
    public UIManager UIManager => uiManager;
    public BallManager BallManager => ballManager;








    [Header("Gameplay Info")]
    public int shotsLeft = 0;

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
        uiManager ??= GetComponentInChildren<UIManager>();





    }







}
