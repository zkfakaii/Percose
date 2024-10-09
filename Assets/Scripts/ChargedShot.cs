using UnityEngine;

public class ChargedShot : MonoBehaviour
{
    public float damage = 50f; // Da�o del disparo
    public float duration = 2f; // Duraci�n de la carga
    private float speed; // Velocidad del proyectil

    // M�todo para configurar la velocidad
    public void SetSpeed(float projectileSpeed)
    {
        speed = projectileSpeed;
        // Si tienes un Rigidbody, puedes configurar la velocidad aqu� si lo deseas
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * speed; // Configura la velocidad inicial del proyectil
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto con el que colisiona es un Boid
        if (other.CompareTag("Boid"))
        {
            // Obt�n la referencia al script Boid
            Boid boid = other.GetComponent<Boid>();
            if (boid != null)
            {
                // Llama al m�todo que reduce la velocidad del boid
                
                boid.ReduceSpeed();
                // Aqu� puedes agregar cualquier otra l�gica, como aplicar da�o
                // boid.ApplyDamage(damage); // Si implementas un m�todo de da�o en Boid
            }

            // Destruir el disparo despu�s de la colisi�n (opcional)
           
        }
    }
}
