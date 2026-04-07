  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Reflection;
using GameFramework.Samples.Localization;
using Unity.Android.Gradle;

public class ForgingDialogManager : MonoBehaviour
{
    public Button[] askButtons;   // 提问
    public GameObject[] answers;   // 回答
    public GameObject[] tips;
    public bool isAsked;
    public bool isCombatScene;

    // 调用其他脚本
    public ForgingProduceManager fpM;
    public ShopManager shopManager;

    void Start()
    {
        // 对话内容初始化
        ClearDialogContent();
        DefineTextContent(LanguageType.lt.lanType);

        // 首次使用时只显示第一个问题，有过提问之后显示全部问题
        if (isAsked == false)
        {
            askButtons[0].gameObject.SetActive(true);
            for (int i = 1; i < askButtons.Length; i++)
            {
                askButtons[i].gameObject.SetActive(false);
            }
        }
        else if (isAsked == true)
        {
            foreach (var ask in askButtons)
            {
                ask.gameObject.SetActive(true);
            }
        }
        
        // 绑定按键事件
        if (askButtons[0] != null)
        {
            askButtons[0].onClick.AddListener(Answer0);
        }
        if (askButtons[1] != null)
        {
            askButtons[1].onClick.AddListener(Answer1);
        }
        for (int i = 2; i < askButtons.Length; i++)
        {
            if (askButtons[i] != null)
            {
                int index = i;     // 创建局部副本
                askButtons[i].onClick.AddListener(() => AnswerOther(index));
            }
        }
    }

    // 第一个问题的回答
    public void Answer0()
    {
        OnlyAnswer(0);

        // 首次提问时使用
        if (isAsked == false)
        {
            isAsked = true;

            askButtons[1].gameObject.SetActive(true);
            askButtons[1].onClick.AddListener(Answer1);

            for (int i = 2; i < askButtons.Length; i++)
            {
                int index = i;     
                askButtons[i].gameObject.SetActive(true);
                askButtons[i].onClick.AddListener(() => AnswerOther(index));
            }
        }
    }

    // 第二个问题的回答
    public void Answer1()
    {
        // 战斗时不可购买物品
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;   // 获取当前场景序列数

        // 判断当前场景是否为战斗场景
        CombatSceneManager.csm.JudgeIsCombatScene(currentSceneIndex);
        isCombatScene = CombatSceneManager.csm.isCombatScene;

        // 生成回答或提示
        if (isCombatScene == true)
        {
            OnlyTip(0);
        }
        else if (isCombatScene == false)
        {
            OnlyAnswer(1);
            askButtons[1].interactable = false;
            shopManager.OpenShop();   // 打开商店
            shopManager.RrefreshProduct();   // 每次打开商店时重新刷新商店内容
        }
    }

    // 其他问题的回答
    public void AnswerOther(int index)
    {
        OnlyAnswer(index);
    }

    // 清空回复信息
    public void ClearDialogContent()
    {
        // 回答内容清空
        foreach (var answer in answers)
        {
            answer.SetActive(false);
        }

        // 提示信息清空
        foreach (var tip in tips)
        {
            tip.SetActive(false);
        }
    }

    // 仅显示一个回答内容
    public void OnlyAnswer(int index)
    {
        // 显示回答
        for (int i = 0; i < answers.Length; i++)
        {
            if (i == index)
            {
                answers[i].SetActive(true);
            }
            else
            {
                answers[i].SetActive(false);
            }
        }

        // 不显示提示
        foreach (var tip in tips)
        {
            tip.SetActive(false);
        }
    }

    // 仅显示一个提示内容
    public void OnlyTip(int index)
    {
        // 显示提示
        for (int i = 0; i < tips.Length; i++)
        {
            if (i == index)
            {
                tips[i].SetActive(true);
            }
            else
            {
                tips[i].SetActive(false);
            }
        }

        // 不显示回答
        foreach (var answer in answers)
        {
            answer.SetActive(false);
        }
    }

    // 定义特定回答/提示文本内容
    public void DefineTextContent(int lanT)
    {
        if (lanT == 0)    // 中文
        {
            answers[4].GetComponent<TMP_Text>().text = "一次最多只可投入" + fpM.forgCoinsMax + "枚铜币，以及10个锻造定向材料";
            tips[3].GetComponent<TMP_Text>().text = "锻造炉能力有限，一次只能投入不超过" + fpM.forgCoinsMax + "枚铜币";
        }
        else if (lanT == 1)   // 英文
        {
            answers[4].GetComponent<TMP_Text>().text = "You can deposit up to " + fpM.forgCoinsMax + " copper coins and 10 forging directional materials at a time.";
            tips[3].GetComponent<TMP_Text>().text = "The forging furnace has limited capacity and can only handle no more than " + fpM.forgCoinsMax + " coins";
        }
    }
}
