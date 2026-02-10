using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetCards : MonoBehaviour
{
    [System.Serializable]
    public struct TargetCardInfo
    {
        public GameObject targetCard;
        public int cardType;
        public int cardCount;
        public TMP_Text amount;
        public bool isInfinite; // 新增无限标记字段
    }

    public List<TargetCardInfo> targetCards;

    void Start()
    {
        if (targetCards.Count > 0)
        {
            TargetCardInfo firstCard = targetCards[0];
            firstCard.isInfinite = true;
            firstCard.amount.text = "∞";
            targetCards[0] = firstCard;
        }

        for (int i = 0; i < targetCards.Count; i++)
        {
            if (i != 0)
            {
                TargetCardInfo card = targetCards[i];
                card.amount.text = card.cardCount.ToString();
                targetCards[i] = card;
            }
        }
    }

    void Update()
    {

    }
}
