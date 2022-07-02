using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ApplicationMenu : MonoBehaviour
{
 public void Apply ()
    {
        SceneManager.LoadScene(3);
    }
}
