using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject projectile;
    public float projectileSpeed = 10f;
    [SerializeField] private float spawnOffset = 1f; // spawn in front of shooter

    void Start()
    {
        // start the repeating shoot routine
        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            float shootInterval = Random.Range(2f, 5f);
            yield return new WaitForSeconds(shootInterval);
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        if (projectile == null)
        {
            Debug.LogError("EnemyProjectile: projectile prefab is not assigned.");
            return;
        }

        Vector3 spawnPos = transform.position + transform.forward * spawnOffset;
        GameObject spawned = Instantiate(projectile, spawnPos, transform.rotation);

        Rigidbody rb = spawned.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * projectileSpeed;
        }
        else
        {
            Debug.LogWarning("EnemyProjectile: spawned projectile is missing a Rigidbody. It was instantiated but won't move by physics.");
        }
    }
}


