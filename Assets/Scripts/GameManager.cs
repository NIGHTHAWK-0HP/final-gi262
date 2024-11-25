using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    void Update()
        {
            // Check if ESC key is pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Load the main menu scene (make sure the scene is added in Build Settings)
                SceneManager.LoadScene("MainMenu");
            }
            // Check if the "R" key is pressed
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartCurrentScene();
            }
        }
    void RestartCurrentScene()
    {
        // Get the currently active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the current scene
        SceneManager.LoadScene(currentScene.name);
    }
}
