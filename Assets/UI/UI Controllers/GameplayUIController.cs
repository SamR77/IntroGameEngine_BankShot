using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class GameplayUIController : MonoBehaviour
{
    [SerializeField] private UIDocument gameplayUIDoc => GetComponent<UIDocument>();

    [SerializeField] private Label shotsRemainingLabel => gameplayUIDoc.rootVisualElement.Q<Label>("ShotsRemainingLabel");
    [SerializeField] private Label levelCountLabel => gameplayUIDoc.rootVisualElement.Q<Label>("LevelCountLabel");


    [Header("Manager References")]
    GameManager gameManager => GameManager.Instance;
    InputManager inputManager => GameManager.Instance.InputManager;

    VisualElement KeyboardMouseInfo => gameplayUIDoc.rootVisualElement.Q<VisualElement>("KeyboardMouse");
    VisualElement GamepadInfo => gameplayUIDoc.rootVisualElement.Q<VisualElement>("Gamepad");


    private void Start()
    {
        if (gameplayUIDoc == null)
        {
            Debug.LogError("gameplayUIDoc not found!");
            return;
        }

        //UpdateShotsRemainingLabel();
        //SetLevelLabel(SceneManager.GetActiveScene().buildIndex);

        StartCoroutine(InitializeUI());
    }

    private IEnumerator InitializeUI()
    {
        // Wait until GameManager is available and configured
        while (GameManager.Instance == null)
        {
            yield return null;
        }

        // Wait one additional frame to ensure LevelInfo has updated the shots
        yield return null;

        UpdateShotsRemainingLabel();
        SetLevelLabel(SceneManager.GetActiveScene().buildIndex);
        KeyboardMouseInfo.style.display = DisplayStyle.Flex;
        GamepadInfo.style.display = DisplayStyle.None;
    }

    public void UpdateShotsRemainingLabel()
    {
        if (shotsRemainingLabel == null)
        {
            Debug.LogError("shotsRemainingLabel not found!");
            return;
        }
        shotsRemainingLabel.text = $"Shots Remaining: {GameManager.Instance.shotsRemaining}";
    }

    public void SetLevelLabel(int levelIndex)
    {
        if (levelCountLabel == null)
        {
            Debug.LogError("levelCountLabel not found!");
            return;
        }
        levelCountLabel.text = $"Level {levelIndex - 1}";
    }



    private void UpdateInputUI(InputManager.InputDeviceType deviceType)
    {
        switch (deviceType)
        {
            case InputManager.InputDeviceType.KeyboardAndMouse:
                // Update UI for keyboard and mouse
                KeyboardMouseInfo.style.display = DisplayStyle.Flex;
                GamepadInfo.style.display = DisplayStyle.None;


                Debug.Log("Show UI Prompts for Keyboard and mouse");
                break;

            case InputManager.InputDeviceType.Gamepad:
                // Update UI for gamepad
                KeyboardMouseInfo.style.display = DisplayStyle.None;
                GamepadInfo.style.display = DisplayStyle.Flex;

                Debug.Log("Show UI Prompts for Gamepad");
                break;

            default:
                Debug.Log("Show non-input specific UI prompts");
                break;

        }
    }


    void OnEnable()
    {
        inputManager.InputDeviceChanged += UpdateInputUI;
    }


    void OnDestroy()
    {
        inputManager.InputDeviceChanged -= UpdateInputUI;
    }


}
