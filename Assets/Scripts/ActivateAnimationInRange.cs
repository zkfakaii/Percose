using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAnimationOnRange : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador
    public Animator animator; // El Animator que controla las animaciones del objeto
    public string animationTrigger = "Activate"; // El nombre del trigger de la animaci�n
    public float detectionRange = 5f; // Rango de detecci�n para la interacci�n
    private bool isInRange = false; // Verifica si el jugador est� en rango

    void Update()
    {
        // Calculamos la distancia entre el jugador y el objeto
        float distance = Vector3.Distance(player.position, transform.position);

        // Verificamos si el jugador est� dentro del rango
        if (distance <= detectionRange)
        {
            isInRange = true;

            // Si se presiona la tecla '3' y el jugador est� en rango, activamos la animaci�n
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetTrigger(animationTrigger); // Activamos el trigger de la animaci�n
            }
        }
        else
        {
            isInRange = false; // Si no est� en rango, lo actualizamos
        }
    }

    // M�todo para visualizar el rango en la escena con Gizmos
    private void OnDrawGizmosSelected()
    {
        // Cambiamos el color a verde si el jugador est� en rango, rojo si no
        Gizmos.color = isInRange ? Color.green : Color.red;

        // Dibujamos una esfera alrededor del objeto para representar el rango de interacci�n
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
