using UnityEngine;
using UnityEngine.UIElements;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField] private UIDocument gameplayUIDoc => GetComponent<UIDocument>();

    [SerializeField] private Label shotsRemainingLabel => gameplayUIDoc.rootVisualElement.Q<Label>("ShotsRemainingLabel");
    [SerializeField] private Label levelCountLabel => gameplayUIDoc.rootVisualElement.Q<Label>("LevelCountLabel");

    private void Awake()
    {
        if (gameplayUIDoc == null)
        {
            Debug.LogError("gameplayUIDoc not found!");
            return;
        }
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
        levelCountLabel.text = $"Level {levelIndex}";
    }



}
