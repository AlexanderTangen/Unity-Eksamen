using UnityEngine;

public class GunController : MonoBehaviour
{
    public float range = 100f; // Shooting distance
    public Camera fpsCamera; // Camera for the shooting view

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
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name); // Log the object hit

            // If the object has a health script or tag you want to destroy, add that here
            Destroy(hit.transform.gameObject); // This will destroy the object that was hit
        }
    }
}