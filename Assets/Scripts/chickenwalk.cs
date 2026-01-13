using UnityEngine;

public class chickenwalk : MonoBehaviour
{
    private Rigidbody chickRb;
    private GameObject player;
    public float speed;
    public float stoppingDistance = 25f;
    [SerializeField] private float rotationSpeed = 8f;   // how quickly the enemy turns to face the player
    [Header("Wander (transform)")]
    [SerializeField] private float wanderRadius = 5f;    // radius for random target selection
    [SerializeField] private float wanderInterval = 2f;  // seconds between picking new targets
    [SerializeField] private float wanderSpeed = 2f;     // speed used when moving transform position
    [SerializeField] private bool useKinematicWhileWandering = true; // avoid physics conflicts when moving transform
    private Vector3 wanderTarget;
    private float wanderTimer;
    public float jump = 30f;
    private float jumpTimer;
   


    // configurable parameters
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotationJitterDegrees = 20f;
    [SerializeField] private float maxForwardSpeed = 3f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float minJumpInterval = 5f;
    [SerializeField] private float maxJumpInterval = 7f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chickRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (chickRb == null || player == null)
            return;

        // Rotate to face the player (y-axis only to avoid tilting)
        Vector3 directionToPlayer = player.transform.position - transform.position;
        directionToPlayer.y = 0f; // keep rotation on horizontal plane
        if (directionToPlayer.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Determine distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Wander by modifying transform.position (random movement)
        if (useKinematicWhileWandering && !chickRb.isKinematic)
        {
            chickRb.isKinematic = true;
        }

        wanderTimer -= Time.deltaTime;

        // Pick a new random wander target periodically or when reached
        if (wanderTimer <= 0f || Vector3.Distance(transform.position, wanderTarget) < 0.1f)
        {
            wanderTarget = transform.position + Random.insideUnitSphere * wanderRadius;
            wanderTarget.y = transform.position.y; // keep same height
            wanderTimer = wanderInterval;
        }

        // Move transform smoothly toward the wander target
        transform.position = Vector3.MoveTowards(transform.position, wanderTarget, wanderSpeed * Time.deltaTime);

        //Randomly jumps every 5 to 7 seconds but only if on the ground
        jumpTimer = Random.Range(minJumpInterval, maxJumpInterval);
        jumpTimer -= Time.deltaTime;

        if (jumpTimer <= 0f)
        {
            chickRb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            

        }
    }
}

    
    

  
   

