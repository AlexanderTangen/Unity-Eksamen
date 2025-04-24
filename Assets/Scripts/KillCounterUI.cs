using UnityEngine;
using UnityEngine.UI; // Use legacy UI namespace

public class KillCounterUI : MonoBehaviour
{
    public Text killCountText; // Legacy Text component

    void Update()
    {
        if (GameManager.Instance != null && killCountText != null)
        {
            killCountText.text = $"Kills: {GameManager.Instance.GetKillCount()}";
        }
    }
}