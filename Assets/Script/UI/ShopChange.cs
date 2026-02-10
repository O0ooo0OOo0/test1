using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopChange : MonoBehaviour
{
    public Button shop;
    public Button close_shop;
    public GameObject shopPanel;
    public GameObject black;
    public float fadeInSpeed = 1f;
    public float add;

    public DialogNPC npcAnswer;

    void Start()
    {
        shopPanel.SetActive(false);

        if (shop != null)
        {
            shop.onClick.AddListener(OpenShopPanel);
        }
        if (close_shop != null)
        {
            close_shop.onClick.AddListener(CloseShopPanel);
        }
    }

    public void OpenShopPanel()
    {
        StartCoroutine(EnterPanel());
    }

    public void CloseShopPanel()
    {
        StartCoroutine(ExitPanel());
    }

    IEnumerator EnterPanel()
    {
        black.SetActive(true);
        float alpha = 0f;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * (fadeInSpeed + add);
            black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        shopPanel.SetActive(true);

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeInSpeed;
            black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        alpha = 0f;
        black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
        black.SetActive(false);
    }

    IEnumerator ExitPanel()
    {
        black.SetActive(true);
        float alpha = 0f;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * (fadeInSpeed + add);
            black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        ResetDialogAnswer();
        shopPanel.SetActive(false);

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeInSpeed;
            black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        alpha = 0f;
        black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
        black.SetActive(false);
    }

    public void ResetDialogAnswer()
    {
        npcAnswer.answer.text = null;
        npcAnswer.shopping_panel.SetActive(false);
        npcAnswer.buttons[1].interactable = true;
    }
}
