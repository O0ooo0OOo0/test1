using GameFramework.Samples.Localization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ForgingProducts : MonoBehaviour
{
    // 锻造产物信息
    public GameObject product;
    public TypeManager.ForgingProductType type;     // 产物类别
    public int index;            // 产物类别下的索引编号
    public string nameCN, nameEN;
    public string nameP;
    public int amount;
    public TMP_Text productContent;

    void Start()
    {
        PdtName(LanguageType.lt.lanType);
    }

    // 使用语言判断
    public void PdtName(int lanT)
    {
        if (lanT == 0)   // 中文
        {
            nameP = nameCN;
        }
        else if (lanT == 1)   // 英文
        {
            nameP = nameEN;
        }
    }
}
