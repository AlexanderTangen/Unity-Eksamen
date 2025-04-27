using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        // Ensure only one instance exists (Singleton pattern)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep it across scenes (but we'll control when it's destroyed)
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

    public void RegisterGhostKill()
    {
        // Your logic here...
    }

    public int GetKillCount()
    {
        return 0; // Your kill count logic...
    }

    // Optionally, listen for when we go back to the MainMenu and clean up
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            Destroy(gameObject); // Destroy GameManager when we load the MainMenu
        }
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}