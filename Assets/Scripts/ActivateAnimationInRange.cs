using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAnimationOnRange : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador
    public Animator animator; // El Animator que controla las animaciones del objeto
    public string animationTrigger = "Activate"; // El nombre del trigger de la animación
    public float detectionRange = 5f; // Rango de detección para la interacción
    public bool isLocked = true; // Indica si el objeto está bloqueado
    [SerializeField] private string requiredItemName; // El nombre del objeto requerido para desbloquear (asignable desde el Inspector)

    private bool isInRange = false; // Verifica si el jugador está en rango

    void Update()
    {
        // Calculamos la distancia entre el jugador y el objeto
        float distance = Vector3.Distance(player.position, transform.position);

        // Verificamos si el objeto está bloqueado
        if (isLocked)
        {
            // Desactivamos el Animator si está bloqueado
            animator.enabled = false;
            isInRange = false; // Aseguramos que no se considere en rango

            // Comprobar si el jugador tiene el objeto requerido
            PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
            if (playerInventory != null && playerInventory.HasItem(requiredItemName))
            {
                isLocked = false; // Desbloquear el objeto
                Debug.Log($"{gameObject.name} ha sido desbloqueado por tener el objeto: {requiredItemName}");
            }
            return; // Salimos del método para no procesar la activación
        }
        else
        {
            // Si no está bloqueado, aseguramos que el Animator esté activo
            animator.enabled = true;
        }

        // Si el objeto no está bloqueado, verificamos el rango normalmente
        if (distance <= detectionRange)
        {
            isInRange = true;

            // Si se presiona la tecla 'E' y el jugador está en rango, activamos la animación
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
