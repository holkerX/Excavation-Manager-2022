using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tool : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public GameObject[] layers;
    System.Random rnd = new System.Random(); 
    public bool isAccurate = true;

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
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
        if(isAccurate) {
            map.SetTile(gridPosition, null);
        } else {
            map.SetTile(gridPosition, null);

            Vector3Int inaccuracyX = gridPosition;
            inaccuracyX.x = inaccuracyX.x + rnd.Next(-1, 1);    
            map.SetTile(inaccuracyX, null);

            Vector3Int inaccuracyY = gridPosition;
            inaccuracyY.y = inaccuracyY.y + rnd.Next(-1, 1);
            map.SetTile(inaccuracyY, null);                                
        } 
    }
}
