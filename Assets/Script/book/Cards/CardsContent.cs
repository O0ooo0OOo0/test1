using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CardsBookContent : MonoBehaviour
{
    // 卡牌描述
    public GameObject noObtainText;  // 未获取的描述
    public GameObject[] cardsText;   // 卡牌描述组
    public GameObject[] cardsSign;   // 选中卡牌的标志

    void Start()
    {
        CardsBookInitialize();
    }

    // 卡牌图鉴界面初始化
    public void CardsBookInitialize()
    {
        noObtainText.SetActive(false);
        foreach (var text in cardsText)
        {
            text.SetActive(false);
        }
        foreach (var sign in cardsSign)
        {
            sign.SetActive(false);
        }
    }

    // 根据不同卡牌更新对应描述
    public void CardsDetails(int id, bool isObtain)
    {
        // 更新文本描述
        if (isObtain == true)   // 如果该卡牌已获取
        {
            noObtainText.SetActive(false);  // 未获取的描述

            for (int i = 0; i < cardsText.Length; i++)
            {
                if (i == id)
                {
                    cardsText[i].SetActive(true);
                }
                else
                {
                    cardsText[i].SetActive(false);
                }
            }
        }
        else if (isObtain == false)   // 如果该卡牌未获取
        {
            noObtainText.SetActive(true);

            foreach (var text in cardsText)
            {
                text.SetActive(false);
            }
        }

        // 更新选中标志
        for (int i = 0; i < cardsSign.Length; i++)
        {
            if (i == id)
            {
                cardsSign[i].SetActive(true);
            }
            else
            {
                cardsSign[i].SetActive(false);
            }
        }
    }
}
