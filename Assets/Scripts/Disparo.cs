using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Disparo : MonoBehaviour
{ 
    [SerializeField] private Transform playerT; // Reference to the player's transform
    [SerializeField] private GameObject projectilePrefab; // The prefab of the projectile to shoot
    [SerializeField] private float shootInterval = 2f; // Time between each shot
    [SerializeField] private float projectileSpeed = 10f; // Speed of the projectile
    [SerializeField] private float damageAmount = 20f; // Damage dealt by the projectile
    private NavMeshAgent navMeshAgent;
    private float timeSinceLastShot;

    void Start()
    {
        // Get the NavMeshAgent component
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Find the player transform
        playerT = FindAnyObjectByType<PlayerController>().transform;

        timeSinceLastShot = shootInterval; // So that the enemy can shoot immediately
    }

    void Update()
    {
        if (playerT == null) return; // Exit if there's no player reference

        // Update the time since the last shot
        timeSinceLastShot += Time.deltaTime;

        // Make the enemy face the player
        Vector3 directionToPlayer = playerT.position - transform.position;
        directionToPlayer.y = 0; // Ignore vertical rotation
        transform.rotation = Quaternion.LookRotation(directionToPlayer);

        // Check if it's time to shoot
        if (timeSinceLastShot >= shootInterval)
        {
            ShootAtPlayer();
            timeSinceLastShot = 0f; // Reset the shot timer
        }
    }

    private void ShootAtPlayer()
    {
        // Instantiate the projectile and set its initial position and rotation
        GameObject projectile = Instantiate(projectilePrefab, transform.position + Vector3.up, Quaternion.identity);

        // Calculate direction towards the player
        Vector3 shootDirection = (playerT.position - transform.position).normalized;

        // Set the projectile velocity
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.velocity = shootDirection * projectileSpeed;

        // Apply damage to the player when the projectile hits
        //projectile.GetComponent<Projectile>().SetDamage(damageAmount);
    }
}
