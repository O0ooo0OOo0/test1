using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APManager : MonoBehaviour
{
    public GameObject[] AP;
    public int maxValue;
    public int currentValue;

    void Start()
    {
        ResetAP();
    }

    //消耗AP
    public void TakeAP(int count)
    {
        currentValue = currentValue - count;

        foreach (GameObject ap in AP)
        {
            if (ap != null)
            {
                ap.SetActive(false);
            }
        }
        AP[currentValue].SetActive(true);
    }

    //增加AP
    public void AddAP(int count)
    {
        currentValue = currentValue + count;

        foreach (GameObject ap in AP)
        {
            if (ap != null)
            {
                ap.SetActive(false);
            }
        }
        AP[currentValue].SetActive(true);
    }

    //定义AP
    public void ResetAP()
    {
        currentValue = maxValue;

        foreach (GameObject ap in AP)
        {
            if (ap != null)
            {
                ap.SetActive(false);
            }
        }
        AP[currentValue].SetActive(true);
    }
}
