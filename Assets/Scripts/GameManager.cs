using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;
    public TextMeshProUGUI gameOverText;
    public bool isGameActive;
    public TextMeshProUGUI restartButton;
    public Button restartBtn;
    public GameObject titleScreen;
    public GameObject Enemy;
    public SpawnManager spawnManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGameActive = false;


    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartGame()
    {         // Implementation for starting the game
        isGameActive = true;
        score = 0;
        UpdateScore(0);

        titleScreen.gameObject.SetActive(false);

        spawnManager = FindFirstObjectByType<SpawnManager>();
        
        Cursor.lockState = CursorLockMode.Locked;
       
        spawnManager.SpawnStart();

    }

    public void GameOver()
    {
        // Handles game over state
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;

        restartBtn.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateScore(int scoreToAdd)
    {
        // Updates the player's score and displays it
        score += scoreToAdd;
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}
