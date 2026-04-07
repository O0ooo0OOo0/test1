using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public static InputManager im;
    public bool isAllDisable;

    // 键位输入相关脚本
    public EscManager escManager;
    public MapManager mapManager;
    public ItemManager itemManager;
    public CardsBagManager cardsManager;
    public BookManager bookManager;
    public ForgingManager forgingManager;

    private void Awake()
    {
        if (im == null)
        {
            im = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        CheckKeysUseful();
    }

    // 检查相关键位是否可用
    public void CheckKeysUseful()
    {
        if (SceneManager.GetActiveScene().name == "main")
        {
            DisableKeysInput();
            isAllDisable = true;
        }
        else if (SceneManager.GetActiveScene().name != "main" && isAllDisable == true)
        {
            EnableKeysInput();
            isAllDisable = false;
        }
    }

    // 允许全部按键输入
    public void EnableKeysInput()
    {
        escManager.isCanEsc = true;
        mapManager.isCanMap = true;
        itemManager.isCanItem = true;
        cardsManager.isCanCardsBag = true;
        bookManager.isCanBook = true;
        forgingManager.isCanForging = true;
    }

    // 禁用全部按键输入
    public void DisableKeysInput()
    {
        escManager.isCanEsc = false;
        mapManager.isCanMap = false;
        itemManager.isCanItem = false;
        cardsManager.isCanCardsBag = false;
        bookManager.isCanBook = false;
        forgingManager.isCanForging = false;
    }

    /// <summary>
    /// 方法一：逐帧检测
    /// </summary>

    // 使用某键位后，禁止其他按键输入
    public void DisableOtherKeysInput()
    {
        if (escManager.isOpenEsc)   // esc已启用
        {
            escManager.isOpenEsc = true;
            mapManager.isCanMap = false;
            itemManager.isCanItem = false;
            cardsManager.isCanCardsBag = false;
            bookManager.isCanBook = false;
            forgingManager.isCanForging = false;
        }
        else if (mapManager.isOpenMap)   // 地图已启用
        {
            mapManager.isCanMap = true;
            escManager.isCanEsc = false;
            itemManager.isCanItem = false;
            cardsManager.isCanCardsBag = false;
            bookManager.isCanBook = false;
            forgingManager.isCanForging = false;
        }
        else if (itemManager.isOpenItem)   // 物品栏已启用
        {
            itemManager.isCanItem = true;
            escManager.isCanEsc = false;
            mapManager.isCanMap = false;
            cardsManager.isCanCardsBag = false;
            bookManager.isCanBook = false;
            forgingManager.isCanForging = false;
        }
        else if (cardsManager.isOpenCardsBag)   // 卡牌库已启用
        {
            cardsManager.isCanCardsBag = true;
            escManager.isCanEsc = false;
            mapManager.isCanMap = false;
            itemManager.isCanItem = false;
            bookManager.isCanBook = false;
            forgingManager.isCanForging = false;
        }
        else if (bookManager.isOpenBook)   // 图鉴已启用
        {
            bookManager.isCanBook = true;
            escManager.isCanEsc = false;
            mapManager.isCanMap = false;
            itemManager.isCanItem = false;
            cardsManager.isCanCardsBag = false;
            forgingManager.isCanForging = false;
        }
        else if (forgingManager.isOpenForging)   // 锻造已启用
        {
            forgingManager.isCanForging = true;
            escManager.isCanEsc = false;
            mapManager.isCanMap = false;
            itemManager.isCanItem = false;
            cardsManager.isCanCardsBag = false;
            bookManager.isCanBook = false;
        }
        else
        {
            EnableKeysInput();
        }
    }

    /// <summary>
    /// 方法二：每个脚本中分别调用
    /// </summary>

    // Esc
    public void EnableEsc()
    {
        escManager.isOpenEsc = true;
        mapManager.isCanMap = false;
        itemManager.isCanItem = false;
        cardsManager.isCanCardsBag = false;
        bookManager.isCanBook = false;
        forgingManager.isCanForging = false;
    }

    // 地图
    public void EnableMap()
    {
        mapManager.isCanMap = true;
        escManager.isCanEsc = false;
        itemManager.isCanItem = false;
        cardsManager.isCanCardsBag = false;
        bookManager.isCanBook = false;
        forgingManager.isCanForging = false;
    }

    // 物品栏
    public void EnableItem()
    {
        itemManager.isCanItem = true;
        escManager.isCanEsc = false;
        mapManager.isCanMap = false;
        cardsManager.isCanCardsBag = false;
        bookManager.isCanBook = false;
        forgingManager.isCanForging = false;
    }

    // 卡牌库
    public void EnableCardsBag()
    {
        cardsManager.isCanCardsBag = true;
        escManager.isCanEsc = false;
        mapManager.isCanMap = false;
        itemManager.isCanItem = false;
        bookManager.isCanBook = false;
        forgingManager.isCanForging = false;
    }

    // 图鉴
    public void EnableBook()
    {
        bookManager.isCanBook = true;
        escManager.isCanEsc = false;
        mapManager.isCanMap = false;
        itemManager.isCanItem = false;
        cardsManager.isCanCardsBag = false;
        forgingManager.isCanForging = false;
    }

    // 锻造
    public void EnableForging()
    {
        forgingManager.isCanForging = true;
        escManager.isCanEsc = false;
        mapManager.isCanMap = false;
        itemManager.isCanItem = false;
        cardsManager.isCanCardsBag = false;
        bookManager.isCanBook = false;
    }
}
