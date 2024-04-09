using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    public static SceneTracker Instance;
    private int previousScene;

    private void Awake()
    {
        if (Instance != this && Instance!= null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Should only be called directly before changing scenes
    public void SetPreviousScene()
    {
        previousScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log($"scene set as previous scene in scene tracker. Index: {previousScene}");
    }
    
    // previous scene getter
    public int GetPreviousScene()
    {
        Debug.Log($"Returning Index {previousScene}");
        return previousScene;
    }
}
