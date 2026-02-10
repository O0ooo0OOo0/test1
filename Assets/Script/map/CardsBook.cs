using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardsBook : MonoBehaviour
{
    public Button card;
    public GameObject ques;
    public bool isQues;
    public string ques_name = "?";
    public string ques_content = "‘›Œ¥ªÒ»°";
    public string card_name;
    public string card_content;
    public TMP_Text tMP_name;
    public TMP_Text tMP_content;

    void Start()
    {
        ques.SetActive(isQues);

        if (card != null)
        {
            card.onClick.AddListener(GetMoreDetails);
        }
    }

    public void GetMoreDetails()
    {
        if (isQues == true)
        {
            tMP_name.text = ques_name;
            tMP_content.text = ques_content;
        }
        else if (isQues == false)
        {
            tMP_name.text = card_name;
            tMP_content.text = card_content;
        }
    }
}
