using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tool : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = new Vector2(2, 2);
    public GameObject[] layers;
    public Vector4 pattern;

/*
    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
*/
    public void activeToolShovel() {
        //cursorTexture = ;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        pattern = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
    }

    public void activeToolBrush() {
        pattern = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
    }

    void Start() {
        layers = GameObject.FindGameObjectsWithTag("GroundLayer");
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
            // map.SetTile(gridPosition + pattern, null);                            
        } 
    }
}
