using UnityEngine;

public class GhostDamage : MonoBehaviour
{
    public float damagePerSecond = 5f;  // Damage dealt per second
    public float detectionRadius = 3f;  // Radius to check for player proximity
    private Transform player;
    private bool isPlayerNear = false;

    private void Start()
    {
        // Find the player by tag, ensure player has the tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < detectionRadius)
        {
            isPlayerNear = true;
        }
        else
        {
            isPlayerNear = false;
        }

        if (isPlayerNear)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}