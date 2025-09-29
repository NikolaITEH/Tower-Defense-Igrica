using System.Collections;
using TMPro;
using UnityEngine;

public class RoundsSurvivedScript : MonoBehaviour
{

    public TextMeshProUGUI roundsText;

    private void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        roundsText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(0.7f);

        while (round < PlayerStatsScript.Rounds)
        {
            round++;
            roundsText.text=round.ToString();
            yield return new WaitForSeconds(.05f);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
