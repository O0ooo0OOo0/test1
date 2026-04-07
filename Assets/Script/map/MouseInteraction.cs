using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool isMouseEnter;   // 检测鼠标进入
    public bool isMouseClick;   // 检测鼠标点击

    private void Start()
    {
        isMouseEnter = false;
        isMouseClick = false;
    }

    // 鼠标进入事件
    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseEnter = true;
    }

    // 鼠标离开事件
    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseEnter = false;
    }

    // 鼠标点击事件
    public void OnPointerClick(PointerEventData eventData)
    {
        isMouseEnter = false;   // 重置鼠标是否进入
        isMouseClick = true;
    }
}
