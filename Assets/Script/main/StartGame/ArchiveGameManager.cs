using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro;
using System;

public class ArchiveGameManager : MonoBehaviour
{
    public static ArchiveGameManager arcm;

    // 存档储存的全部全局信息
    [System.Serializable]
    public struct ArchivesAllInformation
    {
        public int arcIndex;
        public bool isNewGame;   // 判断是否为空存档

        // 选择存档界面所需信息
        public Sprite arcImage;   // 存档的背景图片
        public string arcName;   // 存档名称
        public string arcTime;   // 存档创建时间

        // 玩家信息
        public int sceneIndex;   // 场景序列号

        // 地图信息
        public List<MapInformation> maps;

        // 图鉴信息
        public int unlockedCardsAmount, unlockedBiologyAmount;   // 已解锁的卡牌数及生物数
        public List<BookCardsInformation> cards;
        public List<BookBiologyInformation> biology;

        // 锻造NPC信息
        public bool isAskedNpc;

        // 物品
        public List<PropsInf> props;
        public List<TalismansInf> talismans;
        public List<MaterialsInf> materials;
        public List<ItemsInf> items;
        public List<TargetCardsInf> tarCards;
        public List<NumberCardsInf> numCards;
        public List<ElementCardsInf> eleCards;
        public int coins;   // 铜币
    }

    // 地图信息：地图是否解锁、地图污染/净化状态
    [System.Serializable]
    public struct MapInformation 
    {
        public int mapId;   // 地图id
        public bool isMapUnclock;
        public int mapStatus;
    }

    // 图鉴信息：卡牌图鉴（卡牌是否获取）、生物图鉴（生物是否收集）
    [System.Serializable]
    public struct BookCardsInformation
    {
        public int bookCardId;
        public bool isCardObtain;
    }
    [System.Serializable]
    public struct BookBiologyInformation
    {
        public int bookBiologyId;
        public bool isBiologyGet;
    }

    // 物品信息
    [System.Serializable]
    public struct PropsInf   // 道具
    {
        public int propId;
        public int propAmount;
    }
    [System.Serializable]
    public struct TalismansInf   // 护符
    {
        public int talismanId;
        public int talismanAmount;
    }
    [System.Serializable]
    public struct MaterialsInf   // 锻造材料
    { 
        public int materialId;
        public int materialAmount;
    }
    [System.Serializable]
    public struct ItemsInf   // 物品（非战斗场景使用）
    {
        public int itemId;
        public int itemAmount;
    }
    [System.Serializable]
    public struct TargetCardsInf   // 目标卡
    {
        public int tarCardId;
        public int tarCardAmount;
    }
    [System.Serializable]
    public struct NumberCardsInf   // 数值卡
    {
        public int numCardId;
        public int numCardAmount;
    }
    [System.Serializable]
    public struct ElementCardsInf   // 元素卡
    {
        public int eleCardId;
        public int eleCardAmount;
    }

    // 存档数据总类数
    public int mapsCount;
    public int cardsCount;
    public int biologyCount;
    public int propsCount;
    public int talismansCount;
    public int materialsCount;
    public int itemsCount;
    public int tarcardsCount;
    public int numcardsCount;
    public int elecardsCount;

    public List<ArchivesAllInformation> arcsInf;   // 存档
    public int currentArcIndex;   // 运行的存档序列号

