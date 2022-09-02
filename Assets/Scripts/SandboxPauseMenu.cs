using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SandboxPauseMenu : MonoBehaviour
{
    public void exitGame()
    {
        Application.Quit();
    }
    public void mainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
    public void exitMenu()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
