using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemCleaner : MonoBehaviour
{
    private static EventSystem instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<EventSystem>();
            // No more DontDestroyOnLoad here!
            instance = GetComponent<EventSystem>();
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate EventSystem
        }
    }
}

