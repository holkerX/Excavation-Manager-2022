using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBehavior {
public class Artifact : MonoBehaviour
{
    public bool[][][] filledTiles;
    public void OnMouseEnter()
    {
        GameObject.Find("Tools").GetComponent<Tools>().setCursorArtifact();
    }

    public void OnMouseExit()
    {
        GameObject.Find("Tools").GetComponent<Tools>().setCursorDefault();
    }
}
}
