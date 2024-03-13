using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    
        // Loads scenes
        public void LevelSelect(int level)
        {
            SceneManager.LoadScene(level);
            Time.timeScale = 1;
        }

        public void quit()
        {
            Time.timeScale = 1;
            Application.Quit();
            Debug.Log("Exited");
        }
}
