using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DataStorage;

public class SandboxPauseMenu : MonoBehaviour
{
    public void exitGame()
    {
        Application.Quit();
    }
    public void mainMenu()
    {
        DataPersistenceManager.instance.SaveGame();
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelMenu");
    }
    public void saveGame()
    {
        DataPersistenceManager.instance.SaveGame();
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void exitMenu()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
