using UnityEngine;
using UnityEngine.SceneManagement;

// Loads Bootstrap scene if it's not already loaded

[DefaultExecutionOrder(-100)]
public static class PerformBootstrap
{
    const string sceneName = "Bootstrap";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            // Check all currently loaded scenes to see if the bootstrap scene is already loaded
            for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; sceneIndex++)
            {
                var candidateScene = SceneManager.GetSceneAt(sceneIndex);

                // if bootstrap scene is already loaded, do nothing
                if (candidateScene.name == sceneName)
                {
                    return;
                }
            }

            Debug.Log("Loading Bootstrap scene" + sceneName);

            // if we get here, the bootstrap scene is not loaded, so load it (additively)
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }
}

public class BootstrappedData : MonoBehaviour
{
    public static BootstrappedData Instance { get; private set; } = null;

    private void Awake()
    {
        #region Singleton
        // Singleton pattern to ensure only one instance of GameManager exists
        if (Instance != null)
        {
            Debug.LogWarning("Another instance of BootstrapData already exists. Destroying this one.");
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        Debug.Log("Bootstrap Initialized.");

    }
    #endregion



    public void Test()
    {
        Debug.Log("BootstrapData is working");
    }

}