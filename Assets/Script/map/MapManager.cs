using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public GameObject mapPanel;
    public GameObject mapBig;
    public GameObject mapSmall;
    public Button closeMap;
    public Button closeMapScope;
    public bool isCanMap;
    public bool isOpenMap;

    // 地图板块细节对应界面
    public GameObject[] mapScopes;

    // 调用其他脚本
    public MapBig[] mapBigs;

    void Start()
    {
        // 界面初始化
        mapPanel.SetActive(false);

        foreach (GameObject scope in mapScopes)
        {
            scope.SetActive(false);
        }

        isOpenMap = false;

        if (closeMap != null)
        {
            closeMap.onClick.AddListener(CloseMap);
        }
        if (closeMapScope != null)
        {
            closeMapScope.onClick.AddListener(CloseMapScope);
        }
    }

    void Update()
    {
        if (isCanMap && Input.GetKeyDown(GameKeyManager.gkm.map))
        {
            if (isOpenMap == false)
            {
                OpenMap();
            }
            else if (isOpenMap == true)
            {
                CloseMap();
            }
        }
        if (isCanMap && isOpenMap && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMap();
        }
    }

    // 打开地图
    public void OpenMap()
    {
        isOpenMap = true;
        InputManager.im.EnableMap();
        MouseManager.mouse.ShowMouse();

        mapPanel.SetActive(true);
        mapBig.SetActive(true);
        for (int i = 0; i < mapBigs.Length; i++)
        {
            mapBigs[i].BigMapStatus();
        }
    }

    // 关闭地图
    public void CloseMap()
    {
        mapSmall.SetActive(false);
        mapPanel.SetActive(false);
        isOpenMap = false;
        InputManager.im.EnableKeysInput();
        MouseManager.mouse.HideMouse();
    }

    // 显示选中的地图板块详情
    public void MapScopeDes(int m)
    {
        mapBig.SetActive(false);
        mapSmall.SetActive(true);

        for (int i = 0; i < mapScopes.Length; i++)
        {
            if (i == m)   // 选中的地图细节界面出现
            {
                mapScopes[i].SetActive(true);
            }
            else   // 其他隐藏
            {
                mapScopes[i].SetActive(false);
            }
        }
    }

    // 关闭地图板块详情
    public void CloseMapScope()
    {
        mapSmall.SetActive(false);
        mapBig.SetActive(true);
    }
}
