using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public GameObject achievementsPanel;
    public Button close;

    void Start()
    {
        achievementsPanel.SetActive(false);

        if (close != null)
        {
            close.onClick.AddListener(CloseAchievementsPanel);
        }
    }

    // 打开成就界面
    public void OpenAchievementsPanel()
    {
        achievementsPanel.SetActive(true);
    }

    // 关闭成就界面
    public void CloseAchievementsPanel()
    {
        achievementsPanel.SetActive(false);
    }
}
