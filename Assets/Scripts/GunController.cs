using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour
{
    public float range = 100f;
    public Camera fpsCamera; 
    public GameObject bulletTrailPrefab; 
    public Transform firePoint; 

    public AudioClip gunshotSound; // 🎵 Add this
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // 🔊 Get the AudioSource on start
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 🔫 Play the gunshot sound
        if (gunshotSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(gunshotSound);
        }

        RaycastHit hit;
        Vector3 targetPoint = fpsCamera.transform.position + fpsCamera.transform.forward * range;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            targetPoint = hit.point; 
            Debug.Log("Hit: " + hit.transform.name);

            if (hit.transform.CompareTag("Destructible"))
            {
                Destroy(hit.transform.gameObject); 
            }
        }

        StartCoroutine(SpawnBulletTrail(targetPoint));
    }

    IEnumerator SpawnBulletTrail(Vector3 target)
    {
        GameObject trail = Instantiate(bulletTrailPrefab, firePoint.position, Quaternion.identity);
        TrailRenderer trailRenderer = trail.GetComponent<TrailRenderer>();

        Vector3 startPosition = firePoint.position;
        float duration = 0.1f; 
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            trail.transform.position = Vector3.Lerp(startPosition, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        trail.transform.position = target;
        Destroy(trail, trailRenderer.time); 
    }
}