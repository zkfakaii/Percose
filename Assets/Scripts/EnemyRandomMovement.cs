using UnityEngine;
using UnityEngine.AI;

public class EnemyRandomMovement : MonoBehaviour
{
    public float maxDistance = 5f; // Maximum distance from the initial position
    public float moveInterval = 2f; // Time interval for movement
    private Vector3 initialPosition;
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
        initialPosition = transform.position; // Store the initial position
        navMeshAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component

        // Start the movement coroutine
        InvokeRepeating(nameof(MoveToRandomPosition), 0, moveInterval);

        // Initialize time offset
        timeOffset = Random.Range(0f, Mathf.PI * 2f); // Add randomness to each enemy's wave pattern
    }

    void MoveToRandomPosition()
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
        }
    }

    public void Update()
    {
        // Sinusoidal movement
        float sineValue = Mathf.Sin(Time.time * frequency + timeOffset) * amplitude;
        navMeshAgent.baseOffset = Mathf.Clamp(sineValue + offsetY, maxYMovementRange.x, maxYMovementRange.y);
    }
}
