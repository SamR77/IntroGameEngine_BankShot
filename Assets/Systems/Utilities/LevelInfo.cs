// Sam Robichaud 
// NSCC Truro 2025
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)


using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;


public class LevelInfo : MonoBehaviour
{
    public int ShotsToComplete;

    private void OnEnable()
    {
        StartCoroutine(WaitForGameManager());
    }

    private IEnumerator WaitForGameManager()
    {
        // Wait until GameManager is available (Bootstrap scene loaded)
        while (GameManager.Instance == null)
        {
            yield return null; // Wait one frame
        }

        if (ShotsToComplete > 0)
        {
            GameManager.Instance.shotsRemaining = ShotsToComplete;
        }
    }
}
