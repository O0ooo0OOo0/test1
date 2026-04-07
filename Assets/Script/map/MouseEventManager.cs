using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEventManager : MonoBehaviour
{
    public GameObject[] scopes;   // 范围边界集合
    public GameObject sign;   // 鼠标进入该范围集合的标志
    public bool isEnter;  // 鼠标是否进入该范围

    // 调用其他脚本
    public MapManager mapManager;
    public MapBig bigmap;

    private void Start()
    {
        isEnter = false;
        sign.SetActive(false);
    }

    private void Update()
    {
        if (isEnter == false)   // 鼠标没进入时检测是否进入（已进入后不再检测）
        {
            MouseIsEnter();
        }
        else if (isEnter == true)   // 鼠标已进入后检测是否离开（离开后不再检测）,鼠标已进入后检测是否产生点击
        {
            MouseIsExit();
            ClickScope();
        }
    }

    // 检测鼠标是否进入任一范围
    public void MouseIsEnter()
    {
        for (int i = 0; i < scopes.Length; i++)
        {
            if (scopes[i].GetComponent<MouseInteraction>().isMouseEnter == true)   // 鼠标在任何一个范围内
            {
                isEnter = true;   // 鼠标进入
                sign.SetActive(true);   // 显示标志

                return;   // 终止函数
            }
        }
    }

    // 检测鼠标是否离开所有范围
    public void MouseIsExit()
    {
        for (int i = 0; i < scopes.Length; i++)
        {
            if (scopes[i].GetComponent<MouseInteraction>().isMouseEnter == true)   // 鼠标在任何一个范围内
            {
                return;   // 终止函数
            }
        }

        // 鼠标不在任一范围内时执行
        isEnter = false;   // 鼠标未进入
        sign.SetActive(false);   // 隐藏标志
    }

    // 鼠标点击
    public void ClickScope()
    {
        for (int i = 0; i < scopes.Length; i++)
        {
            if (scopes[i].GetComponent<MouseInteraction>().isMouseClick == true)   // 鼠标在任何一个范围内发生点击
            {
                isEnter = false;   // 重置鼠标状态
                sign.SetActive(false);    // 重置标志状态

                scopes[i].GetComponent<MouseInteraction>().isMouseClick = false;   // 重置鼠标是否点击
                mapManager.MapScopeDes(bigmap.mapIndex);   // 根据编号打开对应小地图

                return;   // 终止函数
            }
        }
    }
}
