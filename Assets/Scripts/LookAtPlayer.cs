using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private Transform player; // Referencia al jugador

    void Update()
    {
        // Obtener la dirección hacia el jugador en el plano horizontal (ignorar el eje Y)
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Mantener el texto en plano horizontal, sin inclinación vertical

        // Si la dirección no es cero, hacer que el texto mire hacia el jugador
        if (direction != Vector3.zero)
        {
            // Crear una rotación que mire hacia el jugador
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Rotar 180 grados en el eje Y para evitar que el texto esté volteado
            transform.rotation = targetRotation * Quaternion.Euler(0, 180, 0);
        }
    }
}
