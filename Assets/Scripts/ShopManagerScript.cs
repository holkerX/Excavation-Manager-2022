using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopManagerScript : MonoBehaviour, IDataPersistence
{
    public int[,] shopItems = new int[5, 5];
    public double money;
    public int manpower;
    public double manpowercast;
    public int expMultiplikator;
    public Text MoneyTXT;
    public Button tools;
    public int toolPrice;
    public double noTools;

    public void LoadData(GameData data){
        this.money = data.money;
        this.manpower = data.manpower;
        this.expMultiplikator = data.expMultiplikator;
    }

    public void SaveData(ref GameData data){
        data.money = this.money;
        data.manpower = this.manpower;
        data.expMultiplikator = this.expMultiplikator;
    }

    void Start()
    {
        MoneyTXT.text = money.ToString();

        //ID's
        shopItems[1, 1] = 1; //Students
        shopItems[1, 2] = 2; //Doctors
        shopItems[1, 3] = 3; //Helpers
        shopItems[1, 4] = 4; //Tools

        //Price
        shopItems[2, 1] = 2000;
        shopItems[2, 2] = 20000;
        shopItems[2, 3] = 3000;
        shopItems[2, 4] = 1000;


        //Quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (money >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            money -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++;
            MoneyTXT.text = money.ToString();
            ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();

        }
    }

    public void startExcavation()
    {
        int i = 0; //Level nummer 0 oder 1
        SceneManager.LoadScene("Sandbox " + i);

        if (shopItems[3, 4] < shopItems[3, 3])
        {
            noTools = 0.7;
        }
         else
        {
            noTools = 1;
        }

        manpowercast = ((shopItems[3, 3] * 4) + shopItems[3, 1]) * noTools;
        manpower = (int)manpowercast;
        expMultiplikator = shopItems[3, 1] + (shopItems[3, 2] * 12);

        Debug.Log("tools : " + noTools);
        Debug.Log("manpower : " + manpower);
        Debug.Log("expMultiplikator : " + expMultiplikator);
        DataPersistenceManager.instance.SaveGame();
    
    }
}
