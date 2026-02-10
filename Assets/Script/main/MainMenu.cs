using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public Button start, archive, setting, exit;    // 开始游戏、继续游戏、设置、退出游戏
    public GameObject black;  // 渐黑
    public float fadeOutSpeed = 1f;   // 渐隐速度

    // 已有存档时，点击开始游戏的提示界面
    public GameObject tipsStartPanel;
    public Button startSure, startCancel;

    // 存档脚本
    public ArchiveGameManager archiveGame;

    // 游戏设置脚本
    public SettingGameManager settingGame;

    // 退出游戏提示界面
    public GameObject tipsExitPanel;
    public Button exitSure, exitCancel;

    void Start()
    {
        JudgeIsArchive();
        black.SetActive(false);

        if (start != null)   // 开始游戏
        {
            start.onClick.AddListener(StartGame);
        }
        if (archive != null)   // 继续游戏（存档）
        {
            archive.onClick.AddListener(ArchiveGame);
        }
        if (setting != null)   // 游戏设置
        {
            setting.onClick.AddListener(SettingGame);
        }
        if (exit != null)   // 结束游戏
        {
            exit.onClick.AddListener(QuitGame);
        }

        // 已有存档时点击开始游戏提示
        tipsStartPanel.SetActive(false);

        if (startSure != null)
        {
            startSure.onClick.AddListener(StartGameAgain);
        }
        if (startCancel != null)
        {
            startCancel.onClick.AddListener(StartGameAgainCancel);
        }

        // 点击退出游戏提示
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

    // 判断存档（继续游戏）按键是否可交互
    public void JudgeIsArchive()
    {
        if (archiveGame.isArchive == false)
        {
            archive.interactable = false;
        }
        else if (archiveGame.isArchive == true)
        {
            archive.interactable = true;
        }
    }

    // 开始游戏
    public void StartGame()
    {
        if (archiveGame.isArchive == false)   // 如果没有存档
        {
            StartCoroutine(ExitScene());    // 直接开始游戏
        }
        else if (archiveGame.isArchive == true)   // 如果有存档
        {
            tipsStartPanel.SetActive(true);   // 提示：是否重新开始游戏
        }
    }

    // 确认重新开始游戏
    public void StartGameAgain()
    {
        archiveGame.DeleteAllSave();   // 删除全部存档
        JudgeIsArchive();   // 重置继续游戏是否可交互
        tipsStartPanel.SetActive(false);
        StartCoroutine(ExitScene());  // 开始游戏
    }

    // 取消重新开始游戏
    public void StartGameAgainCancel()
    {
        tipsStartPanel.SetActive(false);
    }

    // 继续游戏
    public void ArchiveGame()
    {
        archiveGame.OpenArchivePanel();
    }

    // 设置
    public void SettingGame()
    {
        settingGame.OpenSettingPanel();
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

    // 开始游戏协程（渐隐+场景切换）
    IEnumerator ExitScene()
    {
        black.SetActive(true);
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeOutSpeed;
            black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        alpha = 1f;
        black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
        SceneManager.LoadScene("1-1");
    }
}
