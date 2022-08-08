using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using CursorBehavior;
using DataStorage;

public class DiggingToolBehaviour : MonoBehaviour
{
    private GameObject[] layers;

    private GameObject[] artifacts;

    DataStorageClass dataStorage;

    private Vector4 pattern;

    private bool zoomArtifactActive = false;

    DiggingCursorBehaviour cursorBehaviour;

    void Awake()
    {
        cursorBehaviour = GameObject.Find("CursorBehaviourSkript").GetComponent<DiggingCursorBehaviour>();
        layers = GameObject.FindGameObjectsWithTag("GroundLayer");
        artifacts = GameObject.FindGameObjectsWithTag("Artifact");
    }

    void Start()
    {
        activeToolZoomArtifact();
        cursorBehaviour = GameObject.Find("CursorBehaviourSkript").GetComponent<DiggingCursorBehaviour>();
        dataStorage = GameObject.Find("DataStorageObject").GetComponent<DataStorageClass>();
        setManpowerCounter(dataStorage.manpower);
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0; //Die Tilemaps sind auf z = 0 und die Kamera bei z = -10 --> Tilemap.HasTile(mousePosition) muss bei z = 0 haben um true zu sein
            if (zoomArtifactActive)
            {
                zoomArtifact(mousePosition);
            }
            else
            {
                digGround(mousePosition);
            }
        }
    }

    public void activeToolShovel()
    {
        zoomArtifactActive = false;
        cursorBehaviour.setCursorShovel();
        pattern = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
    }

    public void activeToolPickaxe()
    {
        zoomArtifactActive = false;
        cursorBehaviour.setCursorPickaxe();
        pattern = new Vector4(1.0f, 0.0f, 1.0f, 0.0f);
    }

    public void activeToolZoomArtifact()
    {
        zoomArtifactActive = true;
        cursorBehaviour.setCursorZoomArtifact();
        //pattern = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
    }

    void zoomArtifact(Vector3 mousePosition)
    {
        for (int i = 0; i < dataStorage.artifactsEnabled; i++)
        {
            // Sortiert nach Name, größere Zahl = tiefer
            Tilemap artifact =
                GameObject
                    .Find("Artifact (" + i + ")")
                    .GetComponent<Tilemap>();

            Vector3Int gridPosition = artifact.WorldToCell(mousePosition);

            //Check if Artifact Tile was klicked
            if (artifact.HasTile(gridPosition))
            {
                //i ist die zuweisung des jeweiligen Artefakts
                SceneManager.LoadScene("Artifact (" + i + ")");
            }
        }
    }

    void digGround(Vector3 mousePosition)
    {
        //Nur die Anzahl der Layer -1, weil richtige Sortierung wichtig und FindGameObjectsWithTag diese nicht garantiert
        //-1 weil der letzte Layer nicht abgetragen werden kann
        for (int i = 0; i < layers.Length - 1; i++)
        {
            // Sortiert nach Name, größere Zahl = tiefer
            Tilemap groundLayer =
                GameObject
                    .Find("Ground (" + i + ")")
                    .GetComponent<Tilemap>();

            Vector3Int gridPosition =
                groundLayer.WorldToCell(mousePosition);

            if (groundLayer.HasTile(gridPosition))
            {
                deleteTilesAtPosition(gridPosition, groundLayer);
                depleteManpower();
                i = layers.Length + 1;
            }
        }
    }

    void deleteTilesAtPosition(Vector3Int gridPosition, Tilemap map)
    {
        if (pattern == Vector4.zero)
        {
            map.SetTile(gridPosition, null);
        }
        else
        {
            map.SetTile(gridPosition, null);
            Vector3Int gridPosTmp;

            gridPosTmp = gridPosition;
            gridPosTmp.x = gridPosTmp.x + (int)Math.Floor(pattern.x);
            map.SetTile(gridPosTmp, null);

            gridPosTmp = gridPosition;
            gridPosTmp.y = gridPosTmp.y + (int)Math.Floor(pattern.y);
            map.SetTile(gridPosTmp, null);

            gridPosTmp = gridPosition;
            gridPosTmp.x = gridPosTmp.x - (int)Math.Floor(pattern.z);
            map.SetTile(gridPosTmp, null);

            gridPosTmp = gridPosition;
            gridPosTmp.y = gridPosTmp.y - (int)Math.Floor(pattern.w);
            map.SetTile(gridPosTmp, null);
        }
    }

    void depleteManpower()
    {
        if (dataStorage.manpower > 0)
        {
            dataStorage.manpower =
                dataStorage.manpower - 1;
        }

        setManpowerCounter(dataStorage.manpower);
    }

    public void setManpowerCounter(float manpower)
    {
        //Set the Textfield in the UI
        TMPro.TextMeshProUGUI manpowerCounter =
            GameObject.Find("ManpowerCounter").GetComponent<TMPro.TextMeshProUGUI>();
        manpowerCounter.text = "Manpower: \n\r" + manpower;
    }

    public void quitScene()
    {
        SceneManager.LoadScene("Sandbox");
    }
}