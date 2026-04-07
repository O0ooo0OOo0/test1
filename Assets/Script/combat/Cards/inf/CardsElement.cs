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
        amountText.text = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].eleCards[elementCardType].eleCardAmount.ToString();
    }

    public void UseElementCard()
    {
        var elecard = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].eleCards[elementCardType];
        elecard.eleCardAmount--;
        amountText.text = elecard.eleCardAmount.ToString();
        ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].eleCards[elementCardType] = elecard;
    }

    public void ResetElementCard()
    {
        var elecard = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].eleCards[elementCardType];
        elecard.eleCardAmount++;
        amountText.text = elecard.eleCardAmount.ToString();
        ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].eleCards[elementCardType] = elecard;
    }
}
