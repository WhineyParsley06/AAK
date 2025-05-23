using UnityEngine;
using UnityEngine.SceneManagement;

// Handles player movement, jumping, and interaction with triggers and collisions
public class PlayerMovement : MonoBehaviour
{
    [Header("Configuration")]
    float horizontal, vertical;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 7f;
    private bool isGrounded;
    private Vector3 moveDirection;

    [SerializeField] private Rigidbody playerRigidbody;

    public bool Horse_Should_Move = true;
    public DoorController doorController; // Assigned via Inspector
    private Color originalColor;
    public GameObject suelo; // Reference to the floor material

    private int jumpCount = 0;
    public int maxJumps = 2; // Supports double jump

    // Called on game start
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();

        // Save the original floor color
        if (suelo != null)
        {
            MeshRenderer renderer = suelo.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                originalColor = renderer.material.color;
            }
        }
    }

    // Called every frame
    void Update()
    {
        HandleInput();

        // Rotate player toward movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Handle jumping (with double jump)
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            ApplyJump();
            jumpCount++;
        }

        // Restart scene if player falls off the map
        if (transform.position.y < -5f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Called every physics frame
    void FixedUpdate()
    {
        ApplyMovement();
    }

    // Reads movement input
    void HandleInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
    }

    // Applies physical movement using Rigidbody
    void ApplyMovement()
    {
        Vector3 movement = moveDirection * moveSpeed * Time.fixedDeltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    // Applies upward force to simulate jumping and plays sound
    void ApplyJump()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.JumpSound);
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // Called when colliding with floor to reset jump count
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    // Called when player leaves floor collision
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }

    // Called when player enters a trigger area
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BotonSuelo"))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.ButtonPressedCLip);
            Debug.Log("Floor Button Trigger Activated");

            Horse_Should_Move = false;

            // Simulate button press
            Vector3 currentPosition = other.transform.position;
            other.transform.position = new Vector3(currentPosition.x, -0.80f, currentPosition.z);

            if (doorController != null)
            {
                doorController.OpenDoor();
            }
        }

        // Change floor color to yellow on trigger
        if (other.gameObject.CompareTag("YellowColor"))
        {
            if (suelo != null)
            {
                MeshRenderer renderer = suelo.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.yellow;
                }
            }
        }

        // Change floor color to red on trigger
        if (other.gameObject.CompareTag("RedColor"))
        {
            if (suelo != null)
            {
                MeshRenderer renderer = suelo.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.red;
                }
            }
        }
    }

    // Called when player exits a trigger area
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BotonSuelo"))
        {
            Horse_Should_Move = true;

            // Simulate button release
            Vector3 currentPosition = other.transform.position;
            other.transform.position = new Vector3(currentPosition.x, -0.5f, currentPosition.z);

            if (doorController != null)
            {
                doorController.CloseDoor();
            }
        }

        // Reset floor color when leaving trigger
        if (other.gameObject.CompareTag("YellowColor") || other.gameObject.CompareTag("RedColor"))
        {
            if (suelo != null)
            {
                MeshRenderer renderer = suelo.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    renderer.material.color = originalColor;
                }
            }
        }
    }
}
