using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour , IDataPersistence
{

    private GameObject ButtonNext;
    private bool levelExists;
    
    public void Start(){
        if(!levelExists)
        {
            ButtonNext = GameObject.Find("Continue");
            ButtonNext.SetActive(false);
        }
    }

    public void Continue ()
    {
        SceneManager.LoadScene("LevelMenu");
    }

    public void NewGame ()
    {
        SceneManager.LoadScene("Application");
    }

        public void Credits ()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame ()
    {
        Debug.Log("This Button Quits the Game");
        Application.Quit();
    }

    public void LoadData(GameData data){
        this.levelExists = data.abraumMatrixObjectsInitialized;
    }

    public void SaveData(ref GameData data){
    }
}
