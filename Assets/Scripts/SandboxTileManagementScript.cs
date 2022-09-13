using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DataStorage;

namespace SandboxTileManagement
{
    public class SandboxTileManagementScript : MonoBehaviour
    {
        // Field Values for Number of found Artifacts
        public int[][] sandboxTileValues;
        private DataStorageClass dataStorage;
        private GameObject[] layers;

        void Awake()
        {
            GameObject dataStorageObject = GameObject.Find("DataStorageObject");
            dataStorage = dataStorageObject.GetComponent<DataStorageClass>();

            layers = GameObject.FindGameObjectsWithTag("GroundLayer");

            sandboxTileValues = new int[100][];
            for (int i = 0; i < 100; i++)
            {
                sandboxTileValues[i] = new int[50];
            }
            initSandboxTileValues();
        }

        private void initSandboxTileValues()
        {
            System.Random rnd = new System.Random();
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    sandboxTileValues[i][j] = rnd.Next(1, 6);   // creates a number between 1 and 5
                    //sandboxTileValues[i][j] = 10;   // For testing
                }
            }
        }

        public void initSandboxTileIsShown()
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        //Debug.Log(dataStorage.abraumMatrix[i][j][k] == 0);
                        // 1 means there is a Tile, 0 means there is no Tile
                        if (dataStorage.abraumMatrix[i][j][k] == 0)
                        {
                            Vector3Int gridPosition = new Vector3Int(i, j, 0);

                            Tilemap groundLayer = GameObject.Find("Ground (" + k + ")").GetComponent<Tilemap>();

                            if (groundLayer.HasTile(gridPosition))
                            {
                                groundLayer.SetTile(gridPosition, null);
                            }
                        }
                    }
                }
            }
        }

        public void setSandboxTileIsShown(int x, int y, int k, bool b)
        {
            if (b)
            {
                dataStorage.abraumMatrix[x][y][k] = 1;
            }
            else
            {
                dataStorage.abraumMatrix[x][y][k] = 0;
            }
        }
    }
}