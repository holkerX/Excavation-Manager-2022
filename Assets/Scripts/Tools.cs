using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ToolBehavior {
public class Tools : MonoBehaviour
{
    private Texture2D cursorTexture;
    public Texture2D cursorTextureShovel;
    public Texture2D cursorTextureBrush;
    public Texture2D cursorTextureDefault;
    public Texture2D cursorTextureArtifact;
    private Vector2 cursorHotspot;
    public GameObject[] layers;
    public Vector4 pattern;

    public void activeToolShovel() {
        cursorTexture = cursorTextureShovel;
        pattern = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
    }

    public void activeToolBrush() {
        cursorTexture = cursorTextureBrush;
        pattern = new Vector4(1.0f, 0.0f, 1.0f, 0.0f);
    }

    void Start() {
        activeToolShovel(); 
        layers = GameObject.FindGameObjectsWithTag("GroundLayer");
    }

    public void setCursor() {
        cursorHotspot = new Vector2 (cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }

    public void setCursorDefault() {
        cursorHotspot = new Vector2 (cursorTextureDefault.width / 2, cursorTextureDefault.height / 2);
        Cursor.SetCursor(cursorTextureDefault, cursorHotspot, CursorMode.Auto);
    }

    public void setCursorArtifact() {
        cursorHotspot = new Vector2 (cursorTextureDefault.width / 2, cursorTextureDefault.height / 2);
        Cursor.SetCursor(cursorTextureArtifact, cursorHotspot, CursorMode.Auto);
    }

    void Update() {
        if(Input.GetMouseButtonUp(0)) {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            //Nur die Anzahl der Layer, weil richtige Sortierung wichtig und FindGameObjectsWithTag diese nicht garantiert
            for(int i = 0; i < layers.Length; i++) {
                // Sortiert nach Name, größere Zahl = tiefer
                Tilemap map = GameObject.Find("Ground (" + i + ")").GetComponent<Tilemap>();

                Vector3Int gridPosition = map.WorldToCell(mousePosition);

                // Wenn Artifact && ausgegraben -> Detail Ansicht 
                // Sonst tue nichts oder Artiakt beschädigt???

                if(map.HasTile(gridPosition)) {
                    deleteTilesAtPosition(gridPosition, map);
                    i = layers.Length + 1;
                }
            }     
        }   
    }

    void deleteTilesAtPosition(Vector3Int gridPosition, Tilemap map) {
        if(pattern == Vector4.zero) {
            map.SetTile(gridPosition, null);
        } else {
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
}
}