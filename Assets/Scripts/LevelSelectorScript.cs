using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorScript : MonoBehaviour
{

    public SceneFaderScript fader;

    public Button[] levelButtons;
    public void Select(string levelName)
    {
        fader.FadeTo(levelName);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i+1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
