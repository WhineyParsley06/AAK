using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float distanceToMove = 4.5f; // se moverá 7 unidades a la izquierda
    private bool shouldOpen = false;
    private Vector3 targetPosition;
    private bool initialized = false;

    void Start()
    {
        // Llama automáticamente al iniciar para probar el movimiento
        OpenDoor();
    }

    public void OpenDoor()
    {
        if (!initialized)
        {
            targetPosition = transform.position + Vector3.left * distanceToMove;
            initialized = true;
        }

        shouldOpen = true;
    }

    void Update()
    {
        if (shouldOpen)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
