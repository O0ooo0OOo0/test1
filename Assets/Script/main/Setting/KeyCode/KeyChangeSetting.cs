using GameFramework.Samples.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyChangeSetting : MonoBehaviour
{
    Transform setKeyColl;  // 键位所在路径
    TMP_Text buttonText;

    private bool isListeningForKey = false;
    private string targetKeyName = null;
    private TMP_Text targetButtonText = null;
    private KeyCode originalKeyCode;

    // 正在修改的键位UI
    public GameObject[] choseTables;

    // 所有键位字典(只读)
    private static readonly string[] KeyNames = {"toRight","toLeft","esc","run", "jump", "dash", "interaction", "grab","map", "item", "cards", "book", "forging"};

    void Start()
    {
        setKeyColl = transform.Find("keyBord");   // 找到键位所在的集合路径
        RefreshKeyBindings();
    }

    private void OnGUI()
    {
        // 使用OnGUI检测键盘事件，可以捕获任意按键
        if (!isListeningForKey) 
        { 
            return; 
        }

        Event e = Event.current;
        if (e != null && e.isKey && e.type == EventType.KeyDown)
        {
            // 过滤掉一些特殊按键（如左Shift单独按下时不触发，配合其他键时才触发）
            if (e.keyCode == KeyCode.None) 
            { 
                return; 
            }

            // 过滤修饰键单独按下，但允许它们作为独立按键
            if (e.keyCode == KeyCode.LeftShift || e.keyCode == KeyCode.RightShift || e.keyCode == KeyCode.LeftControl || e.keyCode == KeyCode.RightControl || e.keyCode == KeyCode.LeftAlt || e.keyCode == KeyCode.RightAlt || e.keyCode == KeyCode.LeftCommand || e.keyCode == KeyCode.RightCommand)
            {
                // 修饰键可以单独作为按键绑定
            }

            OnKeyDetected(e.keyCode);
        }
    }

    // 按键重绑定系统
    public void StartAssignment(string keyName)
    {
        // 如果正在监听其他键，先恢复它
        if (isListeningForKey && targetKeyName != keyName)
        {
            RestorePreviousKey();
            ClearAllChoseTables();
        }

        // 保存当前状态
        targetKeyName = keyName;
        targetButtonText = buttonText;
        originalKeyCode = GameKeyManager.gkm.GetKey(keyName);

        // 显示提示
        isListeningForKey = true;
        if (LanguageType.lt.lanType == 0)   // 根据当前的不同语言生成对应提示词
        {
            buttonText.text = "输入";
        }
        else if (LanguageType.lt.lanType == 0)
        {
            buttonText.text = "Press Key";
        }
    }

    private void OnKeyDetected(KeyCode pressedKey)
    {
        // 检查冲突
        if (IsKeyAlreadyAssigned(pressedKey, targetKeyName))
        {
            StartCoroutine(ShowConflictAndRestore());
            return;
        }

        // 保存新键位
        SaveKeyBinding(targetKeyName, pressedKey);
        ClearAllChoseTables();

        // 清理状态
        ClearState();
    }

    private IEnumerator ShowConflictAndRestore()
    {
        if (LanguageType.lt.lanType == 0)   // 根据当前的不同语言生成对应提示词
        {
            targetButtonText.text = "按键重复";
        }
        else if (LanguageType.lt.lanType == 0)
        {
            targetButtonText.text = "Repeat";
        }

        yield return new WaitForSeconds(0.7f);
        RestorePreviousKey();
        ClearAllChoseTables();
    }

    // 恢复当前正在修改的键位到原始状态
    private void RestorePreviousKey()
    {
        if (targetButtonText != null)
        {
            targetButtonText.text = originalKeyCode.ToString();
        }
        ClearState();
    }

    // 恢复未完成的修改（供返回按钮调用）
    public void OnReturnButtonClicked()
    {
        if (isListeningForKey)
        {
            RestorePreviousKey();
            ClearAllChoseTables();
        }
    }

    // 清理追踪状态
    private void ClearState()
    {
        isListeningForKey = false;
        targetKeyName = null;
        targetButtonText = null;
    }

    // 修改对应文本内容
    public void SendText(TMP_Text text)
    {
       buttonText = text;
    }

    // 检查某个按键是否已被其他功能占用，防止按键冲突
    private bool IsKeyAlreadyAssigned(KeyCode key, string currentKeyName)
    {
        // 遍历预定义的按键名称数组
        foreach (var name in KeyNames)  
        {
            if (name == currentKeyName)  // 跳过当前正在修改的功能
            {
                continue;
            }
        
            KeyCode assignedKey = GameKeyManager.gkm.GetKey(name);    // 获取该功能当前绑定的按键
            
            if (assignedKey == key)   // 如果发现其他功能已使用相同的按键
            { 
                return true;   // 存在冲突
            }
        }
        return false;   // 不存在冲突
    }

    // 保存按键的逻辑
    private void SaveKeyBinding(string KeyName, KeyCode key)
    {
        switch (KeyName)
        {
            case "run":
                GameKeyManager.gkm.run = key;
                buttonText.text = key.ToString();
                PlayerPrefs.SetString("runKey", key.ToString());
                break;
            case "jump":
                GameKeyManager.gkm.jump = key;
                buttonText.text = key.ToString();
                PlayerPrefs.SetString("jumpKey", key.ToString());
                break;
            case "dash":
                GameKeyManager.gkm.dash = key;
                buttonText.text = key.ToString();
                PlayerPrefs.SetString("dashKey", key.ToString());
                break;
            case "interaction":
                GameKeyManager.gkm.interaction = key;
                buttonText.text = key.ToString();
                PlayerPrefs.SetString("interactionKey", key.ToString());
                break;
            case "grab":
                GameKeyManager.gkm.grab = key;
                buttonText.text = key.ToString();
                PlayerPrefs.SetString("grabKey", key.ToString());
                break;
            case "map":
                GameKeyManager.gkm.map = key;
                buttonText.text = key.ToString();
                PlayerPrefs.SetString("mapKey", key.ToString());
                break;
            case "item":
                GameKeyManager.gkm.item = key;
                buttonText.text = key.ToString();
                PlayerPrefs.SetString("itemKey", key.ToString());
                break;
            case "cards":
                GameKeyManager.gkm.cards = key;
                buttonText.text = key.ToString();
                PlayerPrefs.SetString("cardsKey", key.ToString());
                break;
            case "book":
                GameKeyManager.gkm.book = key;
                buttonText.text = key.ToString();
                PlayerPrefs.SetString("bookKey", key.ToString());
                break;
            case "forging":
                GameKeyManager.gkm.forging = key;
                buttonText.text = key.ToString();
                PlayerPrefs.SetString("forgingKey", key.ToString());
                break;
        }
        PlayerPrefs.Save();
    }

    // 更新按键文本显示
    public void RefreshKeyBindings()
    {
        for (int i = 0; i < setKeyColl.childCount; i++)
        {
            var button = setKeyColl.GetChild(i);
            var buttonText = button.GetComponentInChildren<TMP_Text>();

            switch (button.name)
            {
                case "run":
                    buttonText.text = GameKeyManager.gkm.run.ToString();
                    break;
                case "jump":
                    buttonText.text = GameKeyManager.gkm.jump.ToString();
                    break;
                case "dash":
                    buttonText.text = GameKeyManager.gkm.dash.ToString();
                    break;
                case "interaction":
                    buttonText.text = GameKeyManager.gkm.interaction.ToString();
                    break;
                case "grab":
                    buttonText.text = GameKeyManager.gkm.grab.ToString();
                    break;
                case "map":
                    buttonText.text = GameKeyManager.gkm.map.ToString();
                    break;
                case "item":
                    buttonText.text = GameKeyManager.gkm.item.ToString();
                    break;
                case "cards":
                    buttonText.text = GameKeyManager.gkm.cards.ToString();
                    break;
                case "book":
                    buttonText.text = GameKeyManager.gkm.book.ToString();
                    break;
                case "forging":
                    buttonText.text = GameKeyManager.gkm.forging.ToString();
                    break;
            }
        }
    }

    // 清除正在修改UI
    public void ClearAllChoseTables()
    {
        foreach (var chos in choseTables)
        {
            chos.SetActive(false);
        }
    }
}
