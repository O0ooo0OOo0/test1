using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsBagManager : MonoBehaviour
{
    public GameObject cardsBagPanel;
    public Button closeCards;
    public bool isCanCardsBag;
    public bool isOpenCardsBag;

    // 调用其他脚本
    public CardsBagAmountText cardsAmountText;

    void Start()
    {
        isOpenCardsBag = false;
        cardsBagPanel.SetActive(false);

        if (closeCards != null)
        {
            closeCards.onClick.AddListener(CloseCardsBagPanel);
        }
    }

    private void Update()
    {
        if (isCanCardsBag && Input.GetKeyDown(GameKeyManager.gkm.cards))
        {
            if (isOpenCardsBag == false)
            {
                OpenCardsBagPanel();
            }
            else if (isOpenCardsBag == true)
            {
                CloseCardsBagPanel();
            }
        }
        if (isCanCardsBag && isOpenCardsBag && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCardsBagPanel();
        }
    }

    // 打开卡牌库
    public void OpenCardsBagPanel()
    {
        isOpenCardsBag = true;
        InputManager.im.EnableCardsBag();
        MouseManager.mouse.ShowMouse();
        cardsBagPanel.SetActive(true);
        cardsAmountText.AmountTextCards();   // 每次打开卡牌库时更新卡牌数量
    }

    // 关闭卡牌库
    public void CloseCardsBagPanel()
    {
        cardsBagPanel.SetActive(false);
        isOpenCardsBag = false;
        InputManager.im.EnableKeysInput();
        MouseManager.mouse.HideMouse();
    }
}
