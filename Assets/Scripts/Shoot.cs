using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    public float projectileSpeed = 50f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Fire projectile on left click
        if (Input.GetMouseButtonDown(0) && projectile != null)
        {


            GameObject spawnedProjectile = Instantiate(projectile, transform.position + transform.forward * 1f, transform.rotation);
            Rigidbody projectileRb = spawnedProjectile.GetComponent<Rigidbody>();
            if (projectileRb != null)
            {
                projectileRb.linearVelocity = transform.forward * projectileSpeed;
            }
        }
    }
}
