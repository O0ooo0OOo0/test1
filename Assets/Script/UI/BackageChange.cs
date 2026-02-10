using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackageChange : MonoBehaviour
{
    public Button bag;
    public Button close_bag;
    public GameObject bag_panel;


    void Start()
    {
        bag_panel.SetActive(false);

        if (bag != null)
        {
            bag.onClick.AddListener(OpenBagPanel);
        }
        if (close_bag != null)
        {
            close_bag.onClick.AddListener(CloseBagPanel);
        }
    }

    public void OpenBagPanel()
    {
        bag_panel.SetActive(true);
    }

    public void CloseBagPanel()
    {
        bag_panel.SetActive(false);
    }
}
