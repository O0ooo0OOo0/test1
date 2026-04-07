using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardsBook : MonoBehaviour
{
    [System.Serializable]
    public class CardsContent
    { 
        public int cardIndex;   // 卡牌对应编号
        public Button cardButton;   // 卡牌对应按键
        public bool isObtain;   // 卡牌是否被获取
        public GameObject ques;   // 卡牌未被获取时
        public GameObject cardText;
        public GameObject cardSign;   // 卡牌被选中的标志
    }

    public GameObject noObtainText;  // 未获取的描述
    public List<CardsContent> cardsInf;

    void Start()
    {
        CardsBookButton();
    }

    // 卡牌按键功能绑定
    public void CardsBookButton()
    {
        for (int i = 0; i < cardsInf.Count; i++)
        {
            if (cardsInf[i].cardButton != null)
            {
                int index = i;
                cardsInf[i].cardButton.onClick.AddListener(() => CardsDetails(cardsInf[index].cardIndex, cardsInf[index].isObtain));
            }
        }
    }

    // 获取存档中的卡牌信息+更新界面显示
    public void GetBookCardsInf()
    {
        // 获取存档中的卡牌信息
        for (int i = 0; i < cardsInf.Count; i++)
        {
            cardsInf[i].isObtain = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].cards[i].isCardObtain;
        }

        // 界面显示
        noObtainText.SetActive(false);
        for (int i = 0; i < cardsInf.Count; i++)
        {
            cardsInf[i].cardText.SetActive(false);
            cardsInf[i].cardSign.SetActive(false);
            cardsInf[i].ques.SetActive(!cardsInf[i].isObtain);
        }
    }

    // 根据不同卡牌更新对应描述
    public void CardsDetails(int id, bool isObtain)
    {
        // 更新文本信息
        if (cardsInf[id].isObtain == true)   // 如果该卡牌已获取
        {
            noObtainText.SetActive(false);  // 未获取的描述

            for (int i = 0; i < cardsInf.Count; i++)
            {
                if (cardsInf[i].cardIndex == id)
                {
                    cardsInf[i].cardText.SetActive(true);
                }
                else
                {
                    cardsInf[i].cardText.SetActive(false);
                }
            }
        }
        else if (cardsInf[id].isObtain == false)   // 如果该卡牌未获取
        {
            noObtainText.SetActive(true);

            for (int i = 0; i < cardsInf.Count; i++)
            {
                cardsInf[i].cardText.SetActive(false);
            }
        }

        // 更新选中标志
        for (int i = 0; i < cardsInf.Count; i++)
        {
            if (cardsInf[i].cardIndex == id)
            {
                cardsInf[i].cardSign.SetActive(true);
            }
            else
            {
                cardsInf[i].cardSign.SetActive(false);
            }
        }
    }
}
