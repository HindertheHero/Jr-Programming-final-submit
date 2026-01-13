using UnityEngine;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        Debug.Log("Play button clicked");
        GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (gameManager != null)
        {
            gameManager.StartGame();
        }
        else
        {
            Debug.LogError("GameManager not found!");
        }
    }

}
