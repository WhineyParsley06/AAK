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
}
