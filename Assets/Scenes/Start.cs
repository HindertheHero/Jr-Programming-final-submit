using UnityEngine;

public class Start : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void EnterGame()
    {         // Load the main game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("My Game");
    }
}
