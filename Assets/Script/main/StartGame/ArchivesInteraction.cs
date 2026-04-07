using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class ArchivesInteraction : MonoBehaviour
{
    public int arcIndex;
    public Button arcButton;
    public Button deleteArc;   // 删除存档
    public Button copyArc;   // 复制存档
    public GameObject newArc;   // 新存档
    public GameObject oldArc;   // 已存在存档

    // 需传递的信息
    public bool isNewGame;   
    public Image arcImage;   
    public TMP_Text arcName;   
    public TMP_Text arcTime;   

    // 获取存档选择界面所需信息
    public void GetArcsInf()
    {
        var arc = ArchiveGameManager.arcm.arcsInf[arcIndex];
        isNewGame = arc.isNewGame;
        arcImage.sprite = arc.arcImage;
        arcName.text = arc.arcName;
        arcTime.text = arc.arcTime;
    }

    // 返回存档界面的存档信息
    public void SendArcsInf()
    {
        var arc = ArchiveGameManager.arcm.arcsInf[arcIndex];
        arc.isNewGame = isNewGame;
        arc.arcImage = arcImage.sprite;
        arc.arcName = arcName.text;
        arc.arcTime = arcTime.text;
        ArchiveGameManager.arcm.arcsInf[arcIndex] = arc;
    }
}
