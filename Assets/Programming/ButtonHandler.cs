using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    string currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {}

    public void restartLevel()
    {
        var currentScene = SceneManager.GetActiveScene().name;
        if (currentScene.Contains("Flipped"))
        {
            SceneManager.LoadScene(currentScene.Substring(0, 6));
        }
        GameObject.Find("Player").transform.position = GameObject.Find("RespawnPoint").transform.position;
    }

    public void ExitGameRunner()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("LevelSelectMenu");
    }
}