    private void Awake()
    {
        if (arcm == null)
        {
            arcm = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeAllArchives();
    }

    // 初始化全部存档内容
    public void InitializeAllArchives()
    {
        for (int i = 0; i < arcsInf.Count; i++)
        {
            IniArcInfIndex(i);
            InitializeArchive(i);
        }
    }

    // 初始化存档的各个内容的数量及序列号id（只定义一次，后续不修改）
    public void IniArcInfIndex(int id)
    {
        var arc = arcsInf[id];

        // 地图id：总共15个小地图，序号0-14
        while (arc.maps.Count < mapsCount)
        {
            arc.maps.Add(new MapInformation());
        }
        for (int i = 0; i < arc.maps.Count; i++)
        {
            var map = arc.maps[i];
            map.mapId = i;
            arc.maps[i] = map;
        }

        // 卡牌图鉴id：
        while (arc.cards.Count < cardsCount)
        {
            arc.cards.Add(new BookCardsInformation());
        }
        for (int i = 0; i < arc.cards.Count; i++)
        {
            var card = arc.cards[i];
            card.bookCardId = i;
            arc.cards[i] = card;
        }

        // 生物图鉴id：
        while (arc.biology.Count < biologyCount)
        {
            arc.biology.Add(new BookBiologyInformation());
        }
        for (int i = 0; i < arc.biology.Count; i++)
        {
            var biology = arc.biology[i];
            biology.bookBiologyId = i;
            arc.biology[i] = biology;
        }

        // 道具id：
        while (arc.props.Count < propsCount)
        {
            arc.props.Add(new PropsInf());
        }
        for (int i = 0; i < arc.props.Count; i++)
        {
            var prop = arc.props[i];
            prop.propId = i;
            arc.props[i] = prop;
        }

        // 护符id：
        while (arc.talismans.Count < talismansCount)
        {
            arc.talismans.Add(new TalismansInf());
        }
        for (int i = 0; i < arc.talismans.Count; i++)
        {
            var talisman = arc.talismans[i];
            talisman.talismanId = i;
            arc.talismans[i] = talisman;
        }

        // 锻造材料id：
        while (arc.materials.Count < materialsCount)
        {
            arc.materials.Add(new MaterialsInf());
        }
        for (int i = 0; i < arc.materials.Count; i++)
        {
            var material = arc.materials[i];
            material.materialId = i;
            arc.materials[i] = material;
        }

        // 物品id：
        while (arc.items.Count < itemsCount)
        {
            arc.items.Add(new ItemsInf());
        }
        for (int i = 0; i < arc.items.Count; i++)
        {
            var item = arc.items[i];
            item.itemId = i;
            arc.items[i] = item;
        }

        // 目标卡id：
        while (arc.tarCards.Count < tarcardsCount)
        {
            arc.tarCards.Add(new TargetCardsInf());
        }
        for (int i = 0; i < arc.tarCards.Count; i++)
        {
            var tarcard = arc.tarCards[i];
            tarcard.tarCardId = i;
            arc.tarCards[i] = tarcard;
        }

        // 数值卡id：
        while (arc.numCards.Count < numcardsCount)
        {
            arc.numCards.Add(new NumberCardsInf());
        }
        for (int i = 0; i < arc.numCards.Count; i++)
        {
            var numcard = arc.numCards[i];
            numcard.numCardId = i;
            arc.numCards[i] = numcard;
        }

        // 元素卡id：
        while (arc.eleCards.Count < elecardsCount)
        {
            arc.eleCards.Add(new ElementCardsInf());
        }
        for (int i = 0; i < arc.eleCards.Count; i++)
        {
            var elecard = arc.eleCards[i];
            elecard.eleCardId = i;
            arc.eleCards[i] = elecard;
        }

        arcsInf[id] = arc;
    }

    // 初始化/重置存档信息(单个)
    public void InitializeArchive(int id)
    {
        var arc = arcsInf[id];

        arc.isNewGame = true;

        // 选择存档界面信息
        arc.arcImage = null;
        arc.arcName = null;
        arc.arcTime = null;

        // 玩家信息
        arc.sceneIndex = 0;

        // 地图信息
        for (int i = 0; i < arc.maps.Count; i++)
        {
            var map = arc.maps[i];
            map.isMapUnclock = false;
            map.mapStatus = 0;
            arc.maps[i] = map;
        }

        // 图鉴信息
        arc.unlockedCardsAmount = 0;
        arc.unlockedBiologyAmount = 0;
        for (int i = 0; i < arc.cards.Count; i++)
        {
            var card = arc.cards[i];
            card.isCardObtain = false;
            arc.cards[i] = card;
        }
        for (int i = 0; i < arc.biology.Count; i++)
        {
            var biology = arc.biology[i];
            biology.isBiologyGet = false;
            arc.biology[i] = biology;
        }

        // 锻造npc信息
        arc.isAskedNpc = false;

        // 物品信息
        for (int i = 0; i < arc.props.Count; i++)
        {
            var prop = arc.props[i];
            prop.propAmount = 0;
            arc.props[i] = prop;
        }
        for (int i = 0; i < arc.talismans.Count; i++)
        {
            var talisman = arc.talismans[i];
            talisman.talismanAmount = 0;
            arc.talismans[i] = talisman;
        }
        for (int i = 0; i < arc.materials.Count; i++)
        {
            var material = arc.materials[i];
            material.materialAmount = 0;
            arc.materials[i] = material;
        }
        for (int i = 0; i < arc.items.Count; i++)
        {
            var item = arc.items[i];
            item.itemAmount = 0;
            arc.items[i] = item;
        }
        for (int i = 0; i < arc.tarCards.Count; i++)
        {
            var tarCard = arc.tarCards[i];
            tarCard.tarCardAmount = 0;
            arc.tarCards[i] = tarCard;
        }
        for (int i = 0; i < arc.numCards.Count; i++)
        {
            var numCard = arc.numCards[i];
            numCard.numCardAmount = 0;
            arc.numCards[i] = numCard;
        }
        for (int i = 0; i < arc.eleCards.Count; i++)
        {
            var eleCard = arc.eleCards[i];
            eleCard.eleCardAmount = 0;
            arc.eleCards[i] = eleCard;
        }
        arc.coins = 0;

        arcsInf[id] = arc;
    }

    // 删除存档内容
    public void DeleteArcInf(int id)
    {
        InitializeArchive(id);
    }

    // 复制存档内容*1
    public void CopyArcInf(int id)
    {
        // 更新复制存档的创建时间
        DateTime now = DateTime.Now;
        string time = now.Year.ToString() + "-" + now.Month.ToString() + "-" + now.Day.ToString() + "-" + now.Hour.ToString() + ":" + now.Minute.ToString();

        // 找到当前第一个新存档，并复制存档信息
        for (int i = 0; i < arcsInf.Count; i++)
        {
            if (arcsInf[i].isNewGame == true)
            {
                var arc = arcsInf[id];
                arc.arcIndex = arcsInf[i].arcIndex;   // 恢复自身序列号
                arc.arcTime = time;
                arcsInf[i] = arc;
                return;
            }
        }
    }

    // 更新存档信息
    public void UpdateArcsInf()
    {
        var arc = arcsInf[currentArcIndex];   // 建立局部变量

        // 获取当前所在场景序列号
        arc.sceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 根据场景序列号取得场景地图名称+场景图片
        SceneNameManager.snm.GetSceneMapName(arc.sceneIndex);
        arc.arcImage = SceneNameManager.snm.sceneImage;
        arc.arcName = SceneNameManager.snm.sceneName;

        arcsInf[currentArcIndex] = arc;   // 返还变量值
    }
}
