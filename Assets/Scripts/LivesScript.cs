using UnityEngine;
using TMPro;


public class LivesScript : MonoBehaviour
{

    public TextMeshProUGUI livesText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int lives=PlayerStatsScript.lives;
        if (lives == 1)
        {
            livesText.text = lives + " Life";
        }
        else
        {
            livesText.text = lives + " Lives";
        }
    }
}
