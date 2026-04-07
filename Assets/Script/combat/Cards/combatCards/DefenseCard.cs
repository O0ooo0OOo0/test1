using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefenseCard : MonoBehaviour
{
    public TargetDetector targetDetector;
    public UseCardDraggable useCardDraggable;
    public CardEffect cardEffect;
    public BasisCardsCreate cardsCreate;

    public APManager ap;
    public RecordAction recordAction;

    public int consume = 1;
    public bool isused;

    void Start()
    {
        isused = false;
    }

    void Update()
    {
        if (isused == false && targetDetector.isUseObjectInside && useCardDraggable.isDragging == false && targetDetector.UseObject.CompareTag("Player") && ap.currentValue >= consume)
        {
            JudgeCanBeUse();
            isused = true;
        }
    }

    public void JudgeCanBeUse()
    {
        cardEffect.AddDefense_P(targetDetector.numberContent);

        recordAction.enemy = null;
        recordAction.Action("自己", targetDetector.numberContent, "盾", 2);

        ap.TakeAP(consume);
        //Debug.Log(ap.value);

        // 获取当前卡牌所在的位置索引
        Transform parent = transform.parent;
        int index = System.Array.IndexOf(cardsCreate.cardPositions, parent);

        // 通知 BasisCardsCreate 重新生成卡牌
        BasisCardsCreate cardCreator = cardsCreate;
        if (cardCreator != null && index != -1)
        {
            cardCreator.ReplaceCardAt(index);
        }

        // 销毁当前卡牌
        Destroy(gameObject);
    }
}
