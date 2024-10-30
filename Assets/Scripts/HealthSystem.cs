using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f; // Vida m�xima
    [SerializeField] private float timeToSelfDestructOnDeath = 1f; // Tiempo antes de destruirse
    [SerializeField] private UnityEvent onBeenHit;
    [SerializeField] private UnityEvent onDeath;

    [Header("Drop Settings")]
    [SerializeField] private bool shouldDropItem = true; // Si dropea o no
    [SerializeField] private GameObject itemToDrop; // Objeto a dropear
    [SerializeField] private int dropQuantity = 1; // Cantidad de objetos a dropear
    [SerializeField][Range(0f, 1f)] private float dropChance = 0.5f; // Probabilidad de dropear

    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Vida actual al m�ximo al inicio
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        Debug.Log($"{gameObject.name} ha recibido {damage} de da�o. Vida actual: {currentHealth}");

        if (currentHealth <= 0f)
        {
            onDeath?.Invoke(); // Llamada al evento de muerte
            DropItem(); // Intento de dropear el �tem
            Destroy(gameObject, timeToSelfDestructOnDeath); // Destruir despu�s del tiempo de delay
        }
        else
        {
            onBeenHit?.Invoke(); // Evento de recibir da�o si no est� muerto
        }
    }

    private void DropItem()
    {
        // Verificar si debe dropear el objeto y si cumple la probabilidad de dropeo
        if (shouldDropItem && itemToDrop != null && Random.value <= dropChance)
        {
            for (int i = 0; i < dropQuantity; i++)
            {
                // Instanciar el objeto en la posici�n del enemigo
                Instantiate(itemToDrop, transform.position, Quaternion.identity);
            }
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
    }

    public bool IsAlive()
    {
        return currentHealth > 0f;
    }
}
