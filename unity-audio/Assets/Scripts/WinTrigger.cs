using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    public Timer timerScript;
    public TMP_Text timerText;
    public GameObject WinCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timerScript.enabled = false;
            timerText.fontSize = 65;
            timerText.color = Color.green;
            WinCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
