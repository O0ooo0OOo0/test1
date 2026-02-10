using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefenseNumber : MonoBehaviour
{
    public GameObject defense;
    public TMP_Text defenseValue;
    public int defensenumber;
    public int redundant;

    void Start()
    {
        defense.SetActive(false);
        defensenumber = 0;
        defenseValue.text = defensenumber.ToString();
        redundant = 0;
    }

    //加防
    public void AddDefense(int count)
    {
        defensenumber = defensenumber + count;
        if (defensenumber != 0)
        {
            defense.SetActive(true);
            defenseValue.text = defensenumber.ToString();
        }
    }

    //减防
    public void RemoveDefense(int count)
    {
        if (defensenumber > count)
        {
            defensenumber = defensenumber - count;
            defenseValue.text = defensenumber.ToString();
        }
        else if (defensenumber <= count)
        {
            redundant = count - defensenumber;
            defensenumber = 0;
            defenseValue.text = defensenumber.ToString();
            defense.SetActive(false);
        }
    }

    //定义防御值
    public void DefineDefense(int count)
    {
        if (count == 0)
        {
            defensenumber = 0;
            defenseValue.text = defensenumber.ToString();
            defense.SetActive(false);
        }
        else if (count != 0)
        {
            defense.SetActive(true);
            defensenumber = count;
            defenseValue.text = defensenumber.ToString();
        }
    }
}
