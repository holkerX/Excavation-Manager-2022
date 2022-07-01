using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CursorBehavior;

public class DiggingGroundLayers : MonoBehaviour
{
    //Collider und Positon des Objekts relativ zur Kamera ist für OnMouse...() wichtig
    public void OnMouseEnter()
    {
        GameObject.Find("CursorBehaviourSkript").GetComponent<DiggingCursorBehaviour>().setCursor();
    }

    public void OnMouseExit()
    {
        GameObject.Find("CursorBehaviourSkript").GetComponent<DiggingCursorBehaviour>().setCursorDefault();
    }
}