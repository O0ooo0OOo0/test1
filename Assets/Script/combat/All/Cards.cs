using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour
{
    public APnumber APnumber;

    public List<Button> cardDeck;    // 定义牌组中的所有牌（Button）
    public Transform[] cardPositions;    // 定义五个固定位置的Transform
    public int[] cardValues;    // 定义每种牌对应的数字属性
    public float fadeDuration = 1.0f;    // 渐显渐隐的持续时间
    public int cardDepleteCount;    // 当前卡牌的消耗值
    public bool isClick;

    void Start()
    {
        isClick = false;

        // 检查牌组和位置是否已经设置
        if (cardDeck == null || cardDeck.Count == 0)
        {
            return;
        }
        if (cardPositions == null || cardPositions.Length != 5)
        {
            return;
        }
        if (cardValues == null || cardValues.Length != cardDeck.Count)
        {
            return;
        }

        foreach (Button card in cardDeck)
        {
            card.gameObject.SetActive(false);
        }

        // 随机分配牌到五个位置
        for (int i = 0; i < cardPositions.Length; i++)
        {
            int randomIndex = Random.Range(0, cardDeck.Count);            // 随机选择一张牌
            Button selectedCard = cardDeck[randomIndex];

            Button newCard = Instantiate(selectedCard, cardPositions[i]);            // 创建一个新按钮并设置为选中的牌
            newCard.transform.localPosition = Vector3.zero;
            newCard.transform.localRotation = Quaternion.identity;
            newCard.transform.localScale = Vector3.one;

            StartCoroutine(FadeIn(newCard, fadeDuration));
            int positionIndex = i; // 避免闭包问题
            newCard.onClick.AddListener(() => OnCardClicked(newCard, positionIndex, randomIndex));
        }
    }

    private IEnumerator FadeIn(Button card, float duration)
    {
        card.gameObject.SetActive(true);
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

    private IEnumerator FadeOut(Button card, float duration)
    {
        Image cardImage = card.GetComponent<Image>();
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cardImage.color = new Color(cardImage.color.r, cardImage.color.g, cardImage.color.b, 1 - elapsedTime / duration);
            yield return null;
        }

        cardImage.color = new Color(cardImage.color.r, cardImage.color.g, cardImage.color.b, 0);
        Destroy(card.gameObject);
    }

    // 点击事件处理
    private void OnCardClicked(Button clickedCard, int positionIndex, int cardIndex)
    {
        if (APnumber.value >= cardValues[cardIndex])
        {
            cardDepleteCount = cardValues[cardIndex];
            APnumber.value = APnumber.value - cardDepleteCount;
            isClick = true;

            StartCoroutine(FadeOut(clickedCard, fadeDuration));
            StartCoroutine(ReplaceCard(positionIndex));
        }
    }

    // 替换牌
    private IEnumerator ReplaceCard(int positionIndex)
    {
        yield return new WaitForSeconds(fadeDuration);
        isClick = false;

        foreach (Transform child in cardPositions[positionIndex])
        {
            Destroy(child.gameObject);
        }

        // 随机选择一张新牌
        int randomIndex = Random.Range(0, cardDeck.Count);
        Button newCard = cardDeck[randomIndex];

        // 创建一个新按钮并设置为选中的牌
        Button instantiatedCard = Instantiate(newCard, cardPositions[positionIndex]);
        instantiatedCard.transform.localPosition = Vector3.zero;
        instantiatedCard.transform.localRotation = Quaternion.identity;
        instantiatedCard.transform.localScale = Vector3.one;

        StartCoroutine(FadeIn(instantiatedCard, fadeDuration));
        instantiatedCard.onClick.AddListener(() => OnCardClicked(instantiatedCard, positionIndex, randomIndex));
    }
}
