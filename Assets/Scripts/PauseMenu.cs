using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    
    public FirstPersonController firstPersonController;
    public GunController gunController;
    
    public GameObject resumeButton;

    // Crosshair
    public GameObject crosshair;
    
    public AudioSource[] audioSources;
    
    public AudioListener mainAudioListener;

    void Start()
    {
        // Pause meny deaktivert når spillet starter
        pauseMenuUI.SetActive(false);
        audioSources = FindObjectsOfType<AudioSource>();
        mainAudioListener = FindObjectOfType<AudioListener>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    // Fortsette spill funksjon 
    public void Resume()
    {
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f;  
        isPaused = false;
        
        firstPersonController.enabled = true;
        gunController.enabled = true;
        
        ResumeAllSounds();

        // Låse og skjule musepeker
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        if (crosshair != null)
        {
            crosshair.SetActive(true); 
        }
    }

    // Pause spill
    public void Pause()
    {
        pauseMenuUI.SetActive(true);  
        Time.timeScale = 0f;  
        isPaused = true;
        
        firstPersonController.enabled = false;
        gunController.enabled = false;
        
        PauseAllSounds();
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        if (crosshair != null)
        {
            crosshair.SetActive(false);  
        }
        
        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(resumeButton);  
    }
    
    private void PauseAllSounds()
    {
        if (mainAudioListener != null)
        {
            AudioListener.pause = true; 
        }
        
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (var source in allAudioSources)
        {
            if (source.isPlaying)
            {
                source.Pause(); 
            }
        }
    }
    private void ResumeAllSounds()
    {
        if (mainAudioListener != null)
        {
            AudioListener.pause = false; 
        }

        // Gjennopta lyd
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (var source in allAudioSources)
        {
            if (!source.isPlaying)
            {
                source.UnPause();  
            }
        }
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}

