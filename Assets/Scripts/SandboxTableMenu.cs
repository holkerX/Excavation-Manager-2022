using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DataStorage;

public class SandboxTableMenu : MonoBehaviour
{
    public void startDigging()
    {
        GameObject dataStorageObject = GameObject.Find("DataStorageObject");
        DataStorageClass dataStorage = dataStorageObject.GetComponent<DataStorageClass>();
        Time.timeScale = 1;
        SceneManager.LoadScene("Digging " + dataStorage.LevelNumber);
    }

    public void mainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    // TODO DO NOT USE!!! first see checkPlayerAtDesk()
    public void exitMenu()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
