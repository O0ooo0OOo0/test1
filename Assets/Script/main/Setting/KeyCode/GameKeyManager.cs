using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKeyManager : MonoBehaviour
{
    // 定义公共静态量：游戏键位管理器
    public static GameKeyManager gkm;

    // 固定键位
    public KeyCode toRight {  get; set; }   // 向右运动
    public KeyCode toLeft { get; set; }    // 向左运动
    public KeyCode esc {  get; set; }   // 暂停游戏

    // 可自定义键位
    public KeyCode run { get; set; }    // 奔跑
    public KeyCode jump { get; set; }    // 跳跃
    public KeyCode dash { get; set; }    // 冲刺
    public KeyCode interaction { get; set; }   // 交互
    public KeyCode grab { get; set; }   // 抓
    public KeyCode map { get; set; }    // 地图
    public KeyCode item { get; set; }   // 物品栏
    public KeyCode cards { get; set; }   // 卡牌库
    public KeyCode book { get; set; }   // 图鉴
    public KeyCode forging { get; set; }    // 锻造

    void Awake()
    {
        if(gkm == null)
        {
            DontDestroyOnLoad(gameObject);
            gkm = this;
        }
        else if(gkm != this)
        {
            Destroy(gameObject);
        }

        // 定义每个键及其初始键位
        toRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("toRightKey", "RightArrow"));
        toLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("toLeftKey", "LeftArrow"));
        esc = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("escKey", "Escape"));
        run = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("runKey", "LeftShift"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "UpArrow"));
        dash = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("dashKey", "Space"));
        interaction = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("interactionKey", "Z"));
        grab = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("grabKey", "X"));
        map = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("mapKey", "Tab"));
        item = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("itemKey", "Q"));
        cards = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("cardsKey", "F"));
        book = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("bookKey", "E"));
        forging = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forgingKey", "LeftAlt"));
    }

    // 获取功能按键名称
    public KeyCode GetKey(string keyName)
    {
        switch (keyName)
        {
            case "toRight":
                return toRight;
            case "toLeft":
                return toLeft;
            case "esc":
                return esc;

            case "run": 
                return run;
            case "jump": 
                return jump;
            case "dash": 
                return dash;
            case "interaction": 
                return interaction;
            case "grab": 
                return grab;
            case "map": 
                return map;
            case "item": 
                return item;
            case "cards": 
                return cards;
            case "book": 
                return book;
            case "forging": 
                return forging;
            default:
                return KeyCode.None;
        }
    }
}
