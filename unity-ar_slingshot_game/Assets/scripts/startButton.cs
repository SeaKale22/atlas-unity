using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startButton : MonoBehaviour
{

    public GameObject startCanvas;
    public GameManager GameManager;
    
    public void StartGame()
    {
        Debug.Log("Start button pushed");
        // call method to start game
        GameManager.StartGame();
        startCanvas.SetActive(false);
    }
}
