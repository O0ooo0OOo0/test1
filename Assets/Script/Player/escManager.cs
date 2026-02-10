using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class escManager : MonoBehaviour
{
    // ESC面板UI
    public GameObject escPanel;
    public GameObject escMain;
    public Button continueButton, settingButton, reMainButton;

    // 返回主界面UI
    public GameObject reMainTips;
    public Button sure, cancel;

    // 退出场景UI
    public bool isFadeOut;

    // 设置脚本
    public SettingGameManager settingGameManager;

    void Start()
    {
        escPanel.SetActive(false);
        isFadeOut = false;

        if (continueButton != null)
        {
            continueButton.onClick.AddListener(ContinueGame);
        }
        if (settingButton != null)
        {
            settingButton.onClick.AddListener(Setting);
        }
        if (reMainButton != null)
        {
            reMainButton.onClick.AddListener(ReturnMain);
        }

        if (sure != null)
        {
            sure.onClick.AddListener(SureReturnMain);
        }
        if (cancel != null)
        {
            cancel.onClick.AddListener(CancelReturnMain);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenEscPanel();
        }
    }

    // 打开ESC面板
    public void OpenEscPanel()
    {
        escPanel.SetActive(true);
        escMain.SetActive(true);
        reMainTips.SetActive(false);
    }

    // 继续游戏则关闭ESC面板
    public void ContinueGame()
    {
        escPanel.SetActive(false);
    }

    // 打开设置
    public void Setting()
    {
        escPanel.SetActive(false);
        settingGameManager.OpenSettingPanel();
    }

    // 返回主界面
    public void ReturnMain()
    {
        escMain.SetActive(false);
        reMainTips.SetActive(true);
    }

    // 确认返回主界面
    public void SureReturnMain()
    {
        escPanel.SetActive(false);
        isFadeOut = true;
    }

    // 取消返回主界面
    public void CancelReturnMain()
    {
        reMainTips.SetActive(false);
        escMain.SetActive(true);
    }
}
