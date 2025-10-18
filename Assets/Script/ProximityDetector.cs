using UnityEngine;

public class ProximityDetector : MonoBehaviour
{
    public float detectionRadius = 5f;
    private Transform playerTransform;

    void Start()
    {
        // Find the player object by its tag
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object with 'Player' tag not found!");
        }
    }

    void Update()
    {
        if (playerTransform == null)
        {
            return;
        }

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Check if the player is within the detection radius
        if (distanceToPlayer < detectionRadius)
        {
            Debug.Log("Player is near the object!");

            VaultDoor.InZone = true;
            // Add your custom logic here (e.g., show an interaction prompt)
        }
        else
        {
            Debug.Log("Player is NOT near the object!");

            VaultDoor.InZone = false;
            // Player is not near
        }
    }
}