using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float healAmount = 20f; // Cantidad de vida a recuperar

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si el objeto que colisiona tiene un HealthSystem (como el jugador)
        HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();

        if (healthSystem != null)
        {
            healthSystem.Heal(healAmount); // Llama al método Heal para recuperar vida
            Destroy(gameObject); // Destruir el objeto de curación después de ser recogido
        }
    }
}
