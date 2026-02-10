using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberCards : MonoBehaviour
{
    [System.Serializable]
    public struct NumberCardInfo
    {
        public GameObject numberCard; 
        public int cardValue;
        public int cardCount;
        public TMP_Text amount;
        public bool isInfinite; // 新增无限标记字段
    }

    public List<NumberCardInfo> numberCards;

    void Start()
    {
        if (numberCards.Count > 0)
        {
            NumberCardInfo firstCard = numberCards[0];
            firstCard.isInfinite = true;
            firstCard.amount.text = "∞"; 
            numberCards[0] = firstCard;
        }

        if (numberCards.Count > 3)
        {
            NumberCardInfo fourthCard = numberCards[3];
            fourthCard.isInfinite = true;
            fourthCard.amount.text = "∞"; 
            numberCards[3] = fourthCard;
        }

        // 处理其他卡牌
        for (int i = 0; i < numberCards.Count; i++)
        {
            if (i != 0 && i != 3) 
            {
                NumberCardInfo card = numberCards[i];
                card.amount.text = card.cardCount.ToString();
                numberCards[i] = card;
            }
        }
    }

    void Update()
    {
        
    }
}
