using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manger : MonoBehaviour {

 private int textNumber;
 public Text TextObject = null;
 public void addOne(){
     if (TextObject != null) {
             textNumber++;
             TextObject.text = textNumber.ToString ();
     }
 }
 public void minusOne(){
     if (TextObject != null) {
             --textNumber;
             TextObject.text = textNumber.ToString();
     }
 }
}
