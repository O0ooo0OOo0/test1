using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogNPC : MonoBehaviour
{
    public Button[] buttons;
    public TMP_Text answer;
    public string[] answerContent;
    public Animator shopping;
    public GameObject shopping_panel;
    public Button endShopping;

    public StartProduce startProduce;

    public bool isAllAsk;

    void Start()
    {
        answer.text = null;
        answerContent = new string[buttons.Length];
        AnswerContent();

        buttons[0].gameObject.SetActive(true);
        if (buttons[0] != null)
        {
            buttons[0].onClick.AddListener(Answer0);
        }

        if (endShopping != null)
        {
            endShopping.onClick.AddListener(EndShop);
        }
    }

    private void Update()
    {
        if (isAllAsk == true)
        {
            buttons[1].gameObject.SetActive(true);
            buttons[1].onClick.AddListener(Answer1);

            for (int i = 2; i < buttons.Length; i++)
            {
                int index = i;     // 关键：创建局部副本
                buttons[i].gameObject.SetActive(true);
                buttons[i].onClick.AddListener(() => AnswerOther(index));
            }

            return;
        }
    }

    public void Answer0()
    {
        answer.text = answerContent[0].ToString();
        isAllAsk = true;
    }

    public void Answer1()
    {
        if (SceneManager.GetActiveScene().name != "map")
        {
            answer.text = "抱歉，当前环境被污染，锻造殿无法赋予我创造能力";
        }
        else
        {
            answer.text = answerContent[1].ToString();
            buttons[1].interactable = false;
            StartShop();
        }
    }

    public void AnswerOther(int i)
    {
        answer.text = answerContent[i].ToString();
    }

    public void StartShop()
    {
        shopping_panel.SetActive(true);
        shopping.SetBool("isShopping", true);
        StartCoroutine(StartShopping());
    }

    public void EndShop()
    {
        StartCoroutine(EndShopping());
    }

    public void AnswerContent()
    {
        answerContent[0] = "这里是锻造殿，你可以在这里锻造出各种道具和消耗类卡牌。从右边的材料栏中选择材料并投入，左边的锻造鼎就会随机锻造产物。同时，在这里，我将被赋予创造的能力，你可以向我购买一些我可创造的物品。还有什么其他问题都可以问我。";
        answerContent[1] = "好的";
        answerContent[2] = "消耗类卡牌指战斗过程中会被消耗的卡牌，包括目标卡、数值卡和元素卡；非消耗类卡牌是指战斗过程中不会被消耗的卡牌，包括全部的打出卡，即战斗过程中具有攻击、防御、重复等效果的卡牌";
        answerContent[3] = "想好了我再告诉你";
        answerContent[4] = "一次最多只可投入" + startProduce.forgging + "枚铜币，以及10个锻造定向材料";
    }

    IEnumerator StartShopping()
    {
        endShopping.interactable = false;
        yield return new WaitForSeconds(1f);
        endShopping.interactable = true;
    }

    IEnumerator EndShopping()
    {
        shopping.SetBool("isShopping", false);
        endShopping.interactable = false;
        yield return new WaitForSeconds(1f);
        buttons[1].interactable = true;
        endShopping.interactable = true;
        shopping_panel.SetActive(false);
    }
}
