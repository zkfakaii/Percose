using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject normalProjectilePrefab; // Prefab for the normal projectile
    [SerializeField] GameObject chargedProjectilePrefab; // Prefab for the charged projectile
    [SerializeField] Transform firePoint; // The point from where the projectile is shot
    [SerializeField] float normalProjectileSpeed = 20f; // Speed of the normal projectile
    [SerializeField] float chargedProjectileSpeed = 20f; // Speed of the charged projectile
    [SerializeField] float fireRate = 0.5f; // Time between shots
    private float nextFireTime = 0f;
    [SerializeField] float bulletLife = 1f;

    [SerializeField] float chargeTimeThreshold = 2f; // Time needed to hold the button for a charged shot
    private float chargeTime = 0f;
    private bool isCharging = false; // To track if we're currently charging

    void Update()
    {
        // Check if the fire button is pressed
        if (Input.GetButton("Fire2"))
        {
            if (!isCharging)
            {
                isCharging = true; // Start charging
                chargeTime = 0f; // Reset charge time
            }
            chargeTime += Time.deltaTime; // Increment charge time
        }
        else
        {
            if (isCharging)
            {
                isCharging = false; // Stop charging

                // Only fire a shot if enough time has passed since the last shot
                if (Time.time > nextFireTime)
                {
                    bool isCharged = chargeTime >= chargeTimeThreshold;
                    Shoot(isCharged); // Fire charged or normal shot based on charge time
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
    }

    void Shoot(bool isCharged)
    {
        GameObject projectilePrefab = isCharged ? chargedProjectilePrefab : normalProjectilePrefab;
        float speed = isCharged ? chargedProjectileSpeed : normalProjectileSpeed;

        // Debugging for charged shots
        if (isCharged)
        {
            Debug.Log("Disparando un ChargedShot.");
        }
        else
        {
            Debug.Log("Disparando un disparo normal.");
        }

        // Create a new projectile at the fire point's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody from the main projectile object and set its velocity
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        if (projectileRb != null)
        {
            projectileRb.velocity = firePoint.forward * speed;
        }
        else
        {
            Debug.LogWarning("No Rigidbody found on the projectile.");
        }

        // Destroy the projectile after the specified lifetime
        Destroy(projectile, bulletLife);
    }
}
