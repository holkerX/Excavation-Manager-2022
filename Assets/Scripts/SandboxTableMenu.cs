using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SandboxTableMenu : MonoBehaviour
{
    public void startDigging()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Digging 0");
    }

    // TODO DO NOT USE!!! first see checkPlayerAtDesk()
    public void exitMenu()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
