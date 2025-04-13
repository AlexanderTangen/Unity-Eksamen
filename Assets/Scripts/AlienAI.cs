using UnityEngine;
using UnityEngine.AI;

public class AlienAI : MonoBehaviour
{
    public float chaseRange = 15f;         // Distance at which alien starts chasing
    public float stopDistance = 1.5f;      // Distance at which alien stops chasing
    public float wanderRadius = 10f;       // How far it can wander
    public float wanderTimer = 5f;         // How often it picks a new wander point

    private Transform player;
    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        timer = wanderTimer;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseRange)
        {
            if (distance > stopDistance)
            {
                agent.SetDestination(player.position);  // Chase the player
            }
            else
            {
                agent.ResetPath();  // Stop when too close
                // You could add attack animation here
            }
        }
        else
        {
            Wander();
        }
    }

    void Wander()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}