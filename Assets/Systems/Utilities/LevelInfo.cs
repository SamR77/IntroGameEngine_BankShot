// Sam Robichaud 
// NSCC Truro 2025
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public int ShotsToComplete;    

    private void OnEnable()
    {
        GameManager.Instance.shotsRemaining = ShotsToComplete;
    }


}
