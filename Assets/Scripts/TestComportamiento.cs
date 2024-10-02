using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class TestComportamiento : MonoBehaviour
{
   /* public float maxDistance = 5f; // Maximum distance from the player's position
    public float moveInterval = 2f; // Time interval for movement
    private NavMeshAgent navMeshAgent;
    [SerializeField] Transform playerT;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
        playerT = FindObjectOfType<PlayerController>().transform; // Find the player automatically

        // Start the movement coroutine
        InvokeRepeating(nameof(MoveToRandomPosition), 0, moveInterval);
    }

    void MoveToRandomPosition()
    {
        // Generate a random direction in the vicinity of the player
        Vector3 randomDirection = Random.insideUnitSphere * maxDistance;

        // Make sure movement is only in XZ plane (horizontal)
        randomDirection.y = 0;

        // Calculate the new destination based on the player's position and random direction
        Vector3 newDestination = playerT.position + randomDirection;

        // Check if the destination is valid and on the NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(newDestination, out hit, maxDistance, NavMesh.AllAreas))
        {
            // Move towards the player with some randomization
            navMeshAgent.SetDestination(hit.position);
        }
    }

    void Update()
    {
        // Check if the enemy is using an OffMeshLink
        if (navMeshAgent.isOnOffMeshLink)
        {
            Debug.Log("Enemy has detected an OffMeshLink!"); // Debug to check detection
            StartCoroutine(HandleOffMeshLink());
        }
    }

    IEnumerator HandleOffMeshLink()
    {
        // Perform custom logic when traversing the OffMeshLink
        OffMeshLinkData offMeshLinkData = navMeshAgent.currentOffMeshLinkData; // Correct data type
        Vector3 startPos = navMeshAgent.transform.position;
        Vector3 endPos = offMeshLinkData.endPos + Vector3.up * navMeshAgent.baseOffset;

        // Debug the OffMeshLink positions for further inspection
        Debug.Log($"OffMeshLink Start Position: {startPos}");
        Debug.Log($"OffMeshLink End Position: {endPos}");

        // You can create a custom animation or movement here
        float duration = 1.0f;
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            navMeshAgent.transform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Complete the traversal
        navMeshAgent.CompleteOffMeshLink();
    }*/
}
