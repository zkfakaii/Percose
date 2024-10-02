using System.Collections;
using UnityEngine;

public class SidescrollerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;              // Velocidad de movimiento del personaje
    public string animatorParamName = "IsMoving"; // Nombre del par�metro en el Animator
    public float comboDelay = 0.5f;          // Tiempo en segundos para permitir el siguiente ataque en el combo

    private Rigidbody rb;
    private Animator animator;
    private Vector3 movement;
    private Vector3 originalScale;

    private float lastAttackTime; // Tiempo del �ltimo ataque
    private int comboStep;        // Paso actual del combo

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale; // Guarda la escala original del personaje
        comboStep = 0; // Inicializa el paso del combo
    }

    void Update()
    {
        // Captura la entrada del usuario para el movimiento en X y Z
        float moveX = Input.GetAxis("Horizontal"); // Input de teclado o controlador
        float moveZ = Input.GetAxis("Vertical");   // Input de teclado o controlador

        // Crea el vector de movimiento basado en las entradas
        movement = new Vector3(moveX, 0f, moveZ).normalized;

        // Actualiza el valor del par�metro en el Animator
        if (movement.magnitude > 0.1f)
        {
            animator.SetFloat(animatorParamName, 1f); // Se mueve
        }
        else
        {
            animator.SetFloat(animatorParamName, 0f); // Se detiene
        }

        // Ajusta la escala del personaje seg�n la direcci�n en X y Z
        Vector3 newScale = originalScale; // Inicia con la escala original

        if (moveX != 0)
        {
            newScale.x = Mathf.Sign(moveX) * Mathf.Abs(originalScale.x); // Ajusta la escala en X
        }

        if (moveZ != 0)
        {
            newScale.z = Mathf.Sign(moveZ) * Mathf.Abs(originalScale.z); // Ajusta la escala en Z
        }

        transform.localScale = newScale; // Aplica la nueva escala

        // Maneja la entrada para el combo de ataques
        if (Input.GetKeyDown(KeyCode.F))
        {
            HandleCombo();
        }
    }

    void FixedUpdate()
    {
        // Aplica la fuerza para mover el Rigidbody
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void HandleCombo()
    {
        float currentTime = Time.time;

        // Si ha pasado suficiente tiempo desde el �ltimo ataque, reinicia el combo
        if (currentTime - lastAttackTime > comboDelay)
        {
            comboStep = 0;
        }

        // Ejecuta el ataque correspondiente seg�n el paso del combo
        switch (comboStep)
        {
            case 0:
                // Realiza el primer ataque del combo
                animator.SetBool("Combo1", true);
                break;
            case 1:
                // Realiza el segundo ataque del combo
                animator.SetBool("Combo2", true);
                break;
            case 2:
                // Realiza el tercer ataque del combo
                animator.SetBool("Combo3", true);
                break;
        }

        // Actualiza el paso del combo y el tiempo del �ltimo ataque
        comboStep = (comboStep + 1) % 3; // Cambia al siguiente ataque en el combo (0, 1, 2, 0, 1, 2, ...)
        lastAttackTime = currentTime;
    }

    private void ResetComboBools()
    {
        // Espera el tiempo necesario para que las animaciones se reproduzcan completamente
        animator.SetBool("Combo1", false);
        animator.SetBool("Combo2", false);
        animator.SetBool("Combo3", false);
    }
}
