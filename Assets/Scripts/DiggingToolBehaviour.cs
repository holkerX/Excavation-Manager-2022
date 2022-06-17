using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace CursorBehavior
{
    public class DiggingToolBehaviour : MonoBehaviour
    {
        //Cursor textures
        public Texture2D cursorTextureShovel;

        public Texture2D cursorTextureBrush;

        public Texture2D cursorTextureDefault;

        public Texture2D cursorTextureHoverArtifact;

        public Texture2D cursorTextureZoomArtifact;

        private Texture2D cursorTexture;

        private Vector2 cursorHotspot;

        private GameObject[] layers;

        private GameObject[] artifacts;

        private Vector4 pattern;

        private bool zoomArtifactActive = false;

        public void activeToolShovel()
        {
            zoomArtifactActive = false;
            cursorTexture = cursorTextureShovel;
            pattern = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        }

        public void activeToolPickaxe()
        {
            zoomArtifactActive = false;
            cursorTexture = cursorTextureBrush;
            pattern = new Vector4(1.0f, 0.0f, 1.0f, 0.0f);
        }

        public void activeToolZoomArtifact()
        {
            zoomArtifactActive = true;
            cursorTexture = cursorTextureZoomArtifact;
            //pattern = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
        }

        void Start()
        {
            activeToolZoomArtifact();
            layers = GameObject.FindGameObjectsWithTag("GroundLayer");
            artifacts = GameObject.FindGameObjectsWithTag("Artifact");
        }

        public void setCursor()
        {
            cursorHotspot =
                new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
            Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
        }

        public void setCursorDefault()
        {
            cursorHotspot =
                new Vector2(cursorTextureDefault.width / 2,
                    cursorTextureDefault.height / 2);
            Cursor
                .SetCursor(cursorTextureDefault,
                cursorHotspot,
                CursorMode.Auto);
        }

        public void setCursorArtifact()
        {
            if (!zoomArtifactActive)
            {
                cursorHotspot =
                    new Vector2(cursorTextureHoverArtifact.width / 2,
                        cursorTextureHoverArtifact.height / 2);
                Cursor
                    .SetCursor(cursorTextureHoverArtifact,
                    cursorHotspot,
                    CursorMode.Auto);
            }
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
                    zoomArtifact (mousePosition);
                }
                else
                {
                    digGround (mousePosition);
                }
            }
        }

        void zoomArtifact(Vector3 mousePosition)
        {
            for (int i = 0; i < artifacts.Length; i++)
            {
                // Sortiert nach Name, größere Zahl = tiefer
                Tilemap artifact =
                    GameObject
                        .Find("Artifact (" + i + ")")
                        .GetComponent<Tilemap>();

                Vector3Int gridPosition = artifact.WorldToCell(mousePosition);

                //Zoom artifact UI-Button was pressed
                if (artifact.HasTile(gridPosition))
                {
                    SceneManager.LoadScene("Artifact");
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

                //Zoom artifact UI-Button was pressed
                if (groundLayer.HasTile(gridPosition))
                {
                    deleteTilesAtPosition (gridPosition, groundLayer);
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
                gridPosTmp.x = gridPosTmp.x + (int) Math.Floor(pattern.x);
                map.SetTile(gridPosTmp, null);

                gridPosTmp = gridPosition;
                gridPosTmp.y = gridPosTmp.y + (int) Math.Floor(pattern.y);
                map.SetTile(gridPosTmp, null);

                gridPosTmp = gridPosition;
                gridPosTmp.x = gridPosTmp.x - (int) Math.Floor(pattern.z);
                map.SetTile(gridPosTmp, null);

                gridPosTmp = gridPosition;
                gridPosTmp.y = gridPosTmp.y - (int) Math.Floor(pattern.w);
                map.SetTile(gridPosTmp, null);
            }
        }
    }
}
