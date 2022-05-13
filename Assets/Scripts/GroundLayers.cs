using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBehavior {
public class GroundLayers : MonoBehaviour
{
    public void OnMouseEnter()
    {
        GameObject.Find("Tools").GetComponent<Tools>().setCursor();
    }

    public void OnMouseExit()
    {
        GameObject.Find("Tools").GetComponent<Tools>().setCursorDefault();
    }
}
}