using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RecordAction : MonoBehaviour
{
    public string target;
    public int number;
    public string element;
    public int type;  // 0是无，1是减少, 2是增加，3是有增有减，4两者同减（献祭卡）

    public GameObject enemy;

    void Start()
    {
        Initial();
    }

    public void Action(string targetA, int numberA, string elementA, int typeA)
    {
        target = targetA; 
        number = numberA; 
        element = elementA;
        type = typeA;
    }

    public void Initial()
    {
        target = null;
        number = 0;
        element = null;
        type = 0;
        enemy = null;
    }
}
