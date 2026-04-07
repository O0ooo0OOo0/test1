using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBig : MonoBehaviour
{
    public int mapIndex;   // 地图编号
    public GameObject dirtyMap, clearMap;   // 污染地图， 净化地图
    public GameObject scope;   // 地图区域范围
    public bool isUnlock;    // 当前地图板块是否解锁
    public int mapStatus;     // 当前地图板块状态：污染（0）或净化（1）

    // 地图信息
    public void BigMapStatus()
    {
        GetBigMapStatus();
        ShowBigMapStatus();
    }

    // 获取地图板块的当前状态
    public void GetBigMapStatus()
    {
        isUnlock = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].maps[mapIndex].isMapUnclock;
        mapStatus = ArchiveGameManager.arcm.arcsInf[ArchiveGameManager.arcm.currentArcIndex].maps[mapIndex].mapStatus;
    }

    // 显示地图板块的当前状态
    public void ShowBigMapStatus()
    {
        if (isUnlock == false)   // 未解锁
        {
            scope.SetActive(false);
            dirtyMap.SetActive(false);
            clearMap.SetActive(false);
        }
        else if (isUnlock == true)   // 已解锁
        {
            scope.SetActive(true);

            if (mapStatus == 0)
            {
                dirtyMap.SetActive(true);
                clearMap.SetActive(false);
            }
            else if (mapStatus == 1)
            {
                dirtyMap.SetActive(false);
                clearMap.SetActive(true);
            }
        }
    }
}
