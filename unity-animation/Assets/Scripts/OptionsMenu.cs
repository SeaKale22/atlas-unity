using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{

    public void Back()
    {
        Debug.Log("Attempting to load previous scene from scene tracker");
        int prevScene = SceneTracker.Instance.GetPreviousScene();
        Debug.Log($"Scene tracker returned index {prevScene}");
        SceneManager.LoadScene(prevScene);
        Debug.Log("Scene loaded successfully");
    }
    
}
