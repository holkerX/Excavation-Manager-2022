using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour, IDataPersistence
{
    private int LevelNumber;

    public void LoadLevel(){
        SceneManager.LoadScene("Sandbox " + LevelNumber);
    }

    public void NextLevel(){
        if(LevelNumber == 0)
        {
            LevelNumber = 1;
        } 
        else 
        {
            LevelNumber = 0;
        }

        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("Application");
    }

    public void MainMenu(){
        SceneManager.LoadScene("Menu");
    }

    public void LoadData(GameData data){
        this.LevelNumber = data.LevelNumber;
    }

    public void SaveData(ref GameData data){
        data.LevelNumber = this.LevelNumber;
        Debug.Log("saved level number" + LevelNumber);
    }
}
