using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APnumber : MonoBehaviour
{
    public GameObject[] AP;
    public int initialValue = 3;
    public int maxValue;
    public int value;

    void Start()
    {
        foreach (GameObject ap in AP)
        {
            if (ap != null)
            {
                ap.SetActive(false);
            }
        }
        AP[initialValue].SetActive(true);
        value = initialValue;
    }

    //消耗AP
    public void TakeAP(int count)
    {
        value = value - count;

        foreach (GameObject ap in AP)
        {
            if (ap != null)
            {
                ap.SetActive(false);
            }
        }
        AP[value].SetActive(true);
    }

    //增加AP
    public void AddAP(int count)
    {
        value = value + count;

        foreach (GameObject ap in AP)
        {
            if (ap != null)
            {
                ap.SetActive(false);
            }
        }
        AP[value].SetActive(true);
    }

    //定义AP
    public void DefineAP()
    {
        value = maxValue;

        foreach (GameObject ap in AP)
        {
            if (ap != null)
            {
                ap.SetActive(false);
            }
        }
        AP[value].SetActive(true);
    }
}
