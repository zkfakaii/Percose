using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplayOnRange : MonoBehaviour
{
    [SerializeField] private GameObject textObject; // El objeto de texto 3D
    [SerializeField] private Transform player; // Referencia al jugador
    [SerializeField] private float displayRange = 5f; // Rango en el que el texto se mostrará

    private void Start()
    {
        // Asegurarse de que el texto esté oculto al inicio
        textObject.SetActive(false);
    }

    private void Update()
    {
        // Calcular la distancia entre el jugador y el objeto que contiene el texto
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // Mostrar o esconder el texto dependiendo de la distancia
        if (distanceToPlayer <= displayRange)
        {
            textObject.SetActive(true); // Mostrar el texto si está dentro del rango
        }
        else
        {
            textObject.SetActive(false); // Esconder el texto si está fuera del rango
        }
    }

    // Visualización del rango en la escena con un gizmo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, displayRange);
    }
}

