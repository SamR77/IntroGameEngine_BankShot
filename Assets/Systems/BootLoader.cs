using UnityEngine;
using UnityEngine.SceneManagement;

// Loads BootLoader scene if it's not already loaded

[DefaultExecutionOrder(-100)]
public static class PerformBootLoad
{
    const string sceneName = "BootLoader";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            // Check all currently loaded scenes to see if the bootstrap scene is already loaded
            for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; sceneIndex++)
            {
                var candidateScene = SceneManager.GetSceneAt(sceneIndex);

                // if BootLoader scene is already loaded, do nothing
                if (candidateScene.name == sceneName)
                {
                    return;
                }
            }

            Debug.Log("Loading BootLoader scene" + sceneName);

            // if we get here, the bootstrap scene is not loaded, so load it (additively)
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }
}

public class BootLoader : MonoBehaviour
{
    public static BootLoader Instance { get; private set; } = null;

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

        Debug.Log("BootLoader Initialized.");


        #endregion
    }


    public void Test()
    {
        Debug.Log("BootLoader Scene is active");
    }

}