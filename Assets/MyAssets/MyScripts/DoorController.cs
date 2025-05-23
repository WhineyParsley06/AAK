using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float distanceToMove = 4.5f;

    private bool shouldOpen = false;
    private bool shouldClose = false;

    private Vector3 targetPosition;
    private Vector3 originalPosition;
    private bool playerInDoorway = false;



    void Start()
    {
        originalPosition = transform.position;

        // Solo para prueba, abre al iniciar
        //OpenDoor();
    }

    public void OpenDoor()
    {
        targetPosition = originalPosition + Vector3.left * distanceToMove;
        shouldOpen = true;
        shouldClose = false;
    }

    public void CloseDoor()
    {
        if (playerInDoorway)
        {
            Debug.Log("Player blocking the door – cannot close.");
            return;
        }

        targetPosition = originalPosition;
        shouldClose = true;
        shouldOpen = false;
    }

    void Update()
    {
        if ((shouldOpen || shouldClose) && !playerInDoorway)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Cuando llega a destino, detener movimiento
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                shouldOpen = false;
                shouldClose = false;
            }
        }
    }


    // Detect player inside the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInDoorway = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInDoorway = false;
        }
    }
}
