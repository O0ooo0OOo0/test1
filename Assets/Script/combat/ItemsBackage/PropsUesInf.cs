using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PropsUesInf : MonoBehaviour
{
    public GameObject prop;
    public int indexP;
    public int amountP;
    public TMP_Text amountTextP;

    void Start()
    {
        GetAmount();
        JudgeIsShowProp();
    }

    // 获取当前道具数量
    public void GetAmount()
    {
        amountP = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].props[indexP].propAmount;
    }

    // 判断当前道具是否显示（如果道具数量为0则不显示该道具）
    public void JudgeIsShowProp()
    {
        if (amountP != 0)   // 显示
        {
            prop.SetActive(true);
            amountTextP.text = amountP.ToString();
        }
        else   // 不显示
        {
            prop.SetActive(false);
        }
    }

    // 更新当前道具数量
    public void RefreshAmountText()
    {

    }
}
