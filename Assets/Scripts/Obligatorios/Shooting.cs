using UnityEngine;
using Cinemachine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab; // The projectile to instantiate
    [SerializeField] Transform firePoint; // The point from where the projectile is shot
    [SerializeField] float projectileSpeed = 20f; // Speed of the projectile
    [SerializeField] float fireRate = 0.5f; // Time between shots
    private float nextFireTime = 0f;
    [SerializeField] float bulletLife = 1;

    void Update()
    {
        if (Input.GetButton("Fire2") && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Create a new projectile at the fire point's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Get all Rigidbody components in the projectile and set velocity for the first one found
        Rigidbody[] rigidbodies = projectile.GetComponentsInChildren<Rigidbody>();

        if (rigidbodies.Length > 0)
        {
            // Set the velocity for the first Rigidbody found
            rigidbodies[0].velocity = firePoint.forward * projectileSpeed;
        }
        else
        {
            Debug.LogWarning("No Rigidbody found in the projectile or its children.");
        }

        Destroy(projectile, bulletLife);
    }
}
