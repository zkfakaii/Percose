using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f; // Max health for both player and enemies
    [SerializeField] private float timeToSelfDestructOnDeath = 1f; // Time before destruction
    [SerializeField] private UnityEvent onBeenHit;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private float currentHealth; // Show current health in Inspector

    void Start()
    {
        currentHealth = maxHealth; // Set current health to max at the start
    }

    // Method to apply damage to this object
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            onDeath?.Invoke(); // Trigger onDeath event

            // Check if the object has the "Enemy" tag, and destroy it after the set delay
            if (CompareTag("Enemy"))
            {
                Destroy(gameObject, timeToSelfDestructOnDeath); // Destroy after delay
            }
        }
        else
        {
            onBeenHit?.Invoke(); // Trigger onBeenHit event if not dead
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
    }

    // Optional: Method to check if the object is alive
    public bool IsAlive()
    {
        return currentHealth > 0f;
    }
}
