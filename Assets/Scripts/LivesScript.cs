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
        livesText.text = PlayerStatsScript.lives.ToString() + " Lives";
    }
}
