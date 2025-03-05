using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour
{
    public float range = 100f; // Shooting distance
    public Camera fpsCamera; // Camera for shooting
    public GameObject bulletTrailPrefab; // Bullet trail prefab
    public Transform firePoint; // Fire point of the gun

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Left mouse click
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        Vector3 targetPoint = fpsCamera.transform.position + fpsCamera.transform.forward * range;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            targetPoint = hit.point; // If we hit something, update the target point
            Debug.Log("Hit: " + hit.transform.name);

            if (hit.transform.CompareTag("Destructible"))
            {
                Destroy(hit.transform.gameObject); // Destroy objects with the "Destructible" tag
            }
        }

        StartCoroutine(SpawnBulletTrail(targetPoint));
    }

    IEnumerator SpawnBulletTrail(Vector3 target)
    {
        GameObject trail = Instantiate(bulletTrailPrefab, firePoint.position, Quaternion.identity);
        TrailRenderer trailRenderer = trail.GetComponent<TrailRenderer>();

        Vector3 startPosition = firePoint.position;
        float duration = 0.1f; // Time it takes for the trail to reach the target
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            trail.transform.position = Vector3.Lerp(startPosition, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        trail.transform.position = target;
        Destroy(trail, trailRenderer.time); // Destroy trail after it fades
    }
}