using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    // Lifetime in seconds before the projectile self-destructs
    public float lifetime = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (lifetime > 0f)
            Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}

   

