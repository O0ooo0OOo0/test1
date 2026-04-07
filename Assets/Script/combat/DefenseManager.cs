using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefenseManager : MonoBehaviour
{
    public int defenseNumber;
    public int redundant;   // 防御之后剩余的攻击数值

    void Start()
    {
        defenseNumber = 0;
        redundant = 0;
    }

    // 增加防御值
    public void AddDefense(int count)
    {
        defenseNumber = defenseNumber + count;
    }

    // 减少防御值
    public void RemoveDefense(int count)
    {
        if (defenseNumber > count)
        {
            defenseNumber = defenseNumber - count;
        }
        else if (defenseNumber <= count)
        {
            redundant = count - defenseNumber;
            defenseNumber = 0;
        }
    }

    // 定义防御值
    public void DefineDefense(int count)
    {
        defenseNumber = count;
    }
}
