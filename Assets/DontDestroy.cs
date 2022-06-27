using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public int manpower = 1000; // helpers = manpower   
    public int expMultiplikator = 1; // Students und Professoren
    public int exp = 0; //Endergebniss nach Ausgrabung
    void Awake()
    {
        Debug.Log("hey");
        DontDestroyOnLoad(this.gameObject);
    }
}
