using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float distanceToMove = 4.5f;

    private bool shouldOpen = false;
    private bool shouldClose = false;

    private Vector3 targetPosition;
    private Vector3 originalPosition;

    private bool initialized = false;

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
        targetPosition = originalPosition;
        shouldClose = true;
        shouldOpen = false;
    }

    void Update()
    {
        if (shouldOpen || shouldClose)
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
}
