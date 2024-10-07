using UnityEngine;
using UnityEngine.AI;

public class EnemyRandomMovement : MonoBehaviour
{
    public float maxDistance = 5f; // Maximum distance from the player's position
    public float moveInterval = 2f; // Time interval for movement
    [SerializeField] Transform playerT;
    private NavMeshAgent navMeshAgent;
    [SerializeField] float moveRangePerFrame = 0.1f;
    [SerializeField] Vector2 maxYMovementRange;
    [SerializeField] float offsetY = 1;
    [SerializeField] float frequency = 1f; // Frequency of the sine wave
    [SerializeField] float amplitude = 0.5f; // Amplitude of the sine wave
    private float timeOffset; // Time offset for sine wave movement

    void Start()
    {
        playerT = FindAnyObjectByType<PlayerController>().transform;
        navMeshAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component

        // Start the movement coroutine
        InvokeRepeating(nameof(MoveToRandomPosition), 0, moveInterval);

        // Initialize time offset
        timeOffset = Random.Range(0f, Mathf.PI * 2f); // Add randomness to each enemy's wave pattern
    }

    public void InterruptMovement()
    {
        // Detener el NavMeshAgent
        navMeshAgent.isStopped = true; // Detener el movimiento
                                       // Puedes agregar más lógica si es necesario, como desactivar temporariamente el enemigo
    }


    void MoveToRandomPosition()
    {
        // Only move if the agent is not on an OffMeshLink
        if (!navMeshAgent.isOnOffMeshLink)
        {
            // Generate a random point within a sphere defined by maxDistance
            Vector3 randomDirection = Random.insideUnitSphere * maxDistance;

            // Calculate the new destination
            Vector3 newDestination = playerT.position + randomDirection;

            // Check if the destination is on the NavMesh
            NavMeshHit hit;

            if (NavMesh.SamplePosition(newDestination, out hit, maxDistance, NavMesh.AllAreas))
            {
                navMeshAgent.SetDestination(hit.position); // Set the destination to the hit position
               // Debug.Log("Destination set on NavMesh: " + hit.position);
            }
            else
            {
               // Debug.Log("Failed to find a valid position on the NavMesh.");
            }
        }
        
        else
        {
            // If the agent is on an OffMeshLink, complete the link and allow movement
            Debug.Log("Agent is on OffMeshLink. Completing the link.");
            navMeshAgent.CompleteOffMeshLink();
        }
    }

    void Update()
    {
        // Sinusoidal movement
        float sineValue = Mathf.Sin(Time.time * frequency + timeOffset) * amplitude;
        navMeshAgent.baseOffset = Mathf.Clamp(sineValue + offsetY, maxYMovementRange.x, maxYMovementRange.y);
        if (navMeshAgent.isOnOffMeshLink)
        {
            Debug.Log("Agent is traversing an OffMeshLink.");
        }
    }
}
