using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{    
    public string message;

    private void OnMouseEnter()
    {
        TooltipManager._instance.ShowTooltip(message);
        Debug.Log("ist passiert");
    }

    private void OnMouseExit()
    {
        TooltipManager._instance.HideTooltip();
    }
}
