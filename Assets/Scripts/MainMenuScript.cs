using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public string levelToLoad = "MainLevel";

    public void Play()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
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
