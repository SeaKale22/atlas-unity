using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioSource buttonClick;
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

    public void Click()
    {
        buttonClick.Play();
    }

    
}
