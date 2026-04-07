using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardsNumber : MonoBehaviour
{
    public GameObject numberCard;
    public int numberCardValue;
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
        if (numberCardValue == 1)
        {
            amountText.text = "¡Þ";
        }
        else if (numberCardValue == 4)
        {
            amountText.text = "¡Þ";
        }
        else
        {
            amountText.text = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].numCards[numberCardValue-1].numCardAmount.ToString();
        }
    }

    public void UseNumberCard()
    {
        if (numberCardValue != 1 && numberCardValue != 4)
        {
            var numcard = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].numCards[numberCardValue - 1];
            numcard.numCardAmount--;
            amountText.text = numcard.numCardAmount.ToString();
            ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].numCards[numberCardValue - 1] = numcard;
        }
    }

    public void ResetNumberCard()
    {
        if (numberCardValue != 1 && numberCardValue != 4)
        {
            var numcard = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].numCards[numberCardValue - 1];
            numcard.numCardAmount++;
            amountText.text = numcard.numCardAmount.ToString();
            ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].numCards[numberCardValue - 1] = numcard;
        }
    }
}
