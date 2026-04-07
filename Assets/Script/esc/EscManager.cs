using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscManager : MonoBehaviour
{
    // ESC面板UI
    public GameObject escPanel;
    public GameObject escMain;
    public Button continueButton, settingButton, reMainButton;
    public bool isCanEsc;
    public bool isOpenEsc;

    // 返回主界面UI
    public GameObject reMainTips;
    public Button sure, cancel;

    // 退出场景
    public bool isFadeOut;

    // 调用其他脚本
    public SettingGameManager settingGameManager;

    void Start()
    {
        escPanel.SetActive(false);
        isOpenEsc = false;
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
        if (isCanEsc == true && Input.GetKeyDown(GameKeyManager.gkm.esc))
        {
            if (isOpenEsc == false)
            {
                OpenEscPanel();    
            }
        }
    }

    // 打开ESC面板
    public void OpenEscPanel()
    {
        isOpenEsc = true;
        InputManager.im.EnableEsc();
        MouseManager.mouse.ShowMouse();

        escPanel.SetActive(true);
        escMain.SetActive(true);
        reMainTips.SetActive(false);
    }

    // 继续游戏则关闭ESC面板
    public void ContinueGame()
    {
        escPanel.SetActive(false);
        isOpenEsc = false;
        InputManager.im.EnableKeysInput();
        MouseManager.mouse.HideMouse();
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
        ReMain();
        escPanel.SetActive(false);
        isOpenEsc = false;
        MouseManager.mouse.HideMouse();
        isFadeOut = true;
    }

    // 取消返回主界面
    public void CancelReturnMain()
    {
        reMainTips.SetActive(false);
        escMain.SetActive(true);
    }

    // 返回主界面时需运行的函数
    public void ReMain()
    {
        ArchiveGameManager.arcm.UpdateArcsInf();
    }
}
