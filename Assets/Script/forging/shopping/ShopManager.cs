using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // 商店界面UI及动画
    public GameObject shopPanel;
    public Button closeShop;
    public Animator shopping;   // 购物界面显示动画
    public float shopAnimatorTime = 1f;   // 购物界面动画时长
    public bool isOpenShop;

    // 调用其他脚本
    public ForgingDialogManager sprits;

    void Start()
    {
        isOpenShop = false;
        shopPanel.SetActive(false);

        if (closeShop != null )
        {
            closeShop.onClick.AddListener(CloseShop);
        }
    }

    // 打开商店
    public void OpenShop()
    {
        isOpenShop = true;
        shopPanel.SetActive(true);
        shopping.SetBool("isShopping", true);
        StartCoroutine(StartShopping());
    }

    // 关闭商店
    public void CloseShop()
    {
        StartCoroutine(EndShopping());
    }

    // 随机刷新商店商品
    public void RrefreshProduct()
    {

    }

    // 打开商店界面协程
    IEnumerator StartShopping()
    {
        closeShop.interactable = false;   // 禁止使用关闭商店按键
        yield return new WaitForSeconds(shopAnimatorTime);   // 等待动画时长
        closeShop.interactable = true;   // 恢复关闭商店按键的可交互性
    }

    // 关闭商店界面协程
    IEnumerator EndShopping()
    {
        shopping.SetBool("isShopping", false);
        sprits.OnlyTip(5);
        closeShop.interactable = false;
        yield return new WaitForSeconds(shopAnimatorTime);   // 等待动画时长
        closeShop.interactable = true;
        shopPanel.SetActive(false);
        sprits.askButtons[1].interactable = true;
        isOpenShop = false;
    }
}
