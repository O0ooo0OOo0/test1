using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoseCard : MonoBehaviour
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
            isused = true;
        }
    }

    public void JudgeCanBeUse()
    {
        if (targetDetector.targetContent == "指定一个敌人" && targetDetector.UseObject.CompareTag("Enemy"))
        {
            if (targetDetector.elementContent == "生命")
            {
                cardEffect.TakeDamage_Eone(targetDetector.UseObject, targetDetector.numberContent);   //减敌人血

                recordAction.enemy = targetDetector.UseObject;
                recordAction.Action("指定一个敌人", targetDetector.numberContent, "生命", 1);

                BeUsed();
            }
            else if (targetDetector.elementContent == "盾")
            {
                cardEffect.RemoveDefense_Eone(targetDetector.UseObject, targetDetector.numberContent);   //减敌人盾

                recordAction.enemy = targetDetector.UseObject;
                recordAction.Action("指定一个敌人", targetDetector.numberContent, "盾", 1);

                BeUsed();
            }
        }

        else if (targetDetector.targetContent == "自己" && targetDetector.UseObject.CompareTag("Player"))
        {
            if (targetDetector.elementContent == "生命")
            {
                cardEffect.TakeDamage_P(targetDetector.numberContent);

                recordAction.enemy = null;
                recordAction.Action("自己", targetDetector.numberContent, "生命", 1);

                BeUsed();
            }
            else if (targetDetector.elementContent == "盾")
            {
                cardEffect.RemoveDefense_P(targetDetector.numberContent);

                recordAction.enemy = null;
                recordAction.Action("自己", targetDetector.numberContent, "盾", 1);

                BeUsed();
            }
        }

        else if (targetDetector.targetContent == "所有敌人" && targetDetector.UseObject.CompareTag("Many"))
        {
            if (targetDetector.elementContent == "生命")
            {
                cardEffect.TakeDamage_Eall(targetDetector.numberContent);

                recordAction.enemy = null;
                recordAction.Action("所有敌人", targetDetector.numberContent, "生命", 1);

                BeUsed();
            }
            else if (targetDetector.elementContent == "盾")
            {
                cardEffect.RemoveDefense_Eall(targetDetector.numberContent);

                recordAction.enemy = null;
                recordAction.Action("所有敌人", targetDetector.numberContent, "盾", 1);

                BeUsed();
            }
        }

        else if (targetDetector.targetContent == "所有人" && targetDetector.UseObject.CompareTag("Many"))
        {
            if (targetDetector.elementContent == "生命")
            {
                cardEffect.TakeDamage_All(targetDetector.numberContent);

                recordAction.enemy = null;
                recordAction.Action("所有人", targetDetector.numberContent, "生命" , 1);

                BeUsed();
            }
            else if (targetDetector.elementContent == "盾")
            {
                cardEffect.RemoveDefense_All(targetDetector.numberContent);

                recordAction.enemy = null;
                recordAction.Action("所有人", targetDetector.numberContent, "盾", 1);

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
