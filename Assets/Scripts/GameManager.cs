using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int ghostKillCount = 0;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: keep across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterGhostKill()
    {
        ghostKillCount++;
        Debug.Log($"Ghost killed! Total kills: {ghostKillCount}");
    }

    public int GetKillCount()
    {
        return ghostKillCount;
    }
}