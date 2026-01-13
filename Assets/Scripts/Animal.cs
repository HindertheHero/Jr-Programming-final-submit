using UnityEngine;
using UnityEngine.AI;
    
[RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent))]

// Abstract base class for animals with wandering behavior
public abstract class Animal : MonoBehaviour
{
    public Rigidbody rb;
   
    // Encapsulation of NavMeshAgent
    protected NavMeshAgent agent;
   
    
    // Encapsulation of movement parameters
    [SerializeField] protected float moveSpeed = 1f;
    [SerializeField] protected float rotationSpeed = 75f; // degrees per second used by RotateTowards
    [SerializeField] private float wanderRadius = 10f;
    [SerializeField] private float wanderInterval = 3f;

    private float wanderTimer;

    // Enabling polymorphism for child classes
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            // Let the NavMeshAgent handle rotation automatically 
            agent.updatePosition = true;
            agent.updateRotation = true;
            agent.speed = moveSpeed;
            agent.angularSpeed = rotationSpeed;
            agent.acceleration = 2f;
        }

        // start with immediate destination pick
        wanderTimer = 0f;
        PickNewDestination();
    }

    protected virtual void Update()
    {
        Move();
    }


    // Abstraction of movement logic
    protected void Move() { 
          if (agent == null)
            return;

        
        agent.speed = moveSpeed;

        wanderTimer -= Time.deltaTime;

        // pick new destination when timer expires or agent reached current goal
        bool reached = !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
        if (wanderTimer <= 0f || reached)
        {
            PickNewDestination();
    wanderTimer = wanderInterval;
        }
    }

    private void PickNewDestination()
{
    if (RandomNavmeshLocation(transform.position, wanderRadius, out Vector3 samplePos))
    {
        agent.SetDestination(samplePos);
    }
}

private bool RandomNavmeshLocation(Vector3 origin, float radius, out Vector3 result)
{
    for (int i = 0; i < 30; i++)
    {
        Vector3 randomPoint = origin + Random.insideUnitSphere * radius;
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
    }

    result = transform.position;
    return false;
}
}

