using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextContentShow : MonoBehaviour
{
    public AnimationEndTrigger trigger;
    public GameObject answerScroll;
    public TMP_Text answer;
    public bool isCanShow;

    void Start()
    {
        answerScroll.SetActive(false);
    }

    public void Update()
    {
        isCanShow = trigger.iscanBeShow;
        JudgeIsShow();
    }

    public void JudgeIsShow()
    {
        if (isCanShow == false || answer.text == "")
        {
            answerScroll.SetActive(false);
        }
        else if (isCanShow == true && answer.text != "")
        {
            answerScroll.SetActive(true);
        }
    }
}
