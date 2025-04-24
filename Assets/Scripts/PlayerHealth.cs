using UnityEngine;
using UnityEngine.SceneManagement;  // For loading the main menu

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Here you can load the main menu or restart the game
        Debug.Log("Player Died! Returning to Main Menu.");
        SceneManager.LoadScene("MainMenu");  // Assuming your main menu is named "MainMenu"
    }
}