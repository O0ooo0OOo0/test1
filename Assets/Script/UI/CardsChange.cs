using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CardsContent : MonoBehaviour
{
    public Button cardsBook;
    public Button close_cardsBook;
    public GameObject cardsBookPanel;
    public TMP_Text nameTitle;
    public TMP_Text content;

    void Start()
    {
        cardsBookPanel.SetActive(false);
        CardsBookInitialize();

        if (cardsBook != null)
        {
            cardsBook.onClick.AddListener(OpenCardsBookPanel);
        }
        if (close_cardsBook != null)
        {
            close_cardsBook.onClick.AddListener(CloseCardsBookPanel);
        }
    }

    public void CardsBookInitialize()
    {
        nameTitle.text = "攻击卡";
        content.text = "最常见的普通攻击，对指定目标造成x点伤害";
    }


    public void OpenCardsBookPanel()
    {
        cardsBookPanel.SetActive(true);
    }

    public void CloseCardsBookPanel()
    {
        cardsBookPanel.SetActive(false); 
        CardsBookInitialize();
    }
}
