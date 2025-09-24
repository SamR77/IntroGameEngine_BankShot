using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField] private UIDocument gameplayUIDoc => GetComponent<UIDocument>();

    [SerializeField] private Label shotsRemainingLabel => gameplayUIDoc.rootVisualElement.Q<Label>("ShotsRemainingLabel");
    [SerializeField] private Label levelCountLabel => gameplayUIDoc.rootVisualElement.Q<Label>("LevelCountLabel");

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



}
