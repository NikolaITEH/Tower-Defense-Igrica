using UnityEngine;

public class PlayerStatsScript : MonoBehaviour
{

    public static int gold;

    public int startGold=400;

    public static int lives;
    public int startLives = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gold = startGold;
        lives = startLives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
