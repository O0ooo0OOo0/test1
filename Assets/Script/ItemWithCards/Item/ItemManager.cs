using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public GameObject itemPanel;
    public Button closeItem;
    public bool isCanItem;
    public bool isOpenItem;

    // 调用其他脚本
    public ItemAmountText itemAmountText;

    void Start()
    {
        isOpenItem = false;
        itemPanel.SetActive(false);

        if (closeItem != null)
        {
            closeItem.onClick.AddListener(CloseItemPanel);
        }
    }

    private void Update()
    {
        if (isCanItem && Input.GetKeyDown(GameKeyManager.gkm.item))
        {
            if (isOpenItem == false)
            {
                OpenItemPanel();
            }
            else if (isOpenItem == true)
            {
                CloseItemPanel();
            }
        }
        if (isCanItem && isOpenItem && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseItemPanel();
        }
    }

    // 打开物品栏
    public void OpenItemPanel()
    {
        isOpenItem = true;
        InputManager.im.EnableItem();
        MouseManager.mouse.ShowMouse();
        itemPanel.SetActive(true);
        itemAmountText.AmountTextItem();   // 每次打开物品栏时更新物品数量
    }

    // 关闭物品栏
    public void CloseItemPanel()
    {
        itemPanel.SetActive(false);
        isOpenItem = false;
        InputManager.im.EnableKeysInput();
        MouseManager.mouse.HideMouse();
    }
}
