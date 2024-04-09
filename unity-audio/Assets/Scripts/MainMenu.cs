using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{ 
    // Loads scenes
    public void LevelSelect(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void quit()
    {
        Application.Quit();
        Debug.Log("Exited");
    }
    
    public void Options()
    {
        SceneTracker.Instance.SetPreviousScene();
        SceneManager.LoadScene(4);
    }
}
