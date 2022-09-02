using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int exp;
    public int manpower;
    public int expMultiplikator;
    public GameData()
    {
        this.exp = 0;
        this.manpower = 0;
        this.expMultiplikator = 1;
    }
}