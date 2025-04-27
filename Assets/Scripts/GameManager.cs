using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int ghostKillCount = 0;  // This will track the number of kills

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

    // Method to register a kill (increment the kill count)
    public void RegisterGhostKill()
    {
        ghostKillCount++; // Increment kill count when a ghost is killed
    }

    // Get the current kill count
    public int GetKillCount()
    {
        return ghostKillCount;
    }

    // Optionally, listen for when we go back to the MainMenu and clean up
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            Destroy(gameObject); // Destroy GameManager when we load the MainMenu
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}