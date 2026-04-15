using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TalismanUseManager : MonoBehaviour
{
    [System.Serializable]
    public class TalismansInf
    {
        public GameObject talisman;
        public Button taliButton;
        public int indexT;
        public int amountT;
        public bool isUsed;
    }

    public TalismansInf[] talis;   // 护符信息组
    public int canUseAmount;   // 可同时使用的护符的数量
    public int currentUseAmount;   // 当前已使用的护符的数量
    public TMP_Text canUseText;

    void Start()
    {
        canUseAmount = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].canUseTalismansAmount;
        canUseText.text = canUseAmount.ToString();

        GetTalismanType();
        JudgeIsShowTalisman();
        GetTalismansUsing();

        foreach (var tali in talis)
        {
            tali.taliButton.onClick.AddListener(() => RefreshTalismansUsing(tali.indexT));
        }
    }

    // 获取当前护符数量（是否获取该护符）
    public void GetTalismanType()
    {
        for (int i = 0; i < talis.Length; i++)
        {
            talis[i].amountT = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].talismans[talis[i].indexT].talismanAmount;
        }
    }

    // 判断当前护符是否显示（如果护符数量为0则不显示该护符）
    public void JudgeIsShowTalisman()
    {
        for (int i = 0; i < talis.Length; i++)
        {
            if (talis[i].amountT != 0)   // 显示
            {
                talis[i].talisman.SetActive(true);
            }
            else   // 不显示
            {
                talis[i].talisman.SetActive(false);
            }
        }
    }

    // 获取护符使用情况
    public void GetTalismansUsing()
    {
        currentUseAmount = 0;   // 初始化护符使用数量
        foreach (var tali in talis)
        {
            if (tali.amountT != 0)   // 如果护符被获取
            {
                tali.isUsed = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].talismans[tali.indexT].isUsed;

                if (tali.isUsed == true)   // 如果护符被使用
                {
                    tali.taliButton.gameObject.GetComponent<ClickToggleOutline>().ShowOutline();
                    currentUseAmount++;
                }
                else
                {
                    tali.taliButton.gameObject.GetComponent<ClickToggleOutline>().HideOutline();
                }
            }
        }

        JudgeTalimanUsing();
    }

    // 更新当前护符使用情况
    public void RefreshTalismansUsing(int id)
    {
        for (int i = 0; i < talis.Length; i++)
        {
            if (talis[i].indexT == id)   // 找到被点击的护符
            {
                if (talis[i].isUsed == false)
                {
                    talis[i].isUsed = true;
                    currentUseAmount++;
                }
                else if (talis[i].isUsed == true)
                {
                    talis[i].isUsed = false;
                    currentUseAmount--;
                }
            }
        }

        JudgeTalimanUsing();
    }

    // 判断是否可继续增加护符使用
    public void JudgeTalimanUsing()
    {
        if (currentUseAmount < canUseAmount)   // 未达上限：可继续增加
        {
            foreach (var tali in talis)
            {
                tali.taliButton.interactable = true;
            }
        }
        else if (currentUseAmount == canUseAmount)   // 达到上限：不可继续增加
        {
            foreach (var tali in talis)   // 未被使用的护符禁止再被使用
            {
                if (tali.isUsed == false)
                {
                    tali.taliButton.interactable = false;
                }
                else
                {
                    tali.taliButton.interactable = true;
                }
            }
        }
    }
}
