using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardsElement : MonoBehaviour
{
    public GameObject elementCard;
    public string cardName;
    public int elementCardType;
    public TMP_Text amountText;

    public CardDraggable cardDraggable;

    void Start()
    {
        DefineAmountText();
    }

    public void HideTextAmount()
    {
        amountText.gameObject.SetActive(false);
    }

    public void ShowTextAmount()
    {
        amountText.gameObject.SetActive(true);
    }

    public void DefineAmountText()
    {
        amountText.text = PersistentObject.instance.elementCard[elementCardType].ToString();
    }

    public void UseElementCard()
    {
        PersistentObject.instance.elementCard[elementCardType]--;
        amountText.text = PersistentObject.instance.elementCard[elementCardType].ToString();
    }

    public void ResetElementCard()
    {
        PersistentObject.instance.elementCard[elementCardType]++;
        amountText.text = PersistentObject.instance.elementCard[elementCardType].ToString();
    }
}
