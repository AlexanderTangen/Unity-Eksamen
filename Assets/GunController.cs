using UnityEngine;

public class GunController1 : MonoBehaviour
{
    public float range = 100f; // Shooting distance
    public Camera fpsCamera; // Assign this in Unity (your main camera)

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
            Debug.Log("Hit: " + hit.transform.name); // Print object hit
        }
    }
}