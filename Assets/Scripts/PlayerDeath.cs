using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject deathEffect;

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
        if (other.gameObject.CompareTag("EnemyProjectile"))
        {
            Destroy(gameObject);

            Instantiate(deathEffect, transform.position, transform.rotation);

            gameManager.GameOver();
        }
    }
}
