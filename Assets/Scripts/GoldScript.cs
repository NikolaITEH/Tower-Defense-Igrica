using UnityEngine;
using TMPro;
public class GoldScript : MonoBehaviour
{

    public TextMeshProUGUI goldText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text =PlayerStatsScript.gold.ToString() + " Gold";
    }
}
