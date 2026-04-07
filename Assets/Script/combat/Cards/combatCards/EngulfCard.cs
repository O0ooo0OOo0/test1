using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngulfCard : MonoBehaviour
{
    public TargetDetector targetDetector;
    public UseCardDraggable useCardDraggable;
    public CardEffect cardEffect;
    public BasisCardsCreate cardsCreate;

    public APManager ap;
    public RecordAction recordAction;

    public int consume = 3;
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
        }
    }

    public void JudgeCanBeUse()
    {
        if (targetDetector.targetContent == "指定一个敌人" && targetDetector.UseObject.CompareTag("Enemy"))
        {
            isused = true;
            cardEffect.TakeDamage_Eone(targetDetector.UseObject, targetDetector.numberContent);   //减敌人血
            cardEffect.Heal_P(targetDetector.numberContent);   //加自己血

            recordAction.enemy = targetDetector.UseObject;
            recordAction.Action("指定一个敌人", targetDetector.numberContent, "生命", 3);

            BeUsed();
        }

        else if (targetDetector.targetContent == "所有敌人" && targetDetector.UseObject.CompareTag("Many"))
        {
            isused = true;
            cardEffect.TakeDamage_Eall(targetDetector.numberContent);   //减所有敌人血
            int all = targetDetector.numberContent * cardEffect.enemyCount;
            cardEffect.Heal_P(all);   //加自己血

            recordAction.enemy = null;
            recordAction.Action("所有敌人", targetDetector.numberContent, "生命", 3);

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
