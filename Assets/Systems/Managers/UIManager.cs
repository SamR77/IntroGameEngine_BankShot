using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    #region UI Document references & public accessors
    [Header("UI Menu Objects")]
    [SerializeField] private UIDocument mainMenuUI;
    [SerializeField] private UIDocument pausedUI;
    [SerializeField] private UIDocument gameplayUI;
    [SerializeField] private UIDocument levelCompleteUI;
    [SerializeField] private UIDocument levelFailedUI;
    [SerializeField] private UIDocument gameCompleteUI;
    [SerializeField] private UIDocument creditsUI;
    [SerializeField] private UIDocument optionsUI;

    // Public accessors for external access
    public UIDocument MainMenuUI => mainMenuUI;
    public UIDocument PausedUI => pausedUI;
    public UIDocument GameplayUI => gameplayUI;
    public UIDocument LevelCompleteUI => levelCompleteUI;
    public UIDocument LevelFailedUI => levelFailedUI;
    public UIDocument GameCompleteUI => gameCompleteUI;
    public UIDocument CreditsUI => creditsUI;
    public UIDocument OptionsUI => optionsUI;
    #endregion

    #region UIController references & public accessors
    [Header("UI Controllers")]
    [SerializeField] private MainMenuUIController mainMenuUIController;
    [SerializeField] private PausedUIController pausedUIController;
    [SerializeField] private GameplayUIController gameplayUIController;
    [SerializeField] private LevelCompleteUIController levelCompleteUIController;
    [SerializeField] private LevelFailedUIController levelFailedUIController;
    [SerializeField] private GameCompleteUIController gameCompleteUIController;
    [SerializeField] private CreditsUIController creditsUIController;
    [SerializeField] private OptionsUIController optionsUIController;

    // Public accessors for external access
    public MainMenuUIController MainMenuUIController => mainMenuUIController;
    public PausedUIController PausedUIController => pausedUIController;
    public GameplayUIController GameplayUIController => gameplayUIController;
    public LevelCompleteUIController LevelCompleteUIController => levelCompleteUIController;
    public LevelFailedUIController LevelFailedUIController => levelFailedUIController;
    public GameCompleteUIController GameCompleteUIController => gameCompleteUIController;
    public CreditsUIController CreditsUIController => creditsUIController;
    public OptionsUIController OptionsUIController => optionsUIController;


    #endregion


    private void Awake()
    {
        // TODO: consider having UIDoc register themselves with the UIManager on Awake
        // This would replace the FindObjectsOfType calls and make it more efficient/Modular
                
        mainMenuUI = FindUIDocument("MainMenuUI");
        pausedUI = FindUIDocument("PausedUI");
        gameplayUI = FindUIDocument("GameplayUI");
        levelCompleteUI = FindUIDocument("LevelCompleteUI");
        levelFailedUI = FindUIDocument("LevelFailedUI");
        gameCompleteUI = FindUIDocument("GameCompleteUI");
        creditsUI = FindUIDocument("CreditsUI");
        optionsUI = FindUIDocument("OptionsUI");

        // Activate Parent GameObject of all UI Screens (Some are disbaled for visibity in the editor Game view)
        if(mainMenuUI != null) mainMenuUI.gameObject.SetActive(true);
        if(pausedUI != null) pausedUI.gameObject.SetActive(true);
        if(gameplayUI != null) gameplayUI.gameObject.SetActive(true);
        if(levelCompleteUI != null) levelCompleteUI.gameObject.SetActive(true);
        if(levelFailedUI != null) levelFailedUI.gameObject.SetActive(true);
        if(gameCompleteUI != null) gameCompleteUI.gameObject.SetActive(true);
        if(creditsUI != null) creditsUI.gameObject.SetActive(true);
        if(optionsUI != null) optionsUI.gameObject.SetActive(true);

        // Set references to UI Controllers
        mainMenuUIController = mainMenuUI?.GetComponent<MainMenuUIController>();
        pausedUIController = pausedUI?.GetComponent<PausedUIController>();
        gameplayUIController = gameplayUI?.GetComponent<GameplayUIController>();
        levelCompleteUIController = levelCompleteUI?.GetComponent<LevelCompleteUIController>();
        levelFailedUIController = levelFailedUI?.GetComponent<LevelFailedUIController>();
        gameCompleteUIController = gameCompleteUI?.GetComponent<GameCompleteUIController>();
        creditsUIController = creditsUI?.GetComponent<CreditsUIController>();
        optionsUIController = optionsUI?.GetComponent<OptionsUIController>();

        HideAllMenus(); // Start with all menus hidden



    }


    #region Showing / Hiding UI Screens

    public void ShowMainMenuUI()
    {
        HideAllMenus();
        mainMenuUI.rootVisualElement.style.display = DisplayStyle.Flex; // Show Gameplay UI
    }

    public void ShowPausedUI()
    {
        HideAllMenus();
        pausedUI.rootVisualElement.style.display = DisplayStyle.Flex; // Show Paused UI
    }

    public void ShowGameplayUI()
    {
        HideAllMenus();
        gameplayUI.rootVisualElement.style.display = DisplayStyle.Flex; // Show Gameplay UI
    }

    public void ShowLevelCompleteUI()
    {
        HideAllMenus();
        levelCompleteUI.rootVisualElement.style.display = DisplayStyle.Flex; // Show Level Complete UI
    }

    public void ShowLevelFailedUI()
    {
        HideAllMenus();
        levelFailedUI.rootVisualElement.style.display = DisplayStyle.Flex; // Show Level Failed UI
    }

    public void ShowGameCompleteUI()
    {
        HideAllMenus();
        gameCompleteUI.rootVisualElement.style.display = DisplayStyle.Flex; // Show Game Complete UI
    }

    public void ShowCreditsUI()
    {
        HideAllMenus();
        creditsUI.rootVisualElement.style.display = DisplayStyle.Flex; // Show Credits UI
    }

    public void ShowOptionsUI()
        {
        HideAllMenus();
        optionsUI.rootVisualElement.style.display = DisplayStyle.Flex; // Show Options UI
    }

    public void HideAllMenus()
    {

       if (mainMenuUI == null) Debug.LogError("mainMenuUI is null, please check the UIManager setup.");
       if (pausedUI == null) Debug.LogError("pausedUI is null, please check the UIManager setup.");
       if (gameplayUI == null) Debug.LogError("gameplayUI is null, please check the UIManager setup.");
       if (levelCompleteUI == null) Debug.LogError("levelCompleteUI is null, please check the UIManager setup.");
       if (levelFailedUI == null) Debug.LogError("levelFailedUI is null, please check the UIManager setup.");
       if (gameCompleteUI == null) Debug.LogError("gameCompleteUI is null, please check the UIManager setup.");
       if (creditsUI == null) Debug.LogError("creditsUI is null, please check the UIManager setup.");
       if (optionsUI == null) Debug.LogError("optionsUI is null, please check the UIManager setup.");



        mainMenuUI.rootVisualElement.style.display = DisplayStyle.None;
        pausedUI.rootVisualElement.style.display = DisplayStyle.None;
        gameplayUI.rootVisualElement.style.display = DisplayStyle.None;
        levelCompleteUI.rootVisualElement.style.display = DisplayStyle.None;
        levelFailedUI.rootVisualElement.style.display = DisplayStyle.None;
        gameCompleteUI.rootVisualElement.style.display = DisplayStyle.None;
        creditsUI.rootVisualElement.style.display = DisplayStyle.None;
        optionsUI.rootVisualElement.style.display = DisplayStyle.None;

    }








    #endregion



    private UIDocument FindUIDocument(string name)
    {
        var documents = Object.FindObjectsByType<UIDocument>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var doc in documents)
        {
            if (doc.name == name)
            {
                return doc;
            }
        }
        Debug.LogWarning($"UIDocument '{name}' not found in scene.");
        return null;
    }


}
