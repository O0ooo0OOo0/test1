using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCard : MonoBehaviour
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
        if (isused == false && targetDetector.isUseObjectInside && useCardDraggable.isDragging == false && ap.currentValue >= consume)
        {
            JudgeCanBeUse();
            isused = true;
        }
    }

    public void JudgeCanBeUse()
    {
        if (targetDetector.targetContent == "指定一个敌人" && targetDetector.UseObject.CompareTag("Enemy"))
        {
            if (targetDetector.elementContent == "生命")
            {
                cardEffect.Heal_Eone(targetDetector.UseObject, targetDetector.numberContent);

                recordAction.enemy = targetDetector.UseObject;
                recordAction.Action("指定一个敌人", targetDetector.numberContent, "生命", 2);

                BeUsed();
            }
            else if (targetDetector.elementContent == "盾")
            {
                cardEffect.AddDefense_Eone(targetDetector.UseObject, targetDetector.numberContent);

                recordAction.enemy = targetDetector.UseObject;
                recordAction.Action("指定一个敌人", targetDetector.numberContent, "盾", 2);

                BeUsed();
            }
        }

        else if (targetDetector.targetContent == "自己" && targetDetector.UseObject.CompareTag("Player"))
        {
            if (targetDetector.elementContent == "生命")
            {
                cardEffect.Heal_P(targetDetector.numberContent);

                recordAction.enemy = null;
                recordAction.Action("自己", targetDetector.numberContent, "生命", 2);

                BeUsed();
            }
            else if (targetDetector.elementContent == "盾")
            {
                cardEffect.AddDefense_P(targetDetector.numberContent);

                recordAction.enemy = null;
                recordAction.Action("自己", targetDetector.numberContent, "盾", 2);

                BeUsed();
            }
        }

        else if (targetDetector.targetContent == "所有敌人" && targetDetector.UseObject.CompareTag("Many"))
        {
            if (targetDetector.elementContent == "生命")
            {
                cardEffect.Heal_Eall(targetDetector.numberContent);

                recordAction.enemy = null;
                recordAction.Action("所有敌人", targetDetector.numberContent, "生命", 2);

                BeUsed();
            }
            else if (targetDetector.elementContent == "盾")
            {
                cardEffect.AddDefense_Eall(targetDetector.numberContent);

                recordAction.enemy = null;
                recordAction.Action("所有敌人", targetDetector.numberContent, "盾", 2);

                BeUsed();
            }
        }

        else if (targetDetector.targetContent == "所有人" && targetDetector.UseObject.CompareTag("Many"))
        {
            if (targetDetector.elementContent == "生命")
            {
                cardEffect.Heal_All(targetDetector.numberContent);

                recordAction.enemy = null;
                recordAction.Action("所有人", targetDetector.numberContent, "生命", 2);

                BeUsed();
            }
            else if (targetDetector.elementContent == "盾")
            {
                cardEffect.AddDefense_All(targetDetector.numberContent);

                recordAction.enemy = null;
                recordAction.Action("所有人", targetDetector.numberContent, "盾", 2);

                BeUsed();
            }
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
