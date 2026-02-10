using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProduceSure : MonoBehaviour
{
    public Button sure;
    public GameObject produce_panel;
    public StartProduce forgging;

    void Start()
    {
        if (sure != null)
        {
            sure.onClick.AddListener(CloseProduce);
        }
    }

    public void CloseProduce()
    {
        ResetProduce();
        produce_panel.SetActive(false);
    }

    public void ResetProduce()
    {
        for (int i = 0; i < forgging.products.Count; i++)
        {
            var product = forgging.products[i];
            product.product.SetActive(false);
            product.amount = 0;
            product.creat_content.text = null;
            forgging.products[i] = product;
        }
    }
}
