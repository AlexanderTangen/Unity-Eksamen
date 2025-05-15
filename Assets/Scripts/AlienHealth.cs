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

    // Grønn flash effect 
    public Color hitColor = new Color(0f, 1f, 0f, 0.25f); 
    public float flashDuration = 0.1f; 
    public float emissionIntensity = 0.1f; 

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
        
        alienMaterials = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            alienMaterials[i] = renderers[i].material;
        }
        originalColor = alienMaterials[0].color;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage! HP left: {currentHealth}");
        
        StartCoroutine(FlashColor());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashColor()
    {
        foreach (Material mat in alienMaterials)
        {
            mat.color = hitColor;
            mat.SetColor("_EmissionColor", hitColor * emissionIntensity);
            mat.EnableKeyword("_EMISSION");
        }
        yield return new WaitForSeconds(flashDuration);
        
        foreach (Material mat in alienMaterials)
        {
            mat.color = originalColor;
            mat.SetColor("_EmissionColor", Color.black);
            mat.DisableKeyword("_EMISSION");
        }
    }

    void Die()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterGhostKill();
        }

        if (alienAI != null) alienAI.enabled = false;
        foreach (var r in renderers) r.enabled = false;
        foreach (var c in colliders) c.enabled = false;

        StartCoroutine(RespawnAfterDelay());
    }

    IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        
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
