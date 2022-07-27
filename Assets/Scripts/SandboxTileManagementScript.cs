using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            initSanboxTileValues();
        }

        private void initSanboxTileValues()
        {
            //50 x 100
            sandboxTileValues[0][0] = 1;
            sandboxTileValues[1][0] = 1;
            sandboxTileValues[2][0] = 1;
            sandboxTileValues[3][0] = 3;
            sandboxTileValues[4][0] = 1;
            sandboxTileValues[5][0] = 7;
            sandboxTileValues[6][0] = 1;
            sandboxTileValues[7][0] = 1;
            sandboxTileValues[8][0] = 1;
            sandboxTileValues[9][0] = 1;
            sandboxTileValues[10][0] = 1;
        }
    }
}