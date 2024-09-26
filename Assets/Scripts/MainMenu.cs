using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void NewGame ()
    {
        SceneManager.LoadScene(2);
    }

        public void Credits ()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame ()
    {
        Debug.Log("This Button Quits the Game");
        Application.Quit();
    }
}
