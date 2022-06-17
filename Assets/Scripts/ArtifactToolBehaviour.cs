using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class ArtifactToolBehaviour : MonoBehaviour
{
    private GameObject[] groundLayers;

    private GameObject[] dustLayers;

    private Texture2D cursorTexture;

    private Vector2 cursorHotspot;

    private Vector4 pattern;

    private bool toolCanDamageArtifact;

    private bool toolCanDig;

    // Start is called before the first frame update
    void Start()
    {
        pattern = Vector4.zero;
        groundLayers = GameObject.FindGameObjectsWithTag("GroundLayer");
        dustLayers = GameObject.FindGameObjectsWithTag("DustLayer");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0; //Die Tilemaps sind auf z = 0 und die Kamera bei z = -10 --> Tilemap.HasTile(mousePosition) muss bei z = 0 haben um true zu sein

            useActiveTool (mousePosition);
        }
    }

    private void useActiveTool(Vector3 mousePosition)
    {
        if (!checkTilesDust(mousePosition))
        {
            if (!checkTilesArtifact(mousePosition))
            {
                if (!checkTilesGround(mousePosition))
                {
                    //Not a click on the Grid or a Bug
                }
            }
        }
    }

    bool checkTilesArtifact(Vector3 mousePosition)
    {
        GameObject artifact = GameObject.Find("Artifact");
        Tilemap artifactTilemap = artifact.GetComponent<Tilemap>();

        Vector3Int gridPosition = artifactTilemap.WorldToCell(mousePosition);

        if (artifactTilemap.HasTile(gridPosition))
        {
            if (toolCanDamageArtifact)
            {
                artefactDamaged(artifact);

                //Load Damaged Tile Graphics???
            }
            return true;
        }
        return false;
    }

    void artefactDamaged(GameObject artifact)
    {
        //Get the Exp from the Artifact
        ArtifactArtifact artifactScript = artifact.GetComponent<ArtifactArtifact>();

        //Substract Damaged Penalty
        if(artifactScript.experiencePoints > 0) {
            artifactScript.experiencePoints = artifactScript.experiencePoints  - 20;
        }

        //Set the Textfield in the UI
        TMPro.TextMeshProUGUI expCounter =
            GameObject.Find("ExpCounter").GetComponent<TMPro.TextMeshProUGUI>();
        expCounter.text = "EXP: " + artifactScript.experiencePoints;
    }

    bool checkTilesGround(Vector3 mousePosition)
    {
        // Differenzierung zwischen Staub und "Erde"
        // Staub von Pinsel abtragbar
        // Erde von ...
        if (toolCanDig)
        {
            for (int i = 0; i < groundLayers.Length - 1; i++)
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
                    deleteTilesAtPosition (gridPosition, groundLayer);
                    i = groundLayers.Length + 1;
                    return true;
                }
            }
        }
        return false;
    }

    bool checkTilesDust(Vector3 mousePosition)
    {
        for (int i = 0; i < dustLayers.Length; i++)
        {
            // Sortiert nach Name, größere Zahl = tiefer
            Tilemap dustLayer =
                GameObject.Find("Dust (" + i + ")").GetComponent<Tilemap>();

            Tilemap groundLayer =
                GameObject.Find("Ground (" + i + ")").GetComponent<Tilemap>();

            Vector3Int gridPosition = dustLayer.WorldToCell(mousePosition);

            if (
                dustLayer.HasTile(gridPosition) &&
                !groundLayer.HasTile(gridPosition)
            )
            {
                deleteTilesAtPosition (gridPosition, dustLayer);
                i = dustLayers.Length + 1;
                return true;
            }
        }
        return false;
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
            gridPosTmp.x = gridPosTmp.x + (int) pattern.x;
            map.SetTile(gridPosTmp, null);

            gridPosTmp = gridPosition;
            gridPosTmp.y = gridPosTmp.y + (int) pattern.y;
            map.SetTile(gridPosTmp, null);

            gridPosTmp = gridPosition;
            gridPosTmp.x = gridPosTmp.x - (int) pattern.z;
            map.SetTile(gridPosTmp, null);

            gridPosTmp = gridPosition;
            gridPosTmp.y = gridPosTmp.y - (int) pattern.w;
            map.SetTile(gridPosTmp, null);
        }
    }

    public void activeToolBrush()
    {
        toolCanDamageArtifact = false;
        toolCanDig = false;
    }

    public void activeToolDustpan()
    {
        toolCanDamageArtifact = false;
        toolCanDig = true;
    }

    public void activeToolTrowel()
    {
        toolCanDamageArtifact = true;
        toolCanDig = true;
    }
}
