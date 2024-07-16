using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startButton : MonoBehaviour
{

    public GameObject startCanvas;
    public void TurnOffStartCanvas()
    {
        Debug.Log("Start button pushed");
        startCanvas.SetActive(false);
    }
}
