using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStorage;

namespace SandboxTileManagement
{
    public class SandboxTileManagementScript : MonoBehaviour
    {
        // Field Values for Number of found Artifacts
        public int[][] sandboxTileValues;

        void Awake()
        {
            //Abraum und Löcher
            /* 3D Array
            * 1: Schicht
            * 2: X-Koordinate
            * 3: Y-Koordinate
            *
            * 1 = Tile noch da
            * 0 = weg
            */
            sandboxTileValues = new int[100][];
            for (int i = 0; i < 100; i++){
                sandboxTileValues[i] = new int[50];
            }
            initSanboxTileValues();
        }

        private void initSanboxTileValues()
        {
            System.Random rnd = new System.Random();
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    //sandboxTileValues[i][j] = rnd.Next(1, 6);   // creates a number between 1 and 5
                    sandboxTileValues[i][j] = 4;   // For testing
                }
            }
        }
    }
}