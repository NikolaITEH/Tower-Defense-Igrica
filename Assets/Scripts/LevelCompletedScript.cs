using UnityEngine;

public class LevelCompletedScript : MonoBehaviour
{
    public string nextLevel = "Level2";
    public int levelToUnlock = 2;

    public string menuSceneName = "MainMenu";

    public SceneFaderScript sceneFader;


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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
