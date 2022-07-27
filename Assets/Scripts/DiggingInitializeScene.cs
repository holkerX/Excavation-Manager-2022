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

    void Awake()
    {
        GameObject dataStorageObject = GameObject.Find("DataStorageObject");
        dataStorage = dataStorageObject.GetComponent<DataStorageClass>();

        Debug.Log(dataStorage.startingPoint);
        // 1 zu 20 Tile Verhältnis zwischen den Szenen (2m zu 10cm pro Tile)        
        Vector2 startingPoint = dataStorage.startingPoint * 20;
        startingPoint.x = startingPoint.x + 10;
        startingPoint.y = startingPoint.y + 10;

        Vector2 scaledGridSize = dataStorage.size * 20;

        GameObject mainCamera = GameObject.Find("Main Camera");

        // Position setzten
        Vector3 cameraPosition = mainCamera.transform.position;
        cameraPosition.x = startingPoint.x + scaledGridSize.x / 2;
        cameraPosition.y = startingPoint.y + scaledGridSize.y / 2;

        mainCamera.transform.position = cameraPosition;

        // Min-Max Positionen setzen
        cameraPosXMin = cameraPosition.x - scaledGridSize.x / 2;
        cameraPosXMax = cameraPosition.x + scaledGridSize.x / 2;
        cameraPosYMin = cameraPosition.y - scaledGridSize.y / 2;
        cameraPosYMax = cameraPosition.y + scaledGridSize.y / 2;

        /*
        // Kamera Größe Skalieren
        float x = dataStorage.size.x;
        float y = dataStorage.size.y;
        // Werte können jeh nach Konstellation der Flagge negativ sein
        if (x < 0)
        {
            x = x * -1;
        }
        if (y < 0)
        {
            y = y * -1;
        }

        float factor = 1; //Kameragröße 0 gibts nicht also Vector(0,0) ist Kameragröße 1...
        if (x < y)
        {
            factor = factor + y;
        }
        else
        {
            factor = factor + x;
        }

        mainCamera.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * factor;
        Camera camera = mainCamera.GetComponent<Camera>();
        camera.orthographicSize = factor * 10;
        */
    }

    void Start()
    {
        int artEn = dataStorage.artifactsEnabled;
        GameObject[] artifacts = GameObject.FindGameObjectsWithTag("Artifact");
        
        for (int i = 0; i < artifacts.Length; i++)
        {
            if (artEn >= 0)
            {
                artifacts[i].SetActive(true);
                artEn--;
            }
            artifacts[i].SetActive(false);
        }
    }
}
