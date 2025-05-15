using UnityEngine;

public class GhostDamage : MonoBehaviour
{
    public float damagePerSecond = 5f;
    public float detectionRadius = 3f;
    private Transform player;
    private bool isPlayerNear = false;

    private void Start()
    {
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