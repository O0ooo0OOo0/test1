using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookManager : MonoBehaviour
{
    // 图鉴面板相关UI
    public GameObject bookPanel, cardsBook, biologyBook;
    public Button cardsButton, biologyButton;
    public Button closeBook;
    public bool isCanBook;
    public bool isOpenBook;

    // 调用其他脚本
    public CardsBook cb;

    void Start()
    {
        bookPanel.SetActive(false);
        isOpenBook = false;

        if (cardsButton != null)
        {
            cardsButton.onClick.AddListener(CardsBook);
        }
        if (biologyButton != null)
        {
            biologyButton.onClick.AddListener(BiologyBook);
        }
        if (closeBook != null)
        {
            closeBook.onClick.AddListener(CloseBookPanel);
        }
    }

    // 检测是否调用图鉴面板
    void Update()
    {
        if (isCanBook && Input.GetKeyDown(GameKeyManager.gkm.book))
        {
            if (isOpenBook == false)
            {
                OpenBookPanel();
            }
            else if (isOpenBook == true)
            {
                CloseBookPanel();
            }
        }
        if (isCanBook && isOpenBook && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseBookPanel();
        }
    }

    // 打开图鉴面板
    public void OpenBookPanel()
    {
        isOpenBook = true;
        InputManager.im.EnableBook();
        MouseManager.mouse.ShowMouse();
        bookPanel.SetActive(true);
        cb.GetBookCardsInf();
    }

    // 关闭图鉴面板（不重置图鉴面板打开时的第一内容）
    public void CloseBookPanel()
    {
        bookPanel.SetActive(false);
        isOpenBook = false;
        InputManager.im.EnableKeysInput();
        MouseManager.mouse.HideMouse();
    }

    // 卡牌图鉴
    public void CardsBook()
    {
        cardsBook.SetActive(true);
        biologyBook.SetActive(false);
    }

    // 生物图鉴
    public void BiologyBook()
    {
        cardsBook.SetActive(false);
        biologyBook.SetActive(true);
    }
}
