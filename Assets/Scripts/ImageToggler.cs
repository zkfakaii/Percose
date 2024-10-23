using UnityEngine;
using UnityEngine.UI;

public class ImageToggle : MonoBehaviour
{
    [SerializeField] private Image imageToToggle; // La imagen que queremos activar/desactivar

    void Update()
    {
        // Buscamos si hay un DialogManager activo en la escena
        DialogManager activeDialogManager = FindObjectOfType<DialogManager>();

        // Si hay un DialogManager activo, desactivamos la imagen
        if (activeDialogManager != null && activeDialogManager.gameObject.activeSelf)
        {
            imageToToggle.gameObject.SetActive(false);
        }
        else
        {
            // Si no hay DialogManager activo, activamos la imagen
            imageToToggle.gameObject.SetActive(true);
        }
    }
}
