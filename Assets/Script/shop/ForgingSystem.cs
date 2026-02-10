using UnityEngine;
using System.Collections.Generic;
using static ForgingSystem;

public class ForgingSystem : MonoBehaviour
{
    // 定义元素属性枚举
    public enum ElementType { Cold, Hot, Shield, Spirit }

    // 定义稀有度枚举
    public enum Rarity { Rare1, Rare2, Rare3, Rare4, Rare5 }

    // 材料类
    [System.Serializable]
    public class Material
    {
        public ElementType element;
        public Rarity rarity;
    }

    // 道具类
    [System.Serializable]
    public class Prop
    {
        public string name;
        public bool isCommon; // 普通或稀有
    }

    // 护符类
    [System.Serializable]
    public class Amulet
    {
        public string name;
        public string effect;
    }

    // 卡牌类
    [System.Serializable]
    public class Card
    {
        public int value; // 数值卡
        public string target; // 目标卡
        public ElementType element; // 元素卡
    }

    // 玩家背包
    public List<Material> playerMaterials = new List<Material>();
    public List<Prop> playerProps = new List<Prop>();
    public List<Amulet> playerAmulets = new List<Amulet>();
    public List<Card> playerCards = new List<Card>();
    public int playerCoins = 0; // 玩家拥有的铜钱

    // 锻造概率表
    private float[,] forgingProbabilities = new float[,]
    {
        { 0.1f, 0.5f, 0.05f }, // 1 铜钱：道具 10%，卡牌 50%，护符 5%
        { 0.15f, 0.6f, 0.07f }, // 2 铜钱：道具 15%，卡牌 60%，护符 7%
        { 0.2f, 0.7f, 0.09f }  // 3 铜钱：道具 20%，卡牌 70%，护符 9%
    };

    // 锻造系统预设道具、护符、卡牌库
    public List<Prop> propPool = new List<Prop>();
    public List<Amulet> amuletPool = new List<Amulet>();
    public List<Card> cardPool = new List<Card>();

    // 锻造函数
    public void Forge(int copperCoins, Material material = null)
    {
        // 检查铜钱投入是否有效
        if (copperCoins < 1 || copperCoins > 3)
        {
            Debug.Log("请投入 1-3 个铜钱");
            return;
        }

        // 检查是否有材料投入（可选）
        if (material != null)
        {
            // 从玩家材料中移除投入的材料
            RemoveMaterial(material);
        }

        // 根据投入铜钱数量获取当前锻造概率
        float propProbability = forgingProbabilities[copperCoins - 1, 0];
        float cardProbability = forgingProbabilities[copperCoins - 1, 1];
        float amuletProbability = forgingProbabilities[copperCoins - 1, 2];

        // 计算锻造次数
        int returnQuantity = CalculateReturnQuantity(material);

        // 循环生成返还物品
        for (int i = 0; i < returnQuantity; i++)
        {
            // 随机决定返还物品类型
            float randomValue = Random.value;
            ItemResult result;

            if (randomValue < propProbability)
            {
                // 返还道具
                Prop prop = GetRandomProp(material);
                result = new PropResult { prop = prop };
            }
            else if (randomValue < propProbability + cardProbability)
            {
                // 返还卡牌
                Card card = GetRandomCard(material);
                result = new CardResult { card = card };
            }
            else
            {
                // 返还护符
                Amulet amulet = GetRandomAmulet();
                result = new AmuletResult { amulet = amulet };
            }

            // 根据返还物品类型添加到玩家背包
            if (result is PropResult propResult)
            {
                playerProps.Add(propResult.prop);
            }
            else if (result is CardResult cardResult)
            {
                playerCards.Add(cardResult.card);
            }
            else if (result is AmuletResult amuletResult)
            {
                playerAmulets.Add(amuletResult.amulet);
            }

            Debug.Log("返还物品：" + result.ToString());
        }

        // 返回投入的铜钱（保底机制）
        playerCoins -= copperCoins;
    }

    // 计算锻造次数
    private int CalculateReturnQuantity(Material material = null)
    {
        if (material != null)
        {
            // 返还数量 = 材料稀有度 / 2 取整
            int rarityValue = (int)material.rarity;
            return rarityValue / 2;
        }
        else
        {
            // 如果没投材料默认为 1
            return 1;
        }
    }

