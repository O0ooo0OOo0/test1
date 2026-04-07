using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ForgingSure : MonoBehaviour
{
    public Button sureProducts;
    public GameObject productsPanel;
    public ForgingProduceManager fpm;

    void Start()
    {
        if (sureProducts != null)
        {
            sureProducts.onClick.AddListener(CloseProductsPanel);
        }
    }

    // 关闭锻造结果界面
    public void CloseProductsPanel()
    {
        ResetProductsPanel();
        productsPanel.SetActive(false);
    }

    // 重置锻造结果界面
    public void ResetProductsPanel()
    {
        // 锻造
        for (int i = 0; i < fpm.products.Count; i++)
        {
            var product = fpm.products[i];
            product.product.SetActive(false);
            product.amount = 0;
            product.productContent.text = null;
            fpm.products[i] = product;
        }

        // 额外锻造
        fpm.extraPdtsPanel.SetActive(false);
        fpm.exPdtsText.text = null;
    }
}
