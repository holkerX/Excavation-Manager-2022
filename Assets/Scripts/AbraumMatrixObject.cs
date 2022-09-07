using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbraumMatrixObject
{
    public int x;
    public int y;
    public int k;
    public int tileIsShown;

    public AbraumMatrixObject(int x, int y, int k, int tileIsShown)
    {
        this.x = x;
        this.y = y;
        this.k = k;
        this.tileIsShown = tileIsShown;
    }
}