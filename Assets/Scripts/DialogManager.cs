using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private List<DialogTypewriterController> dialogBoxes; // Lista de cuadros de texto (dialog boxes)
    private int currentDialogIndex = 0; // Índice del cuadro de texto actual
    private bool dialogActive = false; // Estado del diálogo (si está activo o no)

    void Start()
    {
        // Desactivamos todos los cuadros de texto al inicio
        foreach (var dialogBox in dialogBoxes)
        {
            dialogBox.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Verificar si se presiona "E" para activar o desactivar el diálogo
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (dialogActive)
            {
                DeactivateDialog();
            }
            else
            {
                ActivateDialog();
            }
        }

        // Verificar si se presiona espacio y el cuadro actual ha terminado de mostrar el diálogo
        if (dialogActive && Input.GetKeyDown(KeyCode.Space))
        {
            // Preguntamos si el cuadro de texto actual ha terminado todas sus oraciones
            if (dialogBoxes[currentDialogIndex].IsDialogFinished())
            {
                // Avanzamos al siguiente cuadro de texto si ya terminó el actual
                ActivateNextDialogBox();
            }
            else
            {
                // Si no ha terminado, mostramos la siguiente oración del cuadro actual
                dialogBoxes[currentDialogIndex].DisplayNextSentence();
            }
        }
    }

    // Activa todo el diálogo desde el inicio
    private void ActivateDialog()
    {
        currentDialogIndex = 0;
        dialogActive = true;
        ActivateCurrentDialogBox(); // Activamos el primer cuadro de texto
    }

    // Desactiva todo el diálogo
    private void DeactivateDialog()
    {
        dialogActive = false;
        foreach (var dialogBox in dialogBoxes)
        {
            dialogBox.gameObject.SetActive(false); // Desactivar todos los cuadros de texto
        }
        gameObject.SetActive(false); // Desactivar el DialogManager
    }

    // Activa el cuadro de diálogo actual en la lista
    private void ActivateCurrentDialogBox()
    {
        dialogBoxes[currentDialogIndex].gameObject.SetActive(true);
        dialogBoxes[currentDialogIndex].StartDialog(); // Inicia el diálogo del cuadro actual
    }

    // Activa el siguiente cuadro de diálogo en la lista
    private void ActivateNextDialogBox()
    {
        // Avanzamos al siguiente cuadro de texto
        currentDialogIndex++;

        // Si aún hay más cuadros de texto en la lista, los activamos
        if (currentDialogIndex < dialogBoxes.Count)
        {
            ActivateCurrentDialogBox();
        }
        else
        {
            Debug.Log("Todos los cuadros de texto han terminado.");
            dialogActive = false;
            gameObject.SetActive(false); // Desactiva el objeto del DialogManager
        }
    }
}
