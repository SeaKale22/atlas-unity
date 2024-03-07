using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseCanvas;

    public CameraController CameraController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (PauseCanvas.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }   
    }

    void Pause()
    {
        Time.timeScale = 0;
        CameraController.moveCam = false;
        PauseCanvas.SetActive(true);
        
    }

    public void Resume()
    {
        Time.timeScale = 1;
        CameraController.moveCam = true;
        PauseCanvas.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneTracker.Instance.SetPreviousScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneTracker.Instance.SetPreviousScene();
        SceneManager.LoadScene(0);
    }
    
    public void Options()
    {
        Time.timeScale = 1;
        SceneTracker.Instance.SetPreviousScene();
        SceneManager.LoadScene(4);
    }
}
