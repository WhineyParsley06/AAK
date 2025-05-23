using UnityEngine;

// Controls door behavior: opening, closing, and collision checking
public class DoorController : MonoBehaviour
{
    public float moveSpeed = 2f;             // Speed at which the door moves
    public float distanceToMove = 4.5f;      // Distance the door travels when opening

    private bool shouldOpen = false;         // Flag to indicate door should open
    private bool shouldClose = false;        // Flag to indicate door should close

    private Vector3 targetPosition;          // Destination position when moving
    private Vector3 originalPosition;        // Initial position of the door
    private bool playerInDoorway = false;    // True if player is blocking the doorway

    void Start()
    {
        // Save the door's original starting position
        originalPosition = transform.position;

        // Uncomment this if you want the door to open on start (for testing)
        // OpenDoor();
    }

    // Triggers the door to open by calculating a new target position
    public void OpenDoor()
    {
        targetPosition = originalPosition + Vector3.left * distanceToMove;
        shouldOpen = true;
        shouldClose = false;
    }

    // Triggers the door to close, unless the player is in the way
    public void CloseDoor()
    {
        if (playerInDoorway)
        {
            Debug.Log("Player blocking the door – cannot close.");
            return; // Do not proceed with closing if blocked
        }

        targetPosition = originalPosition;
        shouldClose = true;
        shouldOpen = false;
    }

    // Handles door movement frame-by-frame
    void Update()
    {
        // Move only if opening or closing, and not blocked by the player
        if ((shouldOpen || shouldClose) && !playerInDoorway)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Stop moving once the door has reached its target
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                shouldOpen = false;
                shouldClose = false;
            }
        }
    }

    // Called when an object with tag "Player" enters the door's trigger area
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInDoorway = true;
        }
    }

    // Called when the player exits the door's trigger area
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInDoorway = false;
        }
    }
}
