using UnityEngine;

public class ActivatePanelOnStart : MonoBehaviour
{
    [SerializeField] private GameObject panelToActivate; // Panel que se activará al iniciar la escena

    void Start()
    {
        if (panelToActivate != null)
        {
            panelToActivate.SetActive(true); // Activa el panel cuando inicie la escena
        }
        else
        {
            Debug.LogWarning("No se ha asignado ningún panel en el Inspector.");
        }
    }
}
