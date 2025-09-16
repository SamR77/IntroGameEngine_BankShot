// Sam Robichaud 
// NSCC Truro 2025
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

using System;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    GameManager gameManager => GameManager.Instance;
    BallManager ballManager => GameManager.Instance.BallManager;
    CameraManager cameraManager => GameManager.Instance.CameraManager;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;
    InputManager inputManager => GameManager.Instance.InputManager;
    UIManager uIManager => GameManager.Instance.UIManager;

    private int nextScene;

    public void LoadNextLevel()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene <= SceneManager.sceneCountInBuildSettings)
        {
            LoadScene(nextScene);
        }

        else if (nextScene > SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("All levels complete!");
        }
    }

    public void LoadScene(int sceneId)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneId);


        if (sceneId == 0) // Loading Main Menu
        {
            gameStateManager.SwitchToState(GameState_MainMenu.Instance);
        }
        else // Gameplay level
        {
            gameStateManager.SwitchToState(GameState_Aim.Instance);
        }
    }

    public void LoadMainMenuScene()
    {
        LoadScene(0);
        gameStateManager.SwitchToState(GameState_Init.Instance);
    }

    public void ReloadCurrentScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameStateManager.SwitchToState(GameState_Aim.Instance);

        // this corrects an issue when scene is reloaded, input stops responding... re-initializes the Input Map?
        // InputManager.instance.SetActionMap_Gameplay();
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int LevelCount = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log("Scene Loaded: " + scene.name + " Build Index: " + scene.buildIndex);

        if (scene.buildIndex > 0)
        {
            // Get a reference to the level info Script for that level
            LevelInfo levelInfo = FindObjectOfType<LevelInfo>();

            // Get the # shots(attempts) available for the level and update the UI
            gameManager.shotsLeft = levelInfo.ShotsToComplete;
            // uIManager.UpdateShotsleft(levelInfo.ShotsToComplete);

            // Update the current level # on the UI
            // uIManager.UpdateLevelCount(LevelCount);

            // Set the ball to the current level start position           
            ballManager.SetBallToStartPosition();
            //Debug.Break();

            // Set the camera to the current level start position
            cameraManager.ResetCameraPosition();
        }

        else if (scene.buildIndex == 0)
        {
            // Noting really needed here, buildIndex 0 = MainMenu scene,
            // Which would be loaded with via 'LoadMainMenuScene' which also switches to the GameInitState which handles all the prep/resetting for the MainMenu.
            // Leaving this here in case of debugging or future use.
        }
        // (Unsuscribe) Stop listening for sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


}
