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

    // For color and emission flash effect
    public Color hitColor = new Color(0f, 1f, 0f, 0.25f); // Dimmed green color with less intensity
    public float flashDuration = 0.1f; // Duration of the flash
    public float emissionIntensity = 0.1f; // Lower intensity for a more subtle glow

    private Material[] alienMaterials;
    private Color originalColor;

    void Start()
    {
        currentHealth = maxHealth;

        originalPosition = transform.position;
        originalRotation = transform.rotation;

        alienAI = GetComponent<AlienAI>();
        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();

        // Get materials from all renderers
        alienMaterials = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            alienMaterials[i] = renderers[i].material;
        }

        // Store the original color of the materials
        originalColor = alienMaterials[0].color;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage! HP left: {currentHealth}");

        // Flash the color and glow green
        StartCoroutine(FlashColor());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashColor()
    {
        // Set the flash color and lower the emission intensity (subtle glow)
        foreach (Material mat in alienMaterials)
        {
            mat.color = hitColor; // Apply the flash color
            mat.SetColor("_EmissionColor", hitColor * emissionIntensity); // Apply dimmed emission
            mat.EnableKeyword("_EMISSION"); // Ensure emission is enabled
        }

        // Wait for the flash duration
        yield return new WaitForSeconds(flashDuration);

        // Reset color and turn off emission
        foreach (Material mat in alienMaterials)
        {
            mat.color = originalColor; // Restore original color
            mat.SetColor("_EmissionColor", Color.black); // Turn off emission
            mat.DisableKeyword("_EMISSION"); // Disable emission keyword
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


