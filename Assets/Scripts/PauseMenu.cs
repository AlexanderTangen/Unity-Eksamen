using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    // Add references to the FirstPersonController and GunController scripts
    public FirstPersonController firstPersonController;  // Reference to FirstPersonController script
    public GunController gunController;  // Reference to GunController script

    void Start()
    {
        // Ensure the pause menu is disabled when the game starts
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        // Check for Escape key press to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    // Function to resume the game
    public void Resume()
    {
        pauseMenuUI.SetActive(false);  // Disable the pause menu
        Time.timeScale = 1f;  // Resume time
        isPaused = false;

        // Enable player controls and gun controls when resuming
        firstPersonController.enabled = true;
        gunController.enabled = true;

        // Resume audio playback
        AudioListener.pause = false;
    }

    // Function to pause the game
    public void Pause()
    {
        pauseMenuUI.SetActive(true);  // Enable the pause menu
        Time.timeScale = 0f;  // Pause time
        isPaused = true;

        // Disable player controls and gun controls when pausing
        firstPersonController.enabled = false;
        gunController.enabled = false;

        // Pause all audio playback
        AudioListener.pause = true;
    }

    // Function to quit the game (for when the player clicks 'Quit' in the pause menu)
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}