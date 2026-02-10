using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArchiveGameManager : MonoBehaviour
{
    public bool isArchive;   // 是否有存档

    // 存档界面UI
    public GameObject archivePanel;
    public Button closeArchivePanel;

    // 存档内容
    public string sceneName;   // 存档所在场景

    void Start()
    {
        archivePanel.SetActive(false);

        if (closeArchivePanel != null)
        {
            closeArchivePanel.onClick.AddListener(CloseArchivePanel);
        }
    }

    // 打开存档界面
    public void OpenArchivePanel()
    {
        archivePanel.SetActive(true);
    }

    // 清除所有存档
    public void DeleteAllSave()
    {
        isArchive = false;
    }

    // 关闭存档界面
    public void CloseArchivePanel()
    {
        archivePanel.SetActive(false);
    }
}
