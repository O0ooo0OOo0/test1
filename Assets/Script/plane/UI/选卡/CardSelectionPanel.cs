// CardSelectionPanel.cs
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class CardSelectionPanel : MonoBehaviour
{
    [Header("UI组件")]
    public GameObject cardButtonPrefab;      // 卡牌按钮预制体
    public Transform cardLibraryContent;      // 卡牌库区域
    public Transform slotContent;             // 槽位区域

    [Header("卡牌配置")]
    public List<CardData> allCards;           // 所有卡牌配置
    public List<bool> unlockedCards;          // 对应的解锁状态

    [Header("槽位配置")]
    public int maxSlots = 2;
    public List<Image> slotIcons;             // 槽位图标显示
    public List<TextMeshProUGUI> slotKeyTexts; // Z X C V文字

    private EquipmentData equipment;
    private AbilityManage abilityManage;
    private bool isPanelOpen = false;

    void Awake()
    {
        LoadEquipment();  // ← 移到Awake，保证一定会执行
    }
    void Start()
    {
        // 通过 Tag 查找玩家身上的 AbilityManage
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            abilityManage = player.GetComponent<AbilityManage>();
        }
        

        CreateSlotUI();
        RefreshCardLibrary();
        gameObject.SetActive(false);
    }

    void Update()
    {
 
    }

    public void OpenPanel()
    {
        isPanelOpen = true;
        gameObject.SetActive(true);
        RefreshCardLibrary();
        RefreshSlotUI();
    }

    public void ClosePanel()
    {
        ApplyEquipmentToGame();
        SaveEquipment();

        isPanelOpen = false;
        gameObject.SetActive(false);
    }

    void CreateSlotUI()
    {
        KeyCode[] keys = { KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V };
        for (int i = 0; i < maxSlots; i++)
        {
            if (slotKeyTexts.Count > i && slotKeyTexts[i] != null)
                slotKeyTexts[i].text = keys[i].ToString();
        }

        // 补齐空槽位
        while (equipment.equippedElementCardIds.Count < maxSlots)
            equipment.equippedElementCardIds.Add("");
    }

    void RefreshCardLibrary()
    {
        // 清除现有按钮
        foreach (Transform child in cardLibraryContent)
            Destroy(child.gameObject);

        // 创建卡牌按钮
        for (int i = 0; i < allCards.Count; i++)
        {
            if (!unlockedCards[i]) continue;

            CardData card = allCards[i];
            bool isEquipped = IsCardEquipped(card.cardId);

            GameObject btnObj = Instantiate(cardButtonPrefab, cardLibraryContent);
            CardButton btn = btnObj.GetComponent<CardButton>();
            btn.Init(card, isEquipped, this);
        }
    }

    bool IsCardEquipped(string cardId)
    {
        if (equipment.equippedElementCardIds.Contains(cardId))
            return true;
        if (equipment.equippedOtherCardIds.Contains(cardId))
            return true;
        return false;
    }

    void RefreshSlotUI()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            string cardId = equipment.equippedElementCardIds[i];
            Sprite icon = GetCardIcon(cardId);
            slotIcons[i].sprite = icon;
            slotIcons[i].color = icon != null ? Color.white : new Color(0.2f, 0.2f, 0.2f, 0.5f);
        }
    }

    Sprite GetCardIcon(string cardId)
    {
        CardData card = allCards.Find(c => c.cardId == cardId);
        return card != null ? card.icon : null;
    }

    // 点击卡牌时调用
    public void OnCardClicked(CardData card, bool isCurrentlyEquipped)
    {
        if (isCurrentlyEquipped)
        {
            // 卸下卡牌
            UnequipCard(card.cardId);
        }
        else
        {
            // 装备卡牌
            TryEquipCard(card);
        }

        RefreshCardLibrary();
        RefreshSlotUI();
    }

    void TryEquipCard(CardData card)
    {
        if (card.cardType == CardType.Element)
        {
            // 找空槽位
            int emptySlot = equipment.equippedElementCardIds.FindIndex(id => string.IsNullOrEmpty(id));
            if (emptySlot >= 0)
            {
                equipment.equippedElementCardIds[emptySlot] = card.cardId;
            }
            else
            {
                Debug.Log("槽位已满，无法装备");
            }
        }
        else
        {
            // 其他卡直接添加
            if (!equipment.equippedOtherCardIds.Contains(card.cardId))
                equipment.equippedOtherCardIds.Add(card.cardId);
        }
    }

    void UnequipCard(string cardId)
    {
        // 从元素槽移除
        int index = equipment.equippedElementCardIds.FindIndex(id => id == cardId);
        if (index >= 0)
            equipment.equippedElementCardIds[index] = "";

        // 从其他卡移除
        equipment.equippedOtherCardIds.Remove(cardId);
    }

    void ApplyEquipmentToGame()
    {
        // 重置所有能力
        abilityManage.hasDoubleJump = false;
        abilityManage.hasDash = false;
        abilityManage.hasGlide = false;
        abilityManage.hasIceAbility = false;
        abilityManage.hasFireAbility = false;

        // 启用装备的能力
        foreach (string id in equipment.equippedElementCardIds)
            EnableAbility(id);

        foreach (string id in equipment.equippedOtherCardIds)
            EnableAbility(id);

        // 更新组件启用状态
        UpdateAbilityComponents();
    }

    void EnableAbility(string cardId)
    {
        switch (cardId)
        {
            case "DoubleJump": abilityManage.hasDoubleJump = true; break;
            case "Dash": abilityManage.hasDash = true; break;
            case "Glide": abilityManage.hasGlide = true; break;
            case "Ice": abilityManage.hasIceAbility = true; break;
            case "Fire": abilityManage.hasFireAbility = true; break;
        }
    }

    void UpdateAbilityComponents()
    {
        var doubleJump = GetComponent<DoubleJumpAbility>();
        if (doubleJump) doubleJump.enabled = abilityManage.hasDoubleJump;

        var dash = GetComponent<DashAbility>();
        if (dash) dash.enabled = abilityManage.hasDash;

        var ice = GetComponent<IceAbility>();
        if (ice) ice.enabled = abilityManage.hasIceAbility;
    }

    void LoadEquipment()
    {
        equipment = new EquipmentData();
        equipment.maxSlots = maxSlots;
        equipment.equippedElementCardIds = new List<string>();
        equipment.equippedOtherCardIds = new List<string>();

        for (int i = 0; i < maxSlots; i++)
            equipment.equippedElementCardIds.Add("");
    }

    void SaveEquipment()
    {
        // 可扩展PlayerPrefs保存
        Debug.Log("装备已保存");
    }
}