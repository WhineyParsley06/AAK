using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Necesario para usar UI
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configuración")]
    float horizontal, vertical;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 7f;
    bool isGrounded = true;
    private Vector3 moveDirection;
    [SerializeField] private Rigidbody playerRigidbody;

    public bool Horse_Should_Move = true;
    public DoorController doorController; // Arrástralo desde el Inspector

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleInput();
        

        // Rotar hacia la dirección de movimiento si se está moviendo
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Saltar si se presiona la tecla y está en el suelo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            ApplyJump();
        }
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void HandleInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
    }

    void ApplyMovement()
    {
        Vector3 movement = moveDirection * moveSpeed * Time.fixedDeltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void ApplyJump()
    {
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }

      
    }
    private void OnCollisionExit(Collision other)
    {

        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BotonSuelo"))
        {
            Debug.Log("Boton Suelo Activado por Trigger");
            Horse_Should_Move = false;
            Vector3 currentPosition = other.transform.position;
            other.transform.position = new Vector3(currentPosition.x, -0.30f, currentPosition.z);
            //DoorController door = GameObject.Find("Door").GetComponent<DoorController>();
            if (doorController != null)
            {
                doorController.OpenDoor();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BotonSuelo"))
        {
            Horse_Should_Move = true;
            Vector3 currentPosition = other.transform.position;
            other.transform.position = new Vector3(currentPosition.x, 0.36f, currentPosition.z); // vuelve a subir
            if (doorController != null)
            {
                doorController.CloseDoor();
            }
        }
    }

}
