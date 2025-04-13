using UnityEngine;

public class AlienHealth : MonoBehaviour
{
    public int maxHealth = 3; // 👾 Set how much HP the alien has
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage! HP left: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Optional: Add effects, animation, sound here
        Destroy(gameObject);
    }
}

