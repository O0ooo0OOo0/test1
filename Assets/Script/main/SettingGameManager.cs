using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingGameManager : MonoBehaviour
{
    // 设置界面UI
    public GameObject settingPanel;
    public GameObject mainSetting;  // 设置主界面
    public Button closeSettingPanel;  // 关闭设置界面
    public Button key, sound, language;
    public Button reb1, reb2, reb3;  // 返回按键
    public GameObject keySetting, soundSetting, languageSetting;  // 三个设置界面

    // esc脚本
    public escManager esc;

    void Start()
    {
        settingPanel.SetActive(false);

        if (closeSettingPanel != null)
        {
            closeSettingPanel.onClick.AddListener(CloseSettingPanel);
        }
        if (key != null)
        {
            key.onClick.AddListener(ToKey);
        }
        if (sound != null)
        {
            sound.onClick.AddListener(ToSound);
        }
        if (language != null)
        {
            language.onClick.AddListener(ToLanguage);
        }
        if (reb1 != null && reb2 != null && reb3 != null)
        {
            reb1.onClick.AddListener(ReturnMainSetting);
            reb2.onClick.AddListener(ReturnMainSetting);
            reb3.onClick.AddListener(ReturnMainSetting);
        }
    }

    // 打开设置界面
    public void OpenSettingPanel()
    {
        settingPanel.SetActive(true);
        mainSetting.SetActive(true);   // 首先出现主设置界面
        keySetting.SetActive(false);
        soundSetting.SetActive(false);
        languageSetting.SetActive(false);
    }

    // 关闭设置界面
    public void CloseSettingPanel()
    {
        // 如果是在主界面，直接关闭
        if (SceneManager.GetActiveScene().name == "main")
        {
            settingPanel.SetActive(false);
        }
        // 如果是在游戏中，则返回ESC面板
        else
        {
            settingPanel.SetActive(false);
            esc.escPanel.SetActive(true);
        }
    }

    // 进入键位设置
    public void ToKey()
    {
        mainSetting.SetActive(false);
        keySetting.SetActive(true);
    }

    // 进入音量设置
    public void ToSound()
    {
        mainSetting.SetActive(false);
        soundSetting.SetActive(true);
    }

    // 进入语言设置
    public void ToLanguage()
    {
        mainSetting.SetActive(false);
        languageSetting.SetActive(true);
    }

    // 返回主设置界面
    public void ReturnMainSetting()
    {
        keySetting.SetActive(false);
        soundSetting.SetActive(false);
        languageSetting.SetActive(false);
        mainSetting.SetActive(true);
    }
}
