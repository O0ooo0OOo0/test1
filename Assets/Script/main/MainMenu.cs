using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameController : MonoBehaviour
{
    public Button start, setting, achievement, exit;    // 开始游戏、设置、成就、退出游戏

    // 开始游戏
    public SelectArchiveManager archiveManager;

    // 成就
    public AchievementManager achievementManager;

    // 退出游戏
    public GameObject tipsExitPanel;
    public Button exitSure, exitCancel;

    // 游戏设置脚本（需要调用全局静态变量下的function物体）
    private PlayerManager player;
    private Transform function;

    void Start()
    {
        MouseManager.mouse.ShowMouse();

        // 找到游戏设置脚本
        player = PlayerManager.pm;
        function = player.transform.Find("function");

        // 绑定按键功能
        if (start != null)   // 开始游戏
        {
            start.onClick.AddListener(StartGame);
        }
        if (setting != null)   // 游戏设置
        {
            setting.onClick.AddListener(SettingGame);
        }
        if (achievement != null)   // 成就
        {
            achievement.onClick.AddListener(AchievementGame);
        }
        if (exit != null)   // 退出游戏
        {
            exit.onClick.AddListener(QuitGame);
        }

        // 点击退出游戏提示界面
        tipsExitPanel.SetActive(false);

        if (exitSure != null)
        {
            exitSure.onClick.AddListener(ExitGameSure);
        }
        if (exitCancel != null)
        {
            exitCancel.onClick.AddListener(ExitGameCancel);
        }
    }

    // 开始游戏
    public void StartGame()
    {
        archiveManager.OpenSelectArcPanel();
    }

    // 设置
    public void SettingGame()
    {
        function.GetComponent<SettingGameManager>().OpenSettingPanel();
    }

    // 成就
    public void AchievementGame()
    {
        achievementManager.OpenAchievementsPanel();
    }

    // 退出游戏
    public void QuitGame()
    {
        tipsExitPanel.SetActive(true);
    }

    // 确认退出游戏
    public void ExitGameSure()
    {
#if UNITY_EDITOR
        // 在 Unity 编辑器中，使用 UnityEditor.EditorApplication.isPlaying 来停止播放
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 在发布的游戏中，使用 Application.Quit() 来退出游戏
        Application.Quit();
#endif
    }

    // 取消退出游戏
    public void ExitGameCancel()
    {
        tipsExitPanel.SetActive(false);
    }
}
