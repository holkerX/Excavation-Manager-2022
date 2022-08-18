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
    GameObject dataStorageObject;

    private int[][] pattern = new int[5][];

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
        dataStorageObject = GameObject.Find("DataStorageObject");
        dataStorage = dataStorageObject.GetComponent<DataStorageClass>();
        setManpowerCounter(dataStorage.manpower);
        setExpCounter(dataStorage.exp);
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

        setExpCounter(dataStorage.exp);
    }

    public void activeToolShovel()
    {
        zoomArtifactActive = false;
        cursorBehaviour.setCursorShovel();
        pattern[0] = new int[] { 0, 0, 0, 0, 0 };
        pattern[1] = new int[] { 0, 0, 1, 0, 0 };
        pattern[2] = new int[] { 0, 1, 1, 1, 0 };
        pattern[3] = new int[] { 0, 1, 1, 1, 0 };
        pattern[4] = new int[] { 0, 0, 0, 0, 0 };
    }

    public void activeToolPickaxe()
    {
        zoomArtifactActive = false;
        cursorBehaviour.setCursorPickaxe();
        pattern[0] = new int[] { 0, 0, 1, 0, 0 };
        pattern[1] = new int[] { 0, 0, 1, 1, 0 };
        pattern[2] = new int[] { 0, 1, 1, 1, 0 };
        pattern[3] = new int[] { 0, 1, 1, 1, 0 };
        pattern[4] = new int[] { 0, 1, 1, 0, 0 };
    }

    public void activeToolTrowel()
    {
        zoomArtifactActive = false;
        cursorBehaviour.setCursorTrowel();
        pattern[0] = new int[] { 0, 0, 0, 0, 0 };
        pattern[1] = new int[] { 0, 0, 0, 0, 0 };
        pattern[2] = new int[] { 0, 0, 1, 0, 0 };
        pattern[3] = new int[] { 0, 0, 0, 0, 0 };
        pattern[4] = new int[] { 0, 0, 0, 0, 0 };
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
                //Artefakt bei Seite legen
                artifact.transform.position = new Vector3(-10, -10, 0);
                //Alle GameObjecte deaktivieren
                deactivateAllObjectsInScene();
                dataStorageObject.SetActive(true);

                //i ist die zuweisung des jeweiligen Artefakts
                dataStorage.ArtifactSceneNumber = i;
                SceneManager.LoadScene("Artifact " + i, LoadSceneMode.Additive);
            }
        }
    }

    private void deactivateAllObjectsInScene()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        for (int i = 0; i < allObjects.Length; i++)
        {
            allObjects[i].SetActive(false);
        }
        dataStorage.allSceneObjects = allObjects;
    }

    void digGround(Vector3 mousePosition)
    {
        //Nur die Anzahl der Layer -1, weil richtige Sortierung wichtig und FindGameObjectsWithTag diese nicht garantiert
        //-1 weil der letzte Layer nicht abgetragen werden kann
        for (int i = 0; i < layers.Length - 1; i++)
        {
            // Sortiert nach Name, größere Zahl = tiefer
            GameObject groundLayer = GameObject.Find("Ground (" + i + ")");
            Tilemap groundLayerTilemap = groundLayer.GetComponent<Tilemap>();
            TilemapRenderer groundLayerRenderer = groundLayer.GetComponent<TilemapRenderer>();

            Vector3Int gridPosition =
                groundLayerTilemap.WorldToCell(mousePosition);
            if (groundLayerTilemap.HasTile(gridPosition) && !cursorBehaviour.MouseHoversToolbox)
            {
                deleteTilesAtPosition(gridPosition, groundLayerTilemap, groundLayerRenderer.sortingOrder);
                depleteManpower();
                i = layers.Length + 1;
            }
        }
    }

    void deleteTilesAtPosition(Vector3Int gridPosition, Tilemap groundLayerTilemap, int sortingOrder)
    {
        Vector3Int gridPosTmp = gridPosition;

        //pattern ios set by the tool -> activeToolShovel() 
        //note that they are visualy 180 degree reversed in code
        for (int i = 0; i < pattern.Length; i++)
        {
            for (int k = 0; k < pattern[i].Length; k++)
            {
                int ModifiedCounterI = i + 1;
                int ModifiedCounterK = k + 1;

                if (pattern[i][k] == 1)
                {
                    if (ModifiedCounterI < 3)
                    {
                        gridPosTmp.y = gridPosTmp.y + (ModifiedCounterI - 3);
                    }
                    if (ModifiedCounterK < 3)
                    {
                        gridPosTmp.x = gridPosTmp.x + (ModifiedCounterK - 3);
                    }
                    if (ModifiedCounterI > 3)
                    {
                        gridPosTmp.y = gridPosTmp.y + (ModifiedCounterI - 3);
                    }
                    if (ModifiedCounterK > 3)
                    {
                        gridPosTmp.x = gridPosTmp.x + (ModifiedCounterK - 3);
                    }

                    if (groundLayerTilemap.HasTile(gridPosTmp))
                    {
                        groundLayerTilemap.SetTile(gridPosTmp, null);
                    }
                    checkArtifactsGotHit(sortingOrder, gridPosTmp);
                }
                gridPosTmp = gridPosition;
            }
        }
    }

    void checkArtifactsGotHit(int sortingOrderGround, Vector3Int gridPosTmp)
    {
        for (int i = 0; i < dataStorage.artifactsEnabled; i++)
        {
            // Sortiert nach Name, größere Zahl = tiefer
            GameObject artifact = GameObject
                    .Find("Artifact (" + i + ")");
            Tilemap artifactTilemap = artifact.GetComponent<Tilemap>();
            TilemapRenderer artifactTilemapRenderer = artifact.GetComponent<TilemapRenderer>();

            Vector3Int gridPosition =
                artifactTilemap.WorldToCell(gridPosTmp);

            //Check if Artifact was Hit
            if (artifactTilemapRenderer.sortingOrder == (sortingOrderGround + 1)
                && artifactTilemap.HasTile(gridPosition))
            {
                artifact.transform.position = new Vector3(-10, -10, 0);
            }
        }
    }

    void artefactDamaged(GameObject artifact)
    {
        //Get the Exp from the Artifact
        ArtifactArtifact artifactScript =
            artifact.GetComponent<ArtifactArtifact>();

        //Substract Damaged Penalty
        if (artifactScript.experiencePoints > 0)
        {
            artifactScript.experiencePoints =
                artifactScript.experiencePoints - 20;
        }

        setExpCounter(artifactScript.experiencePoints);
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

    public void setExpCounter(float exp)
    {
        //Set the Textfield in the UI
        TMPro.TextMeshProUGUI expCounter =
            GameObject.Find("ExpCounter").GetComponent<TMPro.TextMeshProUGUI>();
        expCounter.text = "EXP: " + exp;
    }

    public void quitScene()
    {
        SceneManager.LoadScene("Sandbox");
    }
}