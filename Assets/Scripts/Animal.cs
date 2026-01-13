using UnityEngine;
using UnityEngine.AI;
    
[RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent))]
public abstract class Animal : MonoBehaviour
{
    public Rigidbody rb;
    protected NavMeshAgent agent;

    [SerializeField] protected float moveSpeed = 1f;
    [SerializeField] protected float rotationSpeed = 75f; // degrees per second used by RotateTowards
    [SerializeField] private float wanderRadius = 10f;
    [SerializeField] private float wanderInterval = 3f;

    private float wanderTimer;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            // let the agent drive position but we handle Y-axis rotation manually
            agent.updatePosition = true;
            agent.updateRotation = false;
            agent.updateUpAxis = true;
            agent.speed = moveSpeed;
            agent.angularSpeed = rotationSpeed;
            agent.acceleration = 2f;
        }

        wanderTimer = Random.Range(0f, wanderInterval);
        PickNewDestination();
    }

    protected virtual void Update()
    {
        if (agent == null)
            return;

        // Update agent speed in case inspector changed it at runtime
        agent.speed = moveSpeed;

        wanderTimer -= Time.deltaTime;

        // If timer expired or agent reached destination, pick a new random destination
        if (wanderTimer <= 0f || (!agent.pathPending && agent.remainingDistance <= 0.5f))
        {
            PickNewDestination();
            wanderTimer = wanderInterval;
        }

        // Rotate on Y axis to face movement direction
        Vector3 horizontalVelocity = agent.velocity;
        horizontalVelocity.y = 0f;

        if (horizontalVelocity.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(horizontalVelocity);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void PickNewDestination()
    {
        Vector3 samplePos;
        if (RandomNavmeshLocation(transform.position, wanderRadius, out samplePos))
        {
            agent.SetDestination(samplePos);
        }
    }

    private bool RandomNavmeshLocation(Vector3 origin, float radius, out Vector3 result)
    {
        for (int i = 0; i < 30; i++) // try a few times to find a valid sample
        {
            Vector3 randomPoint = origin + Random.insideUnitSphere * radius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        // fallback to current position
        result = transform.position;
        return false;
    }
}

