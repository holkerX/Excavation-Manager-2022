using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataStorage
{
    public class DataStorageClass : MonoBehaviour, IDataPersistence
    {
        //Manager Data Input
        public int exp;
        //Manager Data Output
        public int manpower;
        public int expMultiplikator;

        //Digging Scene (Ausgrabungsschnitt)
        public Vector2 size;
        public int artifactsEnabled;
        bool abraumMatrixInitialized = false;
        public int[][][] abraumMatrix;

        public GameObject[] allSceneObjects;

        public int ArtifactSceneNumber;

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);

            if (!abraumMatrixInitialized)
            {
                initializeAbraumMatrix();
                abraumMatrixInitialized = true;
            }
        }

        private void initializeAbraumMatrix()
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

        public void LoadData(GameData data)
        {
            DataStorageClass dataStorage = GameObject.Find("DataStorageObject").GetComponent<DataStorageClass>();
            exp = data.exp;
            expMultiplikator = data.expMultiplikator;
            manpower = data.manpower;
        }

        public void SaveData(ref GameData data)
        {
            DataStorageClass dataStorage = GameObject.Find("DataStorageObject").GetComponent<DataStorageClass>();
            data.exp = exp;
            data.expMultiplikator = expMultiplikator;
            data.manpower = manpower;
            Debug.Log("Saved Data " + exp);
        }
    }
}
