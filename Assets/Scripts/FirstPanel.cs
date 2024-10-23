using UnityEngine;

public class ActivatePanelOnStart : MonoBehaviour
{
    [SerializeField] private GameObject panelToActivate; // Panel que se activar� al iniciar la escena

    void Start()
    {
        if (panelToActivate != null)
        {
            panelToActivate.SetActive(true); // Activa el panel cuando inicie la escena
        }
        else
        {
            Debug.LogWarning("No se ha asignado ning�n panel en el Inspector.");
        }
    }
}
