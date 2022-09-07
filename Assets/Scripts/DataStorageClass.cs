using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataStorage
{
    public class DataStorageClass : MonoBehaviour
    {
        //Manager Data Input
        public int exp;
        //Manager Data Output
        public int manpower;
        public int expMultiplikator;

        //Digging Scene (Ausgrabungsschnitt)
        public Vector2 size;
        public int artifactsEnabled;
        public bool abraumMatrixInitialized = false;
        public int[][][] abraumMatrix;

        public GameObject[] allSceneObjects;

        public int ArtifactNumber;

        public int LevelNumber;

        public static DataStorageClass instance { get; private set; }

        void Awake()
        {
            if (instance != null)
            {
                Debug.Log("There is already an DataStorageClass present in the scene.");
                GameObject.Destroy(this.gameObject);
            }
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            if (!abraumMatrixInitialized)
            {
                initializeAbraumMatrix();
                abraumMatrixInitialized = true;
            }
        }

        public void initializeAbraumMatrix()
        {
            abraumMatrix = new int[100][][];
            for (int i = 0; i < 100; i++)
            {
                abraumMatrix[i] = new int[50][];
                for (int j = 0; j < 50; j++)
                {
                    abraumMatrix[i][j] = new int[3];
                    for (int k = 0; k < 3; k++)
                    {
                        abraumMatrix[i][j][k] = 1; // 1 means there is a Tile, 0 means there is no Tile
                    }
                }
            }
        }

        public void activateAllObjectsInScene()
        {
            for (int i = 0; i < allSceneObjects.Length; i++)
            {
                allSceneObjects[i].SetActive(true);
            }
            allSceneObjects = null;
        }
    }
}