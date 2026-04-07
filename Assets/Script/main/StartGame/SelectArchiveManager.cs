using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class SelectArchiveManager : MonoBehaviour
{
    public GameObject selectArchivePanel;
    public Button close;

    // 每个存档中的信息
    public List<ArchivesInteraction> archives;

    // 删除/复制存档内容
    public GameObject deleteArcTips;
    public GameObject copyArcTips;
    public GameObject refuseCopyTips;
    public Button sureDe, cancelDe, sureCo, cancelCo, sureRe;
    public int currentDCA;   // 当前选择删除/复制的存档

    // 场景切换
    public GameObject black;  // 渐黑
    public float fadeOutSpeed = 1f;   // 渐隐速度

    // 调用其他脚本
    public ObjectDataManager odm;

    void Start()
    {
        selectArchivePanel.SetActive(false);
        deleteArcTips.SetActive(false);
        copyArcTips.SetActive(false);
        refuseCopyTips.SetActive(false);
        currentDCA = -1;   // 没有选择删除/复制的存档

        if (close != null)
        {
            close.onClick.AddListener(CloseSelectArcPanel);
        }
        if (sureDe != null)
        {
            sureDe.onClick.AddListener(SureDeleteArc);
        }
        if (cancelDe != null)
        {
            cancelDe.onClick.AddListener(CancelDeleteArc);
        }
        if (sureCo != null)
        {
            sureCo.onClick.AddListener(SureCopyArc);
        }
        if (cancelCo != null)
        {
            cancelCo.onClick.AddListener(CancelCopyArc);
        }
        if (sureRe != null)
        {
            sureRe.onClick.AddListener(SureRefuseCopyTips);
        }
    }

    // 打开选择存档界面
    public void OpenSelectArcPanel()
    {
        InitializeArcInformation();   // 每次打开选择存档界面时刷新存档状态
        selectArchivePanel.SetActive(true);
    }

    // 关闭选择存档界面
    public void CloseSelectArcPanel()
    {
        selectArchivePanel.SetActive(false);
    }

    // 初始化存档信息
    public void InitializeArcInformation()
    {
        foreach (var arc in archives)
        {
            arc.GetArcsInf();

            // 初始化UI信息
            if (arc.isNewGame == true)   // 新存档
            {
                arc.newArc.SetActive(true);
                arc.oldArc.SetActive(false);
                arc.arcImage.gameObject.SetActive(false);
                arc.deleteArc.gameObject.SetActive(false);
                arc.copyArc.gameObject.SetActive(false);
            }
            else if (arc.isNewGame == false)   // 已有存档
            {
                arc.newArc.SetActive(false);
                arc.oldArc.SetActive(true);
                arc.arcImage.gameObject.SetActive(true);
                arc.deleteArc.gameObject.SetActive(true);
                arc.copyArc.gameObject.SetActive(true);
            }

            // 绑定删除/复制存档按键功能
            if (arc.deleteArc != null && arc.isNewGame == false)
            {
                arc.deleteArc.onClick.AddListener(() => DeleteArchive(arc.arcIndex));
            }
            if (arc.copyArc != null && arc.isNewGame == false)
            {
                arc.copyArc.onClick.AddListener(() => CopyArchive(arc.arcIndex));
            }

            // 选择新存档进入游戏
            if (arc.arcButton != null && arc.isNewGame == true)
            {
                arc.arcButton.onClick.RemoveAllListeners();   // 移除原有监听
                arc.arcButton.onClick.AddListener(() => EnterNewArchive(arc.arcIndex));
            }

            // 选择已有存档进入游戏
            if (arc.arcButton != null && arc.isNewGame == false)
            {
                arc.arcButton.onClick.RemoveAllListeners();   // 移除原有监听
                arc.arcButton.onClick.AddListener(() => EnterExistingArchive(arc.arcIndex));
            }
        }
    }

    /// <summary>
    /// 删除存档
    /// </summary>
    /// <param name="index"></param>

    public void DeleteArchive(int index)
    {
        deleteArcTips.SetActive(true);
        currentDCA = index;
    }

    // 确认删除存档
    public void SureDeleteArc()
    {
        ResetDeleteArcInf(currentDCA);
        InitializeArcInformation();
        deleteArcTips.SetActive(false);
    }

    // 取消删除存档
    public void CancelDeleteArc()
    {
        deleteArcTips.SetActive(false);
    }

    // 重置被删除的存档的信息
    public void ResetDeleteArcInf(int index)
    {
        // 重置UI信息
        var arc = archives[index];
        arc.newArc.SetActive(true);
        arc.oldArc.SetActive(false);
        arc.arcImage.gameObject.SetActive(false);
        arc.deleteArc.gameObject.SetActive(false);
        archives[index] = arc;

        ArchiveGameManager.arcm.DeleteArcInf(index);
    }

    /// <summary>
    /// 复制存档
    /// </summary>
    /// <param name="index"></param>

    public void CopyArchive(int index)
    {
        for (int i = 0; i < archives.Count; i++)   
        {
            if (archives[i].isNewGame == true)   // 有空存档
            {
                copyArcTips.SetActive(true);
                currentDCA = index;
                return;   // 退出函数
            }
        }

        // 没有空存档：禁止复制存档
        refuseCopyTips.SetActive(true);
    }

    // 确定复制存档
    public void SureCopyArc()
    {
        ArchiveGameManager.arcm.CopyArcInf(currentDCA);
        InitializeArcInformation();
        copyArcTips.SetActive(false);
    }

    // 取消复制存档
    public void CancelCopyArc()
    {
        copyArcTips.SetActive(false);
    }

    // 确认拒绝复制存档的提示信息
    public void SureRefuseCopyTips()
    {
        refuseCopyTips.SetActive(false);
    }

    /// <summary>
    /// 选择存档
    /// </summary>

    // 选择新存档进入游戏
    public void EnterNewArchive(int index)
    {
        ArchiveGameManager.arcm.currentArcIndex = index;
        odm.DefineNewAmount();   // 赋值：物品初始值
        odm.GiveObjAmount(index);   // 同步存档系统

        DateTime now = DateTime.Now;   // 获取当前本地时间
        StartCoroutine(StartNewGame(index, now));
        MouseManager.mouse.HideMouse();
    }

    // 更新新存档信息
    public void UpdateNewArcInf(int index, DateTime time)
    {
        var arc = archives[index];
        arc.isNewGame = false;
        arc.newArc.SetActive(false);
        arc.oldArc.SetActive(true);
        arc.arcImage.gameObject.SetActive(true);
        arc.arcTime.text = time.Year.ToString() + "-" + time.Month.ToString() + "-" + time.Day.ToString() + "-" + time.Hour.ToString() + ":" + time.Minute.ToString();
        arc.deleteArc.gameObject.SetActive(true);
        arc.copyArc.gameObject.SetActive(true);
        archives[index] = arc;
        archives[index].SendArcsInf();
    }

    // 开始游戏协程
    IEnumerator StartNewGame(int index, DateTime time)
    {
        // 渐隐
        black.SetActive(true);
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeOutSpeed;
            black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        alpha = 1f;
        black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);

        // 更新存档信息，切换场景
        UpdateNewArcInf(index, time);
        SceneManager.LoadScene("1-1");
    }

    // 选择已有存档进入游戏
    public void EnterExistingArchive(int index)
    {
        ArchiveGameManager.arcm.currentArcIndex = index;

        StartCoroutine(StartExistingGame(index));
        MouseManager.mouse.HideMouse();
    }

    // 开始游戏协程
    IEnumerator StartExistingGame(int index)
    {
        // 渐隐
        black.SetActive(true);
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeOutSpeed;
            black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        alpha = 1f;
        black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);

        // 切换场景
        int sceneIndex;
        sceneIndex = ArchiveGameManager.arcm.arcsInf[index].sceneIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}
