using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAnimationOnRange : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador
    public Animator animator; // El Animator que controla las animaciones del objeto
    public string animationTrigger = "Activate"; // El nombre del trigger de la animaci�n
    public float detectionRange = 5f; // Rango de detecci�n para la interacci�n
    public bool isLocked = true; // Indica si el objeto est� bloqueado
    [SerializeField] private string requiredItemName; // El nombre del objeto requerido para desbloquear (asignable desde el Inspector)

    private bool isInRange = false; // Verifica si el jugador est� en rango

    void Update()
    {
        // Calculamos la distancia entre el jugador y el objeto
        float distance = Vector3.Distance(player.position, transform.position);

        // Verificamos si el objeto est� bloqueado
        if (isLocked)
        {
            // Desactivamos el Animator si est� bloqueado
            animator.enabled = false;
            isInRange = false; // Aseguramos que no se considere en rango

            // Comprobar si el jugador tiene el objeto requerido
            PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
            if (playerInventory != null && playerInventory.HasItem(requiredItemName))
            {
                isLocked = false; // Desbloquear el objeto
                Debug.Log($"{gameObject.name} ha sido desbloqueado por tener el objeto: {requiredItemName}");
            }
            return; // Salimos del m�todo para no procesar la activaci�n
        }
        else
        {
            // Si no est� bloqueado, aseguramos que el Animator est� activo
            animator.enabled = true;
        }

        // Si el objeto no est� bloqueado, verificamos el rango normalmente
        if (distance <= detectionRange)
        {
            isInRange = true;

            // Si se presiona la tecla 'E' y el jugador est� en rango, activamos la animaci�n
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
