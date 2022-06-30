using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingInitializeScene : MonoBehaviour
{
    void Awake(){
        GameObject DoNotDestroyObject = GameObject.Find("DoNotDestroyObject");
            DontDestroy DontDestroyScript =
                DoNotDestroyObject.GetComponent<DontDestroy>();

        // 1 zu 20 Tile Verhältnis zwischen den Szenen (2m zu 10cm pro Tile)        
        Vector2 startingPoint = DontDestroyScript.startingPoint * 20;
        startingPoint.x = startingPoint.x + 10;
        startingPoint.y = startingPoint.y + 10;

        Vector2 scaledGridSize = DontDestroyScript.size * 20;     

        Debug.Log(startingPoint);
        Debug.Log(DontDestroyScript.size);
        Debug.Log(scaledGridSize);
        Debug.Log("__________________");

        GameObject mainCamera = GameObject.Find("Main Camera");

        // Position setzten
        Vector3 cameraPosition = mainCamera.transform.position;
        cameraPosition.x = startingPoint.x + scaledGridSize.x / 2;
        cameraPosition.y = startingPoint.y + scaledGridSize.y / 2;

        mainCamera.transform.position = cameraPosition;

        Debug.Log(cameraPosition);

        // Größe Skalieren
        mainCamera.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * (DontDestroyScript.size.x + 1);
        Camera camera = mainCamera.GetComponent<Camera>();
        camera.orthographicSize = (DontDestroyScript.size.x + 1) * 10; 
    }
}
