using UnityEngine;

public class GameFunctionalityScript : MonoBehaviour
{

    public static bool gameIsOver;

    public GameObject gameOverUI;

    public GameObject levelCompleteUI;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameIsOver = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsOver) //u koliko ovo ne uradimo, u konzoli ce se spamovati Game over!
        {
            return;
        }

        if (PlayerStatsScript.lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameIsOver = true;
        gameOverUI.SetActive(true);

    }

    public void WinLevel()
    {
        gameIsOver = true;
        levelCompleteUI.SetActive(true);
    }
}
