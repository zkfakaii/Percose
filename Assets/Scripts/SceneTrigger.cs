using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    // Nombre de la escena a la que se quiere cambiar
    public string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        // Asegúrate de que el objeto que entra al trigger sea el correcto
        if (other.CompareTag("Player")) // Cambia "Player" al tag de tu objeto si es diferente
        {
            // Cargar la escena
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
