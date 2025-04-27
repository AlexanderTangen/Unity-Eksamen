using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.EventSystems;
using UnityEngine.UI;  // Add this for UI reference

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    // Add references to the FirstPersonController and GunController scripts
    public FirstPersonController firstPersonController;  // Reference to FirstPersonController script
    public GunController gunController;  // Reference to GunController script

    // Reference to the Resume Button
    public GameObject resumeButton;

    // Reference to the Crosshair GameObject (that contains KillCountTextLegacy, Healthbar, Image)
    public GameObject crosshair;  // Drag your Crosshair GameObject here

    // Add audio sources to mute during pause (optional)
    public AudioSource[] audioSources;

    void Start()
    {
        // Ensure the pause menu is disabled when the game starts
        pauseMenuUI.SetActive(false);

        // Initialize audio sources if necessary
        audioSources = FindObjectsOfType<AudioSource>();
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

        // Resume all audio playback
        AudioListener.pause = false;
        SetAudioMute(false);

        // Lock cursor and hide it (optional)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Enable the Crosshair
        if (crosshair != null)
        {
            crosshair.SetActive(true);  // Enable Crosshair GameObject
        }
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
        SetAudioMute(true);

        // Unlock the cursor and make it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disable the Crosshair GameObject while paused to allow button interaction
        if (crosshair != null)
        {
            crosshair.SetActive(false);  // Disable the entire Crosshair GameObject
        }

        // Ensure EventSystem is active and set the Resume button as selected
        EventSystem.current.SetSelectedGameObject(null);  // Reset selection
        EventSystem.current.SetSelectedGameObject(resumeButton);  // Select the resume button
    }

    // Function to mute/unmute all audio sources
    private void SetAudioMute(bool mute)
    {
        foreach (var audioSource in audioSources)
        {
            audioSource.mute = mute;
        }
    }

    // Function to quit the game (for when the player clicks 'Quit' in the pause menu)
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
