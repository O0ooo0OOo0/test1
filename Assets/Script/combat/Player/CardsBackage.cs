using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsBackage : MonoBehaviour
{
    public Button cardsBackage;
    public GameObject cardsBackagePanel;
    public bool isOpenCardsBackage;

    void Start()
    {
        isOpenCardsBackage = false;
        cardsBackagePanel.SetActive(false);

        if (cardsBackage != null)
        {
            cardsBackage.onClick.AddListener(SwitchCardsBackage);
        }
    }

    public void SwitchCardsBackage()
    {
        if (isOpenCardsBackage == true)
        {
            cardsBackagePanel.SetActive(false);
            isOpenCardsBackage = false;
        }
        else if (isOpenCardsBackage == false)
        {
            cardsBackagePanel.SetActive(true);
            isOpenCardsBackage = true;
        }
    }
}
