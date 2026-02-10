using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TargetCards;
using static UnityEngine.GraphicsBuffer;

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
        if (pages.Count > 0)
        {
            Pages firstPage = pages[0];
            firstPage.page.SetActive(true);
            firstPage.labelPage.SetActive(true);
        }

        for (int i = 0; i < pages.Count; i++)
        {
            if (i != 0)
            {
                Pages otherPage = pages[i];
                otherPage.page.SetActive(false);
                otherPage.labelPage.SetActive(false);
            }
        }

        foreach (var pages in pages)
        {
            if (pages.changePage != null)
            {
                pages.changePage.onClick.AddListener(() => ChangePage(pages.page, pages.labelPage));
            }
        }
    }

    public void ChangePage(GameObject targetPage, GameObject targetLabel)
    {
        foreach (var pages in pages)
        {
            if (pages.page == targetPage)
            {
                pages.page.SetActive(true);
            }
            else if (pages.page != targetPage)
            {
                pages.page.SetActive(false);
            }
        }

        foreach (var pages in pages)
        {
            if (pages.labelPage == targetLabel)
            {
                pages.labelPage.SetActive(true);
            }
            else if (pages.labelPage != targetLabel)
            {
                pages.labelPage.SetActive(false);
            }
        }
    }
}
