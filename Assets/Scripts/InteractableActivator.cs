
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableActivator : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate; // Objeto que se activará
    [SerializeField] private float detectionRadius = 5f; // Rango de detección
    [SerializeField] private LayerMask playerLayer; // Capa del jugador

    private bool isPlayerInRange = false; // Indica si el jugador está en rango

    void Update()
    {
        // Verificar si el jugador está en rango y presiona la tecla E
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ActivateObject();
        }

        // Comprobar si el jugador está dentro del rango de detección
        CheckPlayerInRange();
    }

    // Método para activar el objeto asignado
    private void ActivateObject()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true); // Activa el objeto
            Debug.Log($"{objectToActivate.name} ha sido activado."); // Mensaje de confirmación
        }
        else
        {
            Debug.LogWarning("No se ha asignado un objeto para activar.");
        }
    }

    // Método para verificar si el jugador está dentro del rango
    private void CheckPlayerInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
        isPlayerInRange = hitColliders.Length > 0;
    }

    // Detectar cuando el jugador entra en el rango
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tenga el tag "Player"
        {
            isPlayerInRange = true; // El jugador está en rango
            Debug.Log("Presiona 'E' para activar el objeto."); // Mensaje opcional
        }
    }

    // Detectar cuando el jugador sale del rango
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // El jugador salió del rango
        }
    }

    // Dibujar el gizmo en la escena
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // Dibuja el rango de detección
    }
}
