using UnityEngine;

public class GameFunctionalityScript : MonoBehaviour
{

    private bool gameEnded = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded) //u koliko ovo ne uradimo, u konzoli ce se spamovati Game over!
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
        gameEnded = true;
        Debug.Log("Game over!");

    }
}
