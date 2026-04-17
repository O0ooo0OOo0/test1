using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RefreshCombatCardsManager : MonoBehaviour
{
    public Button refreshCards;
    public int times;   // 当前刷新次数
    public TMP_Text timesText;

    // 调用其他脚本
    public BasisCardsCreate cardsCreate;

    void Start()
    {
        InitializedTimes();

        if (refreshCards != null)
        {
            refreshCards.onClick.AddListener(ReduceTimes);
        }
    }

    // 初始化刷新次数
    public void InitializedTimes()
    {
        times = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].refreshCombatCardsTimes;
        timesText.text = "*" + times.ToString();    
    }

    // 消耗刷新次数并刷新
    public void ReduceTimes()
    {
        if (times > 0)
        {
            times--;
            timesText.text = "*" + times.ToString();
            RefreshCombatCards();
        }
    }

    // 刷新战斗牌
    public void RefreshCombatCards()
    {
        Transform[] cards = cardsCreate.cardPositions;

        for (int i = 0; i < cards.Length; i++)
        {
            Transform slot = cards[i];
            Transform card = slot.GetChild(0);

            // 检测战斗牌是否可被刷新
            bool isCanRef = card.gameObject.GetComponent<UseCardDraggable>().isCanRefresh;

            // 刷新可被刷新的战斗牌
            if (isCanRef)
            {
                cardsCreate.ReplaceCardAt(i);
            }
        }
    }

    // 恢复刷新次数（每两回合刷新一次）
    public void ResetTimes(int currentRound)
    {
        int round = currentRound % 2;   // 当前回合数除以二的余数

        if (round == 1)
        {
            times = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].refreshCombatCardsTimes; 
        }
    }
}
