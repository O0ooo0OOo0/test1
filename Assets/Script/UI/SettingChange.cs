using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingChange : MonoBehaviour
{
    public Button setting;
    public Button close_setting;
    public GameObject settingPanel;

    void Start()
    {
        settingPanel.SetActive(false);

        if (setting != null)
        {
            setting.onClick.AddListener(OpenSettingPanel);
        }
        if (close_setting != null)
        {
            close_setting.onClick.AddListener(CloseSettingPanel);
        }
    }

    public void OpenSettingPanel()
    {
        settingPanel.SetActive(true);
    }

    public void CloseSettingPanel()
    {
        settingPanel.SetActive(false);
    }
}
