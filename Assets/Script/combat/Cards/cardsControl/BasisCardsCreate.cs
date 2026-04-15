using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasisCardsCreate : MonoBehaviour
{
    // 定义一个结构体来存储牌的类型和消耗点数
    [System.Serializable]
    public struct CardInfo
    {
        public GameObject card; // 牌
        public int cardValue;   // 牌的消耗点数
    }

    public List<CardInfo> cardDeckInfo; // 合并后的牌组信息
    public Transform[] cardPositions;   // 固定位置的Transform
    public float fadeDuration = 1.0f;  // 渐显渐隐的持续时间

    void Start()
    {
        // 检查牌组和位置是否已经设置
        if (cardDeckInfo == null || cardDeckInfo.Count == 0)
        {
            Debug.LogError("牌组未设置或为空！");
            return;
        }
        if (cardPositions == null || cardPositions.Length != 5)
        {
            Debug.LogError("卡槽位置未设置或数量不为5！");
            return;
        }

        foreach (CardInfo cardInfo in cardDeckInfo)
        {
            if (cardInfo.card != null) 
            {
                cardInfo.card.SetActive(false);
            }
        }

        // 随机分配牌到五个位置
        for (int i = 0; i < cardPositions.Length; i++)
        {
            int randomIndex = Random.Range(0, cardDeckInfo.Count);            // 随机选择一张牌
            GameObject selectedCard = cardDeckInfo[randomIndex].card;

            GameObject newCard = Instantiate(selectedCard, cardPositions[i]);            // 创建一个新按钮并设置为选中的牌
            newCard.transform.localPosition = Vector3.zero;
            newCard.transform.localRotation = Quaternion.identity;
            newCard.transform.localScale = Vector3.one;

            //newCard.SetActive(true);
            StartCoroutine(FadeIn(newCard, fadeDuration));
        }
    }

    private IEnumerator FadeIn(GameObject card, float duration)
    {
        card.SetActive(true);
        Image cardImage = card.GetComponent<Image>();
        cardImage.color = new Color(cardImage.color.r, cardImage.color.g, cardImage.color.b, 0);
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cardImage.color = new Color(cardImage.color.r, cardImage.color.g, cardImage.color.b, elapsedTime / duration);
            yield return null;
        }

        cardImage.color = new Color(cardImage.color.r, cardImage.color.g, cardImage.color.b, 1);
    }

    // 牌使用后生成新牌
    public void ReplaceCardAt(int index)
    {
        if (index < 0 || index >= cardPositions.Length)
        {
            return;
        }

        Transform slot = cardPositions[index];

        // 清空该位置已有的卡牌（防止残留）
        foreach (Transform child in slot)
        {
            Destroy(child.gameObject);
        }

        // 随机选择一张卡牌
        int randomIndex = Random.Range(0, cardDeckInfo.Count);
        GameObject selectedCard = cardDeckInfo[randomIndex].card;

        // 实例化新卡牌
        GameObject newCard = Instantiate(selectedCard, slot);
        newCard.transform.localPosition = Vector3.zero;
        newCard.transform.localRotation = Quaternion.identity;
        newCard.transform.localScale = Vector3.one;

        // 渐显动画
        StartCoroutine(FadeIn(newCard, fadeDuration));
    }
}