using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsBackage : MonoBehaviour
{
    public Button itemsBackage;
    public GameObject itemsBackagePanel;
    public bool isOpenItemsBackage;

    void Start()
    {
        isOpenItemsBackage = false;
        itemsBackagePanel.SetActive(false);

        if (itemsBackage != null)
        {
            itemsBackage.onClick.AddListener(SwitchItemsBackage);
        }
    }

    public void SwitchItemsBackage()
    {
        if (isOpenItemsBackage == true)
        {
            itemsBackagePanel.SetActive(false);
            isOpenItemsBackage = false;
        }
        else if (isOpenItemsBackage == false)
        {
            itemsBackagePanel.SetActive(true);
            isOpenItemsBackage = true;
        }
    }
}
