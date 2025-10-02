using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelCompletedScript : MonoBehaviour
{
    public string nextLevel = "Level2";
    public int levelToUnlock = 2;

    public string menuSceneName = "MainMenu";

    public SceneFaderScript sceneFader;

    public Button continueButton;


    public void Continue()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo(nextLevel);
    }
    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (levelToUnlock > 5)
        {
            if (continueButton != null)
            {
                continueButton.interactable = false;
                TextMeshProUGUI btnText=continueButton.GetComponentInChildren<TextMeshProUGUI>();
                if(btnText != null)
                {
                    btnText.text = "Game completed!";
                    btnText.autoSizeTextContainer = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
