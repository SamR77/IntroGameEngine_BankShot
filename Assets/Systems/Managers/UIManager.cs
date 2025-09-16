using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [Header("UI Menu Objects")]
    [SerializeField] private UIDocument mainMenu_UI;
    [SerializeField] private UIDocument paused_UI;
    [SerializeField] private UIDocument gameplay_UI;
    [SerializeField] private UIDocument levelComplete_UI;
    [SerializeField] private UIDocument levelFailed_UI;
    [SerializeField] private UIDocument gameComplete_UI;
    [SerializeField] private UIDocument credits_UI;
    [SerializeField] private UIDocument options_UI;

    [Header("UI Controllers")]
    // [SerializeField] private OverlayUI_Controller overlayUI_Controller;
    // [SerializeField] private MainMenuUI_Controller mainMenuUI_Controller;
    // [SerializeField] private GameplayUI_Controller gameplayUI_Controller;
    // [SerializeField] private PauseMenuUI_Controller pauseMenuUI_Controller;
    // [SerializeField] private LoadGameMenuUI_Controller loadGameMenuUI_Controller;
    // [SerializeField] private SettingsMenuUI_Controller settingsMenuUI_Controller;
    // [SerializeField] private CreditsMenuUI_Controller creditsMenuUI_Controller;
    // [SerializeField] private CollectiblesMenuUI_Controller collectiblesMenuUI_Controller;

    // Public read-only properties for external access
    public UIDocument MainMenu_UI => mainMenu_UI;
    public UIDocument Paused_UI => paused_UI;
    public UIDocument Gameplay_UI => gameplay_UI;
    public UIDocument LevelComplete_UI => levelComplete_UI;
    public UIDocument LevelFailed_UI => levelFailed_UI;
    public UIDocument GameComplete_UI => gameComplete_UI;
    public UIDocument Credits_UI => credits_UI;
    public UIDocument Options_UI => options_UI;

    private void Awake()
    {
        // TODO: consider having UIDoc register themselves with the UIManager on Awake
        // This would replace the FindObjectsOfType calls and make it more efficient/Modular

        mainMenu_UI = FindUIDocument("MainMenu_UI");
        paused_UI = FindUIDocument("Paused_UI");
        gameplay_UI = FindUIDocument("Gameplay_UI");
        levelComplete_UI = FindUIDocument("LevelComplete_UI");
        levelFailed_UI = FindUIDocument("LevelFailed_UI");
        gameComplete_UI = FindUIDocument("GameComplete_UI");
        credits_UI = FindUIDocument("Credits_UI");
        options_UI = FindUIDocument("Options_UI");

        // Activate Parent GameObject of all UI Screens (Some are disbaled for visibity in the editor Game view)
        if(mainMenu_UI != null) mainMenu_UI.gameObject.SetActive(true);
        if(paused_UI != null) paused_UI.gameObject.SetActive(true);
        if(gameplay_UI != null) gameplay_UI.gameObject.SetActive(true);
        if(levelComplete_UI != null) levelComplete_UI.gameObject.SetActive(true);
        if(levelFailed_UI != null) levelFailed_UI.gameObject.SetActive(true);
        if(gameComplete_UI != null) gameComplete_UI.gameObject.SetActive(true);
        if(credits_UI != null) credits_UI.gameObject.SetActive(true);
        if(options_UI != null) options_UI.gameObject.SetActive(true);


    }


    #region Showing / Hiding UI Screens

    public void ShowMainMenuUI()
    {
        HideAllMenus();
        mainMenu_UI.rootVisualElement.style.display = DisplayStyle.Flex; // Show Gameplay UI
    }

    public void ShowPausedUI()
    {
        HideAllMenus();
        paused_UI.rootVisualElement.style.display = DisplayStyle.Flex; // Show Paused UI
    }

    public void ShowGameplayUI()
    {
        HideAllMenus();
        gameplay_UI.rootVisualElement.style.display = DisplayStyle.Flex; // Show Gameplay UI
    }

    public void ShowLevelCompleteUI()
    {
        HideAllMenus();
        levelComplete_UI.rootVisualElement.style.display = DisplayStyle.Flex; // Show Level Complete UI
    }

    public void ShowLevelFailedUI()
    {
        HideAllMenus();
        levelFailed_UI.rootVisualElement.style.display = DisplayStyle.Flex; // Show Level Failed UI
    }

    public void ShowGameCompleteUI()
    {
        HideAllMenus();
        gameComplete_UI.rootVisualElement.style.display = DisplayStyle.Flex; // Show Game Complete UI
    }

    public void ShowCreditsUI()
    {
        HideAllMenus();
        credits_UI.rootVisualElement.style.display = DisplayStyle.Flex; // Show Credits UI
    }

    public void ShowOptionsUI()
        {
        HideAllMenus();
        options_UI.rootVisualElement.style.display = DisplayStyle.Flex; // Show Options UI
    }

    public void HideAllMenus()
    {

       if (mainMenu_UI == null) Debug.LogError("mainMenu_UI is null, please check the UIManager setup.");
       if (paused_UI == null) Debug.LogError("paused_UI is null, please check the UIManager setup.");
       if (gameplay_UI == null) Debug.LogError("gameplay_UI is null, please check the UIManager setup.");
       if (levelComplete_UI == null) Debug.LogError("levelComplete_UI is null, please check the UIManager setup.");
       if (levelFailed_UI == null) Debug.LogError("levelFailed_UI is null, please check the UIManager setup.");
       if (gameComplete_UI == null) Debug.LogError("gameComplete_UI is null, please check the UIManager setup.");
       if (credits_UI == null) Debug.LogError("credits_UI is null, please check the UIManager setup.");
       if (options_UI == null) Debug.LogError("options_UI is null, please check the UIManager setup.");



        mainMenu_UI.rootVisualElement.style.display = DisplayStyle.None;
        paused_UI.rootVisualElement.style.display = DisplayStyle.None;
        gameplay_UI.rootVisualElement.style.display = DisplayStyle.None;
        levelComplete_UI.rootVisualElement.style.display = DisplayStyle.None;
        levelFailed_UI.rootVisualElement.style.display = DisplayStyle.None;
        gameComplete_UI.rootVisualElement.style.display = DisplayStyle.None;
        credits_UI.rootVisualElement.style.display = DisplayStyle.None;
        options_UI.rootVisualElement.style.display = DisplayStyle.None;

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
