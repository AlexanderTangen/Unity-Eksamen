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
            instance = GetComponent<EventSystem>();
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}

