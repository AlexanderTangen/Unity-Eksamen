using UnityEngine;
using System.Collections;

public class AlienHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public float respawnDelay = 3f;

    private AlienAI alienAI;
    private Renderer[] renderers;
    private Collider[] colliders;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        currentHealth = maxHealth;

        originalPosition = transform.position;
        originalRotation = transform.rotation;

        alienAI = GetComponent<AlienAI>();
        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();
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
        if (alienAI != null) alienAI.enabled = false;
        foreach (var r in renderers) r.enabled = false;
        foreach (var c in colliders) c.enabled = false;

        StartCoroutine(RespawnAfterDelay());
    }

    IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);

        // Move back to original spawn point
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        currentHealth = maxHealth;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        foreach (var r in renderers) r.enabled = true;
        foreach (var c in colliders) c.enabled = true;
        if (alienAI != null) alienAI.enabled = true;

        Debug.Log($"{gameObject.name} respawned at original location!");
    }
}

