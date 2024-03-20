using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject cutSceneCamera;
    public GameObject player;
    public GameObject timerCanvas;
    public Animator CutsceneCamAnimator;

    public void CutSceneOver()
    {
        Debug.Log("Cutscene over!");
        player.GetComponent<PlayerController>().enabled = true;
        cutSceneCamera.SetActive(false);
        mainCamera.SetActive(true);
        timerCanvas.SetActive(true);
    }
}
