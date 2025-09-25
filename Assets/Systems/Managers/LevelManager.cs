// Sam Robichaud 
// NSCC Truro 2025
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

using UnityEngine;
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

    // Scenes Id's
    // 0 = Bootstrap Scene
    // 1 = Main Menu
    // 2+ = Gameplay Levels



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


    // this is the main Method that handles scene loading
    // all scene changes should go through this method

    private void LoadScene(int sceneId)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneId);
    }




    /*  old method
    public void LoadScene(int sceneId)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneId);
    }
    */

    #region Scene Loaders

    // publicly accessible methods to handle scene loading

    public void LoadMainMenu()
    {
        LoadScene(1);
        Debug.Log("Loading Main Menu...");
        gameStateManager.SwitchToState(GameState_MainMenu.Instance);
    }

    public void LoadFirstLevel()
    {
        LoadScene(2);
        //gameStateManager.SwitchToState(GameState_Aim.Instance);
    }

    public void ReloadCurrentScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
       

        // this corrects an issue when scene is reloaded, input stops responding... re-initializes the Input Map?
        // InputManager.instance.SetActionMap_Gameplay();
    }

    #endregion



    // TODO: Move to GameStateManager?
    public void QuitGame()
    {
        Application.Quit();
    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int LevelCount = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log("Scene Loaded: " + scene.name + " Build Index: " + scene.buildIndex);

        if (scene.buildIndex == 1)
        { 
            // main menu scene
            gameStateManager.SwitchToState(GameState_MainMenu.Instance);

        }

        else if (scene.buildIndex > 1)
        {   
            gameStateManager.SwitchToState(GameState_Aim.Instance);

            // Update the current level # on the UI
            uIManager.GameplayUIController.UpdateShotsRemainingLabel();

            // uIManager.UpdateLevelCount(LevelCount);
            uIManager.GameplayUIController.SetLevelLabel(SceneManager.GetActiveScene().buildIndex);

            // Set the ball to the current level start position           
            ballManager.SetBallToStartPosition();
            //Debug.Break();

            // Set the camera to the current level start position
            cameraManager.ResetCameraPosition();
        }

      
        // (Unsuscribe) Stop listening for sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


}
