using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardsBagAmountText : MonoBehaviour
{
    public TMP_Text[] targetCardText;   // 目标牌
    public TMP_Text[] numberCardText;   // 数值牌
    public TMP_Text[] elementCardText;   // 元素牌

    public void AmountTextCards()
    {
        // 目标牌
        for (int i = 0; i < targetCardText.Length; i++)
        {
            if (i == 0)   // “你”数量为无穷
            {
                targetCardText[0].text = "∞";
            }
            else if (i != 0)
            {
                targetCardText[i].text = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].tarCards[i].tarCardAmount.ToString();
            }
        }

        // 数值牌
        for (int i = 0; i < numberCardText.Length; i++)
        {
            if (i == 0)   // “1”数量无穷
            {
                numberCardText[0].text = "∞";
            }
            else if (i == 3)   // “4”数量无穷
            {
                numberCardText[3].text = "∞";
            }
            else if (i != 0 && i != 3)
            {
                numberCardText[i].text = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].numCards[i].numCardAmount.ToString();
            }
        }

        // 元素牌
        for (int i = 0; i < elementCardText.Length; i++)
        {
            elementCardText[i].text = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].eleCards[i].eleCardAmount.ToString();
        }
    }
}
