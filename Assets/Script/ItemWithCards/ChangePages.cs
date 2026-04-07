using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePages : MonoBehaviour
{
    [System.Serializable]
    public class Pages
    {
        public Button changePage;
        public GameObject page;
        public GameObject labelPage;
    }

    public List<Pages> pages;

    void Start()
    {
        // 初始化物品栏状态
        if (pages.Count > 0)
        {
            Pages firstPage = pages[0];
            firstPage.page.SetActive(true);
            firstPage.labelPage.SetActive(true);
            pages[0] = firstPage;
        }

        for (int i = 0; i < pages.Count; i++)
        {
            if (i != 0)
            {
                Pages otherPage = pages[i];
                otherPage.page.SetActive(false);
                otherPage.labelPage.SetActive(false);
                pages[i] = otherPage;
            }
        }

        foreach (var page in pages)
        {
            if (page.changePage != null)
            {
                page.changePage.onClick.AddListener(() => ChangePage(page.page, page.labelPage));
            }
        }
    }

    // 修改页面及对应页面指示标志
    public void ChangePage(GameObject targetPage, GameObject targetLabel)
    {
        foreach (var page in pages)
        {
            if (page.page == targetPage)
            {
                page.page.SetActive(true);
            }
            else if (page.page != targetPage)
            {
                page.page.SetActive(false);
            }
        }

        foreach (var page in pages)
        {
            if (page.labelPage == targetLabel)
            {
                page.labelPage.SetActive(true);
            }
            else if (page.labelPage != targetLabel)
            {
                page.labelPage.SetActive(false);
            }
        }
    }
}
