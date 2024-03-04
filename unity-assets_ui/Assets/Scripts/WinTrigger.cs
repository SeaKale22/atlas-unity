using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinTrigger : MonoBehaviour
{
    public Timer timerScript;
    public TMP_Text timerText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timerScript.enabled = false;
            timerText.fontSize = 65;
            timerText.color = Color.green;
        }
    }
}
