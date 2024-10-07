using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab; // The projectile to instantiate
    [SerializeField] Transform firePoint; // The point from where the projectile is shot
    [SerializeField] float normalProjectileSpeed = 20f; // Normal speed of the projectile
    [SerializeField] float chargedProjectileSpeed = 20f; // Speed of the charged projectile
    [SerializeField] float fireRate = 0.5f; // Time between shots
    private float nextFireTime = 0f;
    [SerializeField] float bulletLife = 1;

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
        // Create a new projectile at the fire point's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Get the ChargedShot component (if you have it attached)
        ChargedShot chargedShot = projectile.GetComponent<ChargedShot>();

        // Set the projectile's speed
        float speed = isCharged ? chargedProjectileSpeed : normalProjectileSpeed;

        if (chargedShot != null)
        {
            chargedShot.SetSpeed(speed); // Set the speed on ChargedShot if it exists
        }

        // Get all Rigidbody components in the projectile and set velocity for the first one found
        Rigidbody[] rigidbodies = projectile.GetComponentsInChildren<Rigidbody>();

        if (rigidbodies.Length > 0)
        {
            rigidbodies[0].velocity = firePoint.forward * speed;
        }
        else
        {
            Debug.LogWarning("No Rigidbody found in the projectile or its children.");
        }

        Destroy(projectile, bulletLife);
    }
}
