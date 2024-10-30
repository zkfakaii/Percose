using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private string itemName; // Nombre o tipo del objeto recogible

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si el objeto que colisiona es el jugador
        PlayerInventory playerInventory = collision.gameObject.GetComponent<PlayerInventory>();

        if (playerInventory != null)
        {
            // Añadir el objeto al inventario del jugador
            playerInventory.AddItem(itemName);

            // Destruir el objeto de la escena
            Destroy(gameObject);
        }
    }
}
