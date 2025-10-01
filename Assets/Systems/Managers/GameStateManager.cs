using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // Manager References 
    GameManager gameManager => GameManager.Instance;
    InputManager inputManager => GameManager.Instance.InputManager;

    [Header("Debug (read only)")]    
    [SerializeField] private string currentActiveState;
    [SerializeField] private string lastActiveState;

    // Private variables to store state information
    private IGameState currentGameState;  // Current active state
    private IGameState lastGameState;     // Last active state (kept private for encapsulation)

    // Public getter for accessing the lastState externally (read-only access)
    public IGameState LastGameState
    {
        get { return lastGameState; }
    }

    // Instantiate game state objects
    public GameState_BootLoad gameState_Bootstrapped = GameState_BootLoad.Instance;

    // Menu States
    public GameState_MainMenu gameState_MainMenu = GameState_MainMenu.Instance;
    public GameState_Paused gameState_Paused = GameState_Paused.Instance;
    public GameState_LevelComplete gameState_LevelComplete = GameState_LevelComplete.Instance;
    public GameState_LevelFailed gameState_LevelFailed = GameState_LevelFailed.Instance;
    public GameState_Credits gameState_Credits = GameState_Credits.Instance;
    public GameState_GameComplete gameState_GameComplete = GameState_GameComplete.Instance;
    public GameState_Options gameState_OptionsMenu = GameState_Options.Instance;

    // Gameplay States
    public GameState_Aim gameState_Aim = GameState_Aim.Instance;
    public GameState_Rolling gameState_Rolling = GameState_Rolling.Instance;


    private void Start()
    {
        // Initialize the state manager with the initial state
        currentGameState = gameState_Bootstrapped;
        currentGameState.EnterState();
    }

    #region State Machine Update Calls

    // Fixed update is called before update, and is used for physics calculations
    private void FixedUpdate()
    {
        // Handle physics updates in the current active state (if applicable)
        currentGameState.FixedUpdateState();
    }

    private void Update()
    {
        // Handle regular frame updates in the current active state
        currentGameState.UpdateState();

        // Keeping track of active and last states for debugging purposes
        // TODO: I can probably move these out of Update and just set them when switching states ... look into moving down into SwitchToState method
        currentActiveState = currentGameState.ToString();   // Show current state in Inspector
        lastActiveState = lastGameState?.ToString();        // Show last state in Inspector
    }

    // LateUpdate for any updates that need to happen after regular Update
    private void LateUpdate()
    {
        currentGameState.LateUpdateState();
    }

    #endregion

    // Method to switch between states
    public void SwitchToState(IGameState newState)
    {
        Debug.Log($"Switching from {currentGameState?.GetType().Name} to {newState.GetType().Name}");
        // Debug.Log($"Stack trace: {System.Environment.StackTrace}");

        lastGameState = currentGameState;
        currentGameState?.ExitState();
        currentGameState = newState;
        currentGameState.EnterState();
    }

    public void Pause()
    {
        SwitchToState(GameState_Paused.Instance);
    }

    public void Resume()
    {
        if (currentGameState == GameState_Paused.Instance && LastGameState == GameState_Aim.Instance)
        {
            SwitchToState(GameState_Aim.Instance);
            Debug.Log("Resuming gameplay in Aim state");
        }
        if (currentGameState == GameState_Paused.Instance && LastGameState == GameState_Rolling.Instance)
        {
            SwitchToState(GameState_Rolling.Instance);
            Debug.Log("Resuming gameplay in Rolling state");
        }
    }

    public void LoadLastState()
    {
        SwitchToState(lastGameState);
    }

}
