using UnityEngine;

public class ChargedShot : MonoBehaviour
{
    public float damage = 50f; // Daño del disparo
    public float duration = 2f; // Duración de la carga
    private float speed; // Velocidad del proyectil

    // Método para configurar la velocidad
    public void SetSpeed(float projectileSpeed)
    {
        speed = projectileSpeed;
        // Si tienes un Rigidbody, puedes configurar la velocidad aquí si lo deseas
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
            // Obtén la referencia al script Boid
            Boid boid = other.GetComponent<Boid>();
            if (boid != null)
            {
                // Llama al método que reduce la velocidad del boid
                
                boid.ReduceSpeed();
                // Aquí puedes agregar cualquier otra lógica, como aplicar daño
                // boid.ApplyDamage(damage); // Si implementas un método de daño en Boid
            }

            // Destruir el disparo después de la colisión (opcional)
           
        }
    }
}
