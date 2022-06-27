using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{

    public int[,] shopItems = new int[5,5];
    public float money;
    public Text MoneyTXT;
    public Button tools;
    public int toolPrice;

    void Start()
    {
        MoneyTXT.text = "Money:" + money.ToString();

        //ID's
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;

        //Price
        shopItems[2, 1] = 200;
        shopItems[2, 2] = 2000;
        shopItems[2, 3] = 300;
        

        //Quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;

    }

     void Update()
    {
        //Price for tools
        shopItems[2, 4] = toolPrice;
        toolPrice = 100 * shopItems[3, 3];

        //Quantity for tools equals number of helpers
        shopItems[3, 4] = shopItems[3, 3];

        if (toolPrice <= 0)
        {
            tools.GetComponent<Button>().interactable = false;
        }
        else
        {
            tools.GetComponent<Button>().interactable = true;
        }
    }
   
    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (money >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            money -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++;
            MoneyTXT.text = "Money:" + money.ToString();
            ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();

        }


    }
}
