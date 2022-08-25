using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ApplicationMenu : MonoBehaviour
{
    public float moneyMultiplikator;
    public TMP_Dropdown applicationOptions;

    void Start()
    {
        applicationOptions.onValueChanged.AddListener(delegate
        {
            valueChanged(applicationOptions);
        });
    }

     void valueChanged(TMP_Dropdown option)
    {
        Debug.Log("Selected value: " + option.value);
    }

    public void Apply ()
    {
        SceneManager.LoadScene(3);
    }
}
