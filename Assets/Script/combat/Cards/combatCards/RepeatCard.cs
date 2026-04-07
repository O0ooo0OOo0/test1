using UnityEngine;

public class RepeatCard : MonoBehaviour
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
        if (isused == false && targetDetector.isUseObjectInside && useCardDraggable.isDragging == false && targetDetector.UseObject.CompareTag("Many") && recordAction.type != 0 && ap.currentValue >= consume)
        {
            JudgeCanBeUse();
        }
    }

    public void JudgeCanBeUse()
    {
        if (recordAction.target == "指定一个敌人")
        {
            if (recordAction.element == "生命")
            {
                isused = true;
                if (recordAction.type == 1)
                {
                    cardEffect.TakeDamage_Eone(recordAction.enemy, recordAction.number);
                }
                else if (recordAction.type == 2)
                {
                    cardEffect.Heal_Eone(recordAction.enemy, recordAction.number);
                }
                else if (recordAction.type == 3)
                {
                    cardEffect.TakeDamage_Eone(recordAction.enemy, recordAction.number);
                    cardEffect.Heal_P(recordAction.number);
                }
                else if (recordAction.type == 4)
                {
                    cardEffect.Reduceblood_P(recordAction.number);
                    int hurt = recordAction.number * 2;
                    cardEffect.TakeDamage_Eone(recordAction.enemy, hurt);
                }
                BeUsed();
            }

            else if (recordAction.element == "盾")
            {
                isused = true;
                if (recordAction.type == 1)
                {
                    cardEffect.RemoveDefense_Eone(recordAction.enemy, recordAction.number);
                }
                else if (recordAction.type == 2)
                {
                    cardEffect.AddDefense_Eone(recordAction.enemy, recordAction.number);
                }
                BeUsed();
            }
        }

        else if (recordAction.target == "自己")
        {
            if (recordAction.element == "生命")
            {
                isused = true;
                if (recordAction.type == 1)
                {
                    cardEffect.TakeDamage_P(recordAction.number);
                }
                else if (recordAction.type == 2)
                {
                    cardEffect.Heal_P(recordAction.number);
                }
                BeUsed();
            }

            else if (recordAction.element == "盾")
            {
                isused = true;
                if (recordAction.type == 1)
                {
                    cardEffect.RemoveDefense_P(recordAction.number);
                }
                else if (recordAction.type == 2)
                {
                    cardEffect.AddDefense_P(recordAction.number);
                }
                BeUsed();
            }
        }

        else if (recordAction.target == "所有敌人")
        {
            if (recordAction.element == "生命")
            {
                isused = true;
                if (recordAction.type == 1)
                {
                    cardEffect.TakeDamage_Eall(recordAction.number);
                }
                else if (recordAction.type == 2)
                {
                    cardEffect.Heal_Eall(recordAction.number);
                }
                else if (recordAction.type == 3)
                {
                    cardEffect.TakeDamage_Eall(recordAction.number);
                    int all = recordAction.number * cardEffect.enemyCount;
                    cardEffect.Heal_P(all);
                }
                else if (recordAction.type == 4)
                {
                    cardEffect.Reduceblood_P(recordAction.number);
                    int hurt = recordAction.number * 2;
                    cardEffect.TakeDamage_Eall(hurt);
                }
                BeUsed();
            }

            else if (recordAction.element == "盾")
            {
                isused = true;
                if (recordAction.type == 1)
                {
                    cardEffect.RemoveDefense_Eall(recordAction.number);
                }
                else if (recordAction.type == 2)
                {
                    cardEffect.AddDefense_Eall(recordAction.number);
                }
                BeUsed();
            }
        }

        else if (recordAction.target == "所有人")
        {
            if (recordAction.element == "生命")
            {
                isused = true;
                if (recordAction.type == 1)
                {
                    cardEffect.TakeDamage_All(recordAction.number);
                }
                else if (recordAction.type == 2)
                {
                    cardEffect.Heal_All(recordAction.number);
                }
                BeUsed();
            }

            else if (recordAction.element == "盾")
            {
                isused = true;
                if (recordAction.type == 1)
                {
                    cardEffect.RemoveDefense_All(recordAction.number);
                }
                else if (recordAction.type == 2)
                {
                    cardEffect.AddDefense_All(recordAction.number);
                }
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
