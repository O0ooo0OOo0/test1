using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossActionValueManager : MonoBehaviour
{
    public int attackV;
    public int defenseV;
    public int dodgeV;
    public int othersV;
    public bool isUnknow;

    void Start()
    {
        ResetBossActionValue();
    }

    // 重置boss行为数值
    public void ResetBossActionValue()
    {
        attackV = 0;
        defenseV = 0;
        dodgeV = 0;
        othersV = 0;
        isUnknow = false;
    }

    // 获取boss行为数值
    public void GetBossActionValue(int attack, int defense, int dodge, int others, bool isuk)
    {
        attackV = attack; 
        defenseV = defense; 
        dodgeV = dodge; 
        othersV = others;
        isUnknow = isuk;
    }
}
