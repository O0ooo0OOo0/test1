using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferingsCard : MonoBehaviour
{
    public TargetDetector targetDetector;
    public UseCardDraggable useCardDraggable;
    public CardEffect cardEffect;
    public BasisCardsCreate cardsCreate;

    public APnumber ap;
    public RecordAction recordAction;

    public int consume = 1;
    public bool isused;

    void Start()
    {
        isused = false;
    }

    void Update()
    {
        if (isused == false && targetDetector.isUseObjectInside && useCardDraggable.isDragging == false && ap.value >= consume)
        {
            JudgeCanBeUse();
        }
    }

    public void JudgeCanBeUse()
    {
        if (targetDetector.targetContent == "指定一个敌人" && targetDetector.UseObject.CompareTag("Enemy"))
        {
            isused = true;

            cardEffect.Reduceblood_P(targetDetector.numberContent);
            int hurt = targetDetector.numberContent * 2;
            cardEffect.TakeDamage_Eone(targetDetector.UseObject, hurt);

            recordAction.enemy = targetDetector.UseObject;
            recordAction.Action("指定一个敌人", targetDetector.numberContent, "生命", 4);
            BeUsed();
        }
        else if (targetDetector.targetContent == "所有敌人" && targetDetector.UseObject.CompareTag("Many"))
        {
            isused = true;

            cardEffect.Reduceblood_P(targetDetector.numberContent);
            int hurt = targetDetector.numberContent * 2;
            cardEffect.TakeDamage_Eall(hurt);

            recordAction.enemy = null;
            recordAction.Action("所有敌人", targetDetector.numberContent, "生命", 4);
            BeUsed();
        }
    }

    public void BeUsed()
    {
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
