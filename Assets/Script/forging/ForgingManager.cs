using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ForgingManager : MonoBehaviour
{
    // 锻造界面UI
    public GameObject forgingPanel;   // 锻造主界面
    public Button closeForging;
    public GameObject fadeImage;
    public float fadeInSpeed = 1f;
    public float add;
    public bool isCanForging;
    public bool isOpenForging;

    // 调用其他脚本
    public ForgingDialogManager sprits;
    public CancelInput cancelInput;
    public ShopManager shopM;
    public ForgingSure fs;

    void Start()
    {
        isOpenForging = false;
        forgingPanel.SetActive(false);
        fadeImage.SetActive(false);

        if (closeForging != null)
        {
            closeForging.onClick.AddListener(CloseForgingPanel);
        }
    }

    private void Update()
    {
        if (isCanForging && Input.GetKeyDown(GameKeyManager.gkm.forging))
        {
            if (isOpenForging == false)
            {
                OpenForgingPanel();
            }
            else if (isOpenForging == true)
            {
                CloseForgingPanel();
            }
        }
        if (isCanForging && isOpenForging && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseForgingPanel();
        }
    }

    // 打开锻造界面
    public void OpenForgingPanel()
    {
        isOpenForging = true;
        InputManager.im.EnableForging();
        MouseManager.mouse.ShowMouse();
        StartCoroutine(EnterForgingPanel());
    }

    // 关闭锻造界面
    public void CloseForgingPanel()
    {
        StartCoroutine(ExitForgingPanel());
        isOpenForging = false;
        InputManager.im.EnableKeysInput();
        MouseManager.mouse.HideMouse();
    }

    // 打开协程
    IEnumerator EnterForgingPanel()
    {
        InputBlocker.ib.DisableAllInput();   // 禁用输入

        fadeImage.SetActive(true);
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * (fadeInSpeed + add);
            fadeImage.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        forgingPanel.SetActive(true);   // 锻造面板打开

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeInSpeed;
            fadeImage.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        alpha = 0f;
        fadeImage.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
        fadeImage.SetActive(false);

        InputBlocker.ib.EnableAllInput();   // 恢复输入
    }

    // 关闭协程
    IEnumerator ExitForgingPanel()
    {
        InputBlocker.ib.DisableAllInput();   // 禁用输入

        fadeImage.SetActive(true);
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * (fadeInSpeed + add);
            fadeImage.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        ResetForgingPanel();
        forgingPanel.SetActive(false);   // 关闭锻造面板

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeInSpeed;
            fadeImage.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        alpha = 0f;
        fadeImage.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
        fadeImage.SetActive(false);

        InputBlocker.ib.EnableAllInput();   // 恢复输入
    }

    // 重置锻造面板信息
    public void ResetForgingPanel()
    {
        // 重置问答提示信息
        sprits.ClearDialogContent();

        // 重置锻造材料
        cancelInput.RefreshInputs();

        //  关闭锻造结果界面
        fs.CloseProductsPanel();

        // 关闭商店界面
        if (shopM.isOpenShop == true)
        {
            shopM.CloseShop();
        }
    }
}
