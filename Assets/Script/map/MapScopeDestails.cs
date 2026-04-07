using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScopeDestails : MonoBehaviour
{
    public GameObject mapForBig;   // 地图板块对应的大地图信息

    // 地图板块信息
    public GameObject dirtyMapScope, clearMapScope;
    public int mapStatus;   // 地图污染/净化状态

    void Start()
    {
        mapStatus = mapForBig.GetComponent<MapBig>().mapStatus;
        ShowMapScopeStatus();
    }

    // 根据状态显示地图板块
    public void ShowMapScopeStatus()
    {
        if (mapStatus == 0)   // 污染
        {
            dirtyMapScope.SetActive(true);
            clearMapScope.SetActive(false);
        }
        else if (mapStatus == 1)   // 净化
        {
            dirtyMapScope.SetActive(false);
            clearMapScope.SetActive(true);
        }
    }
}
