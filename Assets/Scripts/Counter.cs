using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter: MonoBehaviour {

    public int moneyAmount;
    public string studentNumber;
    public string doctorNumber;
    public string helperNumber;
    public Text money;

    private void Start()
    {
        //get var money from applicaction phase
        moneyAmount = 10000;
    }

    private void Update()
    {
        money.text = moneyAmount.ToString();
    }

    public void StoreStudentNumber(string sn = "0")
    {
        studentNumber = sn;
        Debug.Log("sn: " + studentNumber);
    }

    public void StoreDoctorNumber(string dn = "0")
    {
        doctorNumber = dn;
        Debug.Log("dn: " + doctorNumber);
    }

    public void StoreHelperNumber(string hn = "0")
    {
        helperNumber = hn;
        Debug.Log("hn: " + helperNumber);
    }

    public void MinusMoney()
    {
        moneyAmount -= (int.Parse(studentNumber) * 200) + (int.Parse(doctorNumber) * 2000) + (int.Parse(helperNumber) * 300);
        Debug.Log("Amount " + moneyAmount);
    }

}
