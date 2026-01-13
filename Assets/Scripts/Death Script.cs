using UnityEngine;

public class DeathScript : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject explosionEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);

            Instantiate(explosionEffect, transform.position, transform.rotation);

            gameManager.UpdateScore(1);
        }
    }
}

   



