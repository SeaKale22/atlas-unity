using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startButton : MonoBehaviour
{

    public GameObject startCanvas;
    public GameObject gameCanvas;
    public void TurnOffStartCanvas()
    {
        Debug.Log("Start button pushed");
        gameCanvas.SetActive(true);
        startCanvas.SetActive(false);
    }
}
