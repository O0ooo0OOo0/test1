using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScaleController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IScrollHandler
{
    [SerializeField] private float hoverScale = 1f; // 鼠标悬停时的放大比例
    [SerializeField] private float normalScale = 0.9f;  // 正常大小比例

    public ScrollRect scrollRect; 

    private void Awake()
    {
        scrollRect = GetComponentInParent<ScrollRect>();
    }

    // 鼠标悬停时放大
    public void OnPointerEnter(PointerEventData eventData)
    {
        SetScaleTo(hoverScale);
    }

    // 鼠标离开时恢复大小
    public void OnPointerExit(PointerEventData eventData)
    {
        SetScaleTo(normalScale);
    }

    // 滚动事件处理
    public void OnScroll(PointerEventData eventData)
    {
        // 如果存在 ScrollRect，则将滚动事件传递给它
        if (scrollRect != null)
        {
            scrollRect.OnScroll(eventData);
        }
    }

    // 设置缩放比例的方法
    public void SetScaleTo(float scale)
    {
        transform.localScale = Vector3.one * scale;
    }
}