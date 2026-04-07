using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemAmountText : MonoBehaviour
{
    public TMP_Text[] propText;            // 道具数量
    public TMP_Text[] talismanText;        // 护符数量
    public TMP_Text[] materialText;      // 材料数量
    public TMP_Text[] itemText;      // 物品数量 （非战斗场景使用的物品）

    // 获取物品数量并传入文本显示
    public void AmountTextItem()
    {
        // 道具
        for (int i = 0; i < propText.Length; i++)
        {
            propText[i].text = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].props[i].propAmount.ToString();
        }

        // 护符
        for (int i = 0; i < talismanText.Length; i++)
        {
            talismanText[i].text = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].talismans[i].talismanAmount.ToString();
        }

        // 材料
        for (int i = 0  ; i < materialText.Length; i++)   // 其他锻造材料数
        {
            materialText[i].text = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].materials[i].materialAmount.ToString();
        }

        // 物品（非战斗）
        itemText[0].text = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].coins.ToString();   // 铜币数

        for (int i = 1; i < itemText.Length; i++)
        {
            Transform itemPP = itemText[i].gameObject.transform.parent.parent;   // 获取文本所处的游戏对象
            int itemAmount = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].items[i - 1].itemAmount;

            if (itemAmount > 0)   // 物品数量大于0 
            {
                itemPP.gameObject.SetActive(true);   // 文本所处的游戏对象是可见的
                itemText[i].text = itemAmount.ToString();
            }
            else
            {
                itemPP.gameObject.SetActive(false);
                itemText[i].text = "";
            }
        }
    }
}
