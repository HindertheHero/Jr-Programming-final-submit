using UnityEngine;

public class PlayerControlller : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed ;
    public float jumpForce;
    public float sensitivityX = 3f;
    private GameManager gameManager;


    // How quickly the player rotates to match the camera's yaw
    public float rotationSpeed = 10f;

    // Animator to drive walking animation
    private Animator animator;
    // Animator parameter name (configure in Animator Controller)
    public string walkParameter = "isWalking";
    // Minimum input magnitude considered "moving"
    public float movementThreshold = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("PlayerControlller: No Animator found on player or children. Walking animation will not play.");
        }
    }

    // Update is called once per frame
    void Update()
    { //Moves player in Direction of the focal point of camera
       
       

        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Apply movement forces relative to the camera
        if (Camera.main != null)
        {
            playerRb.AddForce(Camera.main.transform.forward * verticalInput * speed);
            playerRb.AddForce(Camera.main.transform.right * horizontalInput * speed);
        }

        float mouseX = Input.GetAxis("Mouse X") * sensitivityX;

        // Rotate the player to match the camera's yaw (smooth)
        if (Camera.main != null)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Update walking animation state
        bool isMoving = Mathf.Abs(verticalInput) > movementThreshold || Mathf.Abs(horizontalInput) > movementThreshold;
        if (animator != null)
        {
            animator.SetBool(walkParameter, isMoving);
        }

        // Make player sprint
        if (Input.GetKey(KeyCode.LeftShift) && Camera.main != null)
            playerRb.AddForce(Camera.main.transform.forward * verticalInput * (speed * 1.5f) );

        // Make player jump
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(playerRb.linearVelocity.y) < 0.01f)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        

    }

}