    // 获取随机道具（可受材料元素属性影响）
    private Prop GetRandomProp(Material material = null)
    {
        // 如果有材料投入且有元素属性，提高对应元素道具概率
        if (material != null)
        {
            // 这里可以根据材料元素属性调整道具概率，暂时没做
            // 示例：提高对应元素属性道具的获取概率
        }

        // 随机选择普通或稀有道具
        bool isCommon = Random.value < 0.7f; // 普通道具概率 70%，稀有 30%


        // 随机从道具池中选择道具
        List<Prop> filteredProps = new List<Prop>();
        foreach (Prop prop in propPool)
        {
            if (prop.isCommon == isCommon)
            {
                filteredProps.Add(prop);
            }
        }

        return filteredProps[Random.Range(0, filteredProps.Count)];
    }

    // 获取随机卡牌（可受材料元素属性影响）
    private Card GetRandomCard(Material material = null)
    {
        // 卡牌类型概率：数值卡 40%，目标卡 30%，元素卡 30%
        float randomCardType = Random.value;

        if (randomCardType < 0.4f)
        {
            return GetNumericCard(); // 数值卡 40%
        }
        else if (randomCardType < 0.7f)
        {
            return GetTargetCard(); // 目标卡 30%
        }
        else
        {
            // 元素卡逻辑
            if (material != null)
            {
                // 提高对应元素卡牌概率示例（55% 对应元素，其他各 15%）
                float elementRandom = Random.value;
                if (elementRandom < 0.55f)
                {
                    // 返回对应元素卡牌
                    return GetElementCard(material.element);
                }
                else
                {
                    // 随机选择其他三种元素之一
                    ElementType[] otherElements = new ElementType[3];
                    int index = 0;
                    foreach (ElementType type in System.Enum.GetValues(typeof(ElementType)))
                    {
                        if (type != material.element)
                        {
                            otherElements[index++] = type;
                        }
                    }
                    return GetElementCard(otherElements[Random.Range(0, 3)]);
                }
            }
            else
            {
                return GetElementCard(); // 没有材料投入，随机获取元素卡
            }
        }
    }

    // 获取随机数值卡
    private Card GetNumericCard()
    {
        Card card = new Card();
        card.value = Random.Range(0, 10); // 0-9 随机整数
        return card;
    }

    // 获取随机目标卡
    private Card GetTargetCard()
    {
        Card card = new Card();
        card.target = "aim_" + Random.Range(0, 7); // aim_0 到 aim_6
        return card;
    }

    // 获取随机元素卡（可指定元素）
    private Card GetElementCard(ElementType? element = null)
    {
        Card card = new Card();
        if (element.HasValue)
        {
            card.element = element.Value;
        }
        else
        {
            card.element = (ElementType)Random.Range(0, 4); // E0 到 E3
        }
        return card;
    }

    // 获取随机护符
    private Amulet GetRandomAmulet()
    {
        return amuletPool[Random.Range(0, amuletPool.Count)];
    }

    // 从玩家材料中移除投入的材料
    private void RemoveMaterial(Material materialToRemove)
    {
        for (int i = 0; i < playerMaterials.Count; i++)
        {
            if (playerMaterials[i].element == materialToRemove.element && playerMaterials[i].rarity == materialToRemove.rarity)
            {
                playerMaterials.RemoveAt(i);
                break;
            }
        }
    }
}

// 物品结果基类（用于区分返回类型）
public abstract class ItemResult { }
public class PropResult : ItemResult
{
    public Prop prop;
    public override string ToString() => $"道具：{prop.name}";
}
public class AmuletResult : ItemResult
{
    public Amulet amulet;
    public override string ToString() => $"护符：{amulet.name} ({amulet.effect})";
}
public class CardResult : ItemResult
{
    public Card card;
    public override string ToString() => $"卡牌：{(card.value != 0 ? $"数值 {card.value}" : (card.target != null ? $"目标 {card.target}" : $"元素 {card.element}"))}";
}