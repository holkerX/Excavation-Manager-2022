using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStorage;

public class DiggingInitializeScene : MonoBehaviour
{
    public float cameraPosXMin;
    public float cameraPosXMax;
    public float cameraPosYMin;
    public float cameraPosYMax;
    DataStorageClass dataStorage;
    Vector2 scaledGridSize;

    void Awake()
    {
        GameObject dataStorageObject = GameObject.Find("DataStorageObject");
        dataStorage = dataStorageObject.GetComponent<DataStorageClass>();

        // 1 zu 20 Tile Verhältnis zwischen den Szenen (2m zu 10cm pro Tile)     
        scaledGridSize = dataStorage.size * 20;
    }

    void Start()
    {
        initializeArtifacts();
        setCameraPosition();
    }

    private void setCameraPosition()
    {
        // Camera Position setzten
        GameObject mainCamera = GameObject.Find("Main Camera");
        Vector3 cameraPosition = mainCamera.transform.position; // keep z position
        cameraPosition.x = (scaledGridSize.x / 2);
        cameraPosition.y = (scaledGridSize.y / 2);

        mainCamera.transform.position = cameraPosition;

        // Min-Max Positionen setzen
        cameraPosXMin = (cameraPosition.x - (scaledGridSize.x / 2)) + 10;
        cameraPosXMax = (cameraPosition.x + (scaledGridSize.x / 2)) - 10;
        cameraPosYMin = (cameraPosition.y - (scaledGridSize.y / 2)) + 10;
        cameraPosYMax = (cameraPosition.y + (scaledGridSize.y / 2)) - 10;
    }

    private void initializeArtifacts()
    {
        GameObject[] artifacts = GameObject.FindGameObjectsWithTag("Artifact");
        for (int i = 0; i < artifacts.Length; i++)
        {
            artifacts[i].SetActive(false);
        }

        int artifactsEnabled = dataStorage.artifactsEnabled;
        System.Random rnd = new System.Random();
        for (int j = artifactsEnabled; j >= 0; j--)
        {
            int tmp = rnd.Next(0, artifacts.Length);
            artifacts[tmp].SetActive(true);
            setArtifactPosition(artifacts[tmp]);
        }
        Debug.Log("Artifacts Enabled: " + dataStorage.artifactsEnabled);
    }

    private void setArtifactPosition(GameObject artifact)
    {
        System.Random rnd = new System.Random();
        Vector3 newPosition;
        newPosition.x = rnd.Next(0, (int)scaledGridSize.x);
        newPosition.y = rnd.Next(0, (int)scaledGridSize.y);
        newPosition.z = 0; // Layer is controlled by Tilemap Renderer Component and is right now "hardcoded"
        artifact.transform.position = newPosition;
        Debug.Log("Artefact: " + artifact.name + " Is at Position: " + newPosition);
    }
}
