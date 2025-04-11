using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;
    
    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    
    [Header("Settings")]
    [SerializeField] private string gameSceneName = "GameScene"; // Navnet på din spillscene
    
    private void Start()
    {
        // Legger til lyttere på knappene
        startGameButton.onClick.AddListener(StartGame);
        
        if (optionsButton != null)
            optionsButton.onClick.AddListener(ShowOptions);
        
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
            
        // Sørg for at hovedmenyen vises ved start
        ShowMainMenu();
    }
    
    public void StartGame()
    {
        // Last inn spillscenen med LoadSceneMode.Single for å sikre at alt fra forrige scene fjernes
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
    }
    
    public void ShowOptions()
    {
        // Vis innstillingspanelet og skjul hovedmenyen
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    
    public void ShowMainMenu()
    {
        // Vis hovedmenyen og skjul innstillingspanelet
        mainMenuPanel.SetActive(true);
        
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
    }
    
    public void QuitGame()
    {
        // Avslutt spillet (fungerer bare i bygget versjon, ikke i editor)
        Debug.Log("Quitting game...");
        Application.Quit();
        
        // For testing i Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}