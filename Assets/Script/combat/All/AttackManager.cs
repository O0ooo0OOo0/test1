using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    //Player
    public int fangyuP;
    public int gongjiP;

    //Enemy
    public int fangyuE;
    public int gongjiE;


    void Start()
    {
        fangyuP = 0;
        gongjiP = 0;
        fangyuE = 0;
        gongjiE = 0;
    }
}
