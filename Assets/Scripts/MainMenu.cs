using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    [Header("Texts")]
    [SerializeField] private Text startText;
    [SerializeField] private Text quitText;

    [Header("Hover Colors")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = Color.red;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);

        // Add hover listeners
        AddHoverEvents(startButton, startText);
        AddHoverEvents(quitButton, quitText);
    }

    private void AddHoverEvents(Button button, Text text)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = button.gameObject.AddComponent<EventTrigger>();

        trigger.triggers = new System.Collections.Generic.List<EventTrigger.Entry>();

        // OnPointerEnter
        EventTrigger.Entry entryEnter = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        entryEnter.callback.AddListener((eventData) => { text.color = hoverColor; });
        trigger.triggers.Add(entryEnter);

        // OnPointerExit
        EventTrigger.Entry entryExit = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        entryExit.callback.AddListener((eventData) => { text.color = normalColor; });
        trigger.triggers.Add(entryExit);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game"); // Load the Game scene by name
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    // Reset input and UI focus when transitioning to the MainMenu scene
    private void OnEnable()
    {
        // Add listener when the scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Remove listener when the scene is unloaded
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            ResetInput();
        }
    }

    private void ResetInput()
    {
        // Reset the EventSystem's selected object to ensure UI interaction works properly
        EventSystem.current.SetSelectedGameObject(null);

        // Unlock and show the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Ensure the Canvas is enabled
        Canvas mainMenuCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.enabled = true;
        }
    }
}
