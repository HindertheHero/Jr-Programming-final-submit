using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Enemy;
    public int waveNumber = 1;
    public GameManager gameManager;

    void Start()
    {
        if (gameManager == null)
            gameManager = Object.FindFirstObjectByType<GameManager>();

        
    }

    void Update()
    {


        if (gameManager == null)
            gameManager = Object.FindFirstObjectByType<GameManager>();

        //Only spawn if the game is active
        if (!gameManager.isGameActive)
            return;



        // Spawn next wave when there are no enemies left
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }


    public void SpawnStart()
    { Instantiate(Enemy, new Vector3(Random.Range(-9, 9), 1, Random.Range(-9, 9)), Quaternion.identity); }
     public void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(Enemy, new Vector3(Random.Range(-9, 9), 1, Random.Range(-9, 9)), Quaternion.identity);
        }
    }
}
