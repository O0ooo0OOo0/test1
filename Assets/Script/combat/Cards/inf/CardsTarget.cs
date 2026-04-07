using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardsTarget : MonoBehaviour
{
    public GameObject targetCard;
    public string cardName;
    public int targetCardType;
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
        if (targetCardType == 0)
        {
            amountText.text = "¡Þ";
        }
        else if (targetCardType != 0)
        {
            amountText.text = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].tarCards[targetCardType].tarCardAmount.ToString();
        }
    }

    public void UseTargetCard()
    {
        if (targetCardType != 0)
        {
            var tarcard = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].tarCards[targetCardType];
            tarcard.tarCardAmount--;
            amountText.text = tarcard.tarCardAmount.ToString();
            ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].tarCards[targetCardType] = tarcard;
        }
    }

    public void ResetTargetCard()
    {
        if (targetCardType != 0)
        {
            var tarcard = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].tarCards[targetCardType];
            tarcard.tarCardAmount++;
            amountText.text = tarcard.tarCardAmount.ToString();
            ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].tarCards[targetCardType] = tarcard;
        }
    }
}
