using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int exp;
    public int manpower;
    public int expMultiplikator;
    public bool abraumMatrixObjectsInitialized;
    public double money;
    public int LevelNumber;
    public AbraumMatrixObject[] abraumMatrixObjects;

    public GameData()
    {
        this.exp = 0;
        this.manpower = 0;
        this.expMultiplikator = 1;
        this.abraumMatrixObjectsInitialized = false;
        this.abraumMatrixObjects = new AbraumMatrixObject[15000];
        this.money = 0;
        this.LevelNumber = 0;
    }
}