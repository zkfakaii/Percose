using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAnimationOnRange : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador
    public Animator animator; // El Animator que controla las animaciones del objeto
    public string animationTrigger = "Activate"; // El nombre del trigger de la animación
    public float detectionRange = 5f; // Rango de detección para la interacción
    private bool isInRange = false; // Verifica si el jugador está en rango

    void Update()
    {
        // Calculamos la distancia entre el jugador y el objeto
        float distance = Vector3.Distance(player.position, transform.position);

        // Verificamos si el jugador está dentro del rango
        if (distance <= detectionRange)
        {
            isInRange = true;

            // Si se presiona la tecla '3' y el jugador está en rango, activamos la animación
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetTrigger(animationTrigger); // Activamos el trigger de la animación
            }
        }
        else
        {
            isInRange = false; // Si no está en rango, lo actualizamos
        }
    }

    // Método para visualizar el rango en la escena con Gizmos
    private void OnDrawGizmosSelected()
    {
        // Cambiamos el color a verde si el jugador está en rango, rojo si no
        Gizmos.color = isInRange ? Color.green : Color.red;

        // Dibujamos una esfera alrededor del objeto para representar el rango de interacción
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
