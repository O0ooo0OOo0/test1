using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static NumberCards;

public class ElementCards : MonoBehaviour
{
    [System.Serializable]
    public struct ElementCardInfo
    {
        public GameObject elementCard;
        public int eCardType;
        public int cardCount;
        public TMP_Text amount;
    }

    public List<ElementCardInfo> elementCards;

    void Start()
    {
        for (int i = 0; i < elementCards.Count; i++)
        {
            ElementCardInfo card = elementCards[i];
            card.amount.text = card.cardCount.ToString();
            elementCards[i] = card;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
