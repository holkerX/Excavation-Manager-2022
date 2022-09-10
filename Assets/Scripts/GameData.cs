using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int exp;
    public int manpower;
    public int expMultiplikator;
    public bool abraumMatrixInitialized;
    public int[][][] abraumMatrix;
    public double money;

    public GameData()
    {
        this.exp = 0;
        this.manpower = 0;
        this.expMultiplikator = 1;
        abraumMatrixInitialized = false;
        this.abraumMatrix = null;
        this.money = 0;
    }
}