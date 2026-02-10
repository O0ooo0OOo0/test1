using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backage : MonoBehaviour
{
    public Button backage;
    public GameObject backagePanel;
    public bool isOpenBackage;

    void Start()
    {
        isOpenBackage = false;
        backagePanel.SetActive(false);

        if (backage != null)
        {
            backage.onClick.AddListener(SwitchBackage);
        }
    }

    public void SwitchBackage()
    {
        if (isOpenBackage == true)
        {
            backagePanel.SetActive(false);
            isOpenBackage = false;
        }
        else if (isOpenBackage == false)
        {
            backagePanel.SetActive(true);
            isOpenBackage = true;
        }
    }
}
