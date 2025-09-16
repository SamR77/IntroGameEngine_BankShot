// Sam Robichaud 
// NSCC Truro 2025
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

using System;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class LevelManager : MonoBehaviour
{
    // have a Script attached to Parent Object that Holds levels
    // Will scan through its children and find all instances of LevelInfo
    // Will load each Level into an array
    // Level Manager will connect to Parent Level Object, and... then use that reference to load levels?

    [SerializeField] private Transform levelsParent;
    public GameObject[] levels;

    private int currentLevelIndex = -1; // -1 = none active


    private void Awake()
    {

        // Add all levels under levelsParent to levels[] array
        int count = levelsParent.childCount;
        levels = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            levels[i] = levelsParent.GetChild(i).gameObject;
            levels[i].SetActive(false);
        }
    }

    // Deactivate all levels
    public void DeactivateAllLevels()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
    }

    // Activate a specific level (index) after deactivating everything
    public void ActivateLevel(int index)
    {
        DeactivateAllLevels();

        if (index >= 0 && index < levels.Length)
        {
            levels[index].SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Level index {index} out of range!");
        }
    }

    public void ActivateNextLevel()
    {
        int nextIndex = currentLevelIndex + 1;

        // wrap around or clamp depending on desired behaviour:
        if (nextIndex >= levels.Length)
        {
            Debug.Log("No more levels!");
            return;
        }

        ActivateLevel(nextIndex);
    }

    public void ResetCurrentLevel()
    {
        DeactivateAllLevels();
        ActivateLevel(currentLevelIndex);
    }



}
