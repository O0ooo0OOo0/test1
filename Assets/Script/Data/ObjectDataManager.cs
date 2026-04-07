using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ObjectDataManager : MonoBehaviour
{
    public List<int> props;  
    public List<int> talismans;  
    public List<int> materials;   
    public List<int> items;  
    public List<int> targetCards;   
    public List<int> numberCards;  
    public List<int> elementCards;  
    public int coins;   

    private void Start()
    {
        GetObjCount();
    }

    /// <summary>
    /// 开始游戏时，获取物品数据信息
    /// </summary>

    // 获取数组长度并定义
    public void GetObjCount()
    {
        props = new List<int>(new int[ArchiveGameManager.arcm.propsCount]);
        talismans = new List<int>(new int[ArchiveGameManager.arcm.talismansCount]);
        materials = new List<int>(new int[ArchiveGameManager.arcm.materialsCount]);
        items = new List<int>(new int[ArchiveGameManager.arcm.itemsCount]);
        targetCards = new List<int>(new int[ArchiveGameManager.arcm.tarcardsCount]);
        numberCards = new List<int>(new int[ArchiveGameManager.arcm.numcardsCount]);
        elementCards = new List<int>(new int[ArchiveGameManager.arcm.elecardsCount]);
    }

    // 获取数组中物品数量
    //public void GetObjAmount()
    //{
    //    for (int i = 0; i < props.Count; i++)   // 道具
    //    {
    //        props[i] = ArchiveGameManager.arcm.arcsInf[odIndex].props[i].propAmount;
    //    }
    //    for (int i = 0; i < talismans.Count; i++)   // 护符
    //    {
    //        talismans[i] = ArchiveGameManager.arcm.arcsInf[odIndex].talismans[i].talismanAmount;
    //    }
    //    for (int i = 0; i < materials.Count; i++)   // 材料
    //    {
    //        materials[i] = ArchiveGameManager.arcm.arcsInf[odIndex].materials[i].materialAmount;
    //    }
    //    for (int i = 0; i < items.Count; i++)   // 物品
    //    {
    //        items[i] = ArchiveGameManager.arcm.arcsInf[odIndex].items[i].itemAmount;
    //    }
    //    for (int i = 0; i < targetCards.Count; i++)   // 目标卡
    //    {
    //        targetCards[i] = ArchiveGameManager.arcm.arcsInf[odIndex].tarCards[i].tarCardAmount;
    //    }
    //    for (int i = 0; i < numberCards.Count; i++)   // 数值卡
    //    {
    //        numberCards[i] = ArchiveGameManager.arcm.arcsInf[odIndex].numCards[i].numCardAmount;
    //    }
    //    for (int i = 0; i < elementCards.Count; i++)   // 元素卡
    //    {
    //        elementCards[i] = ArchiveGameManager.arcm.arcsInf[odIndex].eleCards[i].eleCardAmount;
    //    }
    //    coins = ArchiveGameManager.arcm.arcsInf[odIndex].coins;   // 铜币
    //}

    /// <summary>
    /// 开始新游戏时，随机化初始值
    /// </summary>

    // 赋值
    public void DefineNewAmount()
    {
        //道具类初始值
        InitializeProp();

        //护符类初始值
        InitializeTalisman();

        //锻造材料初始值
        InitializeMaterial();

        //  物品（非战斗）初始值
        InitializeItem();

        //目标牌初始值
        targetCards[0] = 1;  //无穷
        targetCards[1] = 2;
        targetCards[2] = 3;
        targetCards[3] = 2;
        targetCards[4] = 3;

        //数字牌初始值
        numberCards[0] = 1;  //无穷
        numberCards[1] = 2;
        numberCards[2] = 1;
        numberCards[3] = 1;  //无穷
        numberCards[4] = 3;
        numberCards[5] = 2;
        numberCards[6] = 2;
        numberCards[7] = 1;
        numberCards[8] = 1;

        //元素牌初始值
        elementCards[0] = 3;
        elementCards[1] = 3;
        elementCards[2] = 3;
        elementCards[3] = 3;
        elementCards[4] = 3;

        //铜钱初始值
        coins = 15;
    }

    // 初始化道具数量
    public void InitializeProp()
    {
        int randomIndex = Random.Range(0, props.Count);
        for (int i = 0; i < props.Count; i++)
        {
            if (i == randomIndex)
            {
                props[i] = 1;
            }
            else
            {
                props[i] = 0;
            }
        }
    }

    // 初始化护符数量
    public void InitializeTalisman()
    {
        int randomIndex = Random.Range(0, talismans.Count);
        for (int i = 0; i < talismans.Count; i++)
        {
            if (i == randomIndex)
            {
                talismans[i] = 1;
            }
            else
            {
                talismans[i] = 0;
            }
        }
    }

    // 初始化锻造材料数量
    public void InitializeMaterial()
    {
        int[] randomIndex = new int[materials.Count]; 

        for (int i = 0; i < randomIndex.Length; i++)
        {
            randomIndex[i] = Random.Range(2, 6); 
        }

        for (int i = 0; i < materials.Count; i++)
        {
            materials[i] = randomIndex[i];
        }
    }

    // 初始化物品（非战斗）数量
    public void InitializeItem()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i] = 0;
        }
    }

    /// <summary>
    /// 更新存档系统中的物品数组
    /// </summary>

    // 传回物品数量至存档系统
    public void GiveObjAmount(int id)
    {
        var arc = ArchiveGameManager.arcm.arcsInf[id];

        for (int i = 0; i < props.Count; i++)
        {
            var prop = arc.props[i];
            prop.propAmount = props[i];
            arc.props[i] = prop;
        }
        for (int i = 0; i < talismans.Count; i++)
        {
            var talisman = arc.talismans[i];
            talisman.talismanAmount = talismans[i];
            arc.talismans[i] = talisman;
        }
        for (int i = 0; i < materials.Count; i++)
        {
            var material = arc.materials[i];
            material.materialAmount = materials[i];
            arc.materials[i] = material;
        }
        for (int i = 0; i < items.Count; i++)
        {
            var item = arc.items[i];
            item.itemAmount = items[i];
            arc.items[i] = item;
        }
        for (int i = 0; i < targetCards.Count; i++)
        {
            var tarcard = arc.tarCards[i];
            tarcard.tarCardAmount = targetCards[i];
            arc.tarCards[i] = tarcard;
        }
        for (int i = 0; i < numberCards.Count; i++)
        {
            var numcard = arc.numCards[i];
            numcard.numCardAmount = numberCards[i];
            arc.numCards[i] = numcard;
        }
        for (int i = 0; i < elementCards.Count; i++)
        {
            var elecard = arc.eleCards[i];
            elecard.eleCardAmount = elementCards[i];
            arc.eleCards[i] = elecard;
        }
        arc.coins = coins;

        ArchiveGameManager.arcm.arcsInf[id] = arc;
    }
}