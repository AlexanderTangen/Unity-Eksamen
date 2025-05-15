using UnityEngine;
using UnityEngine.UI;

public class KillCounterUI : MonoBehaviour
{
    public Text killCountText;

    void Update()
    {
        if (GameManager.Instance != null && killCountText != null)
        {
            killCountText.text = $"Kills: {GameManager.Instance.GetKillCount()}";
        }
    }
}