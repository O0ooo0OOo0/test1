using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class AutoScrollText : MonoBehaviour
{
    public RectTransform contentRectTransform; // 文字内容的RectTransform
    public ScrollRect scrollRect; // 滚动视图组件
    public TMP_Text textMeshPro; // TMP_Text组件

    private RectTransform contentRectTransformCache;
    private ScrollRect scrollRectCache;
    private TMP_Text textMeshProCache;

    private bool isPlayerScrolling = false; // 玩家是否正在滚动
    private string previousText; // 上一次的文本内容

    private void Start()
    {
        contentRectTransformCache = contentRectTransform;
        scrollRectCache = scrollRect;
        textMeshProCache = textMeshPro;

        // 初始化上一次的文本内容
        previousText = textMeshProCache.text;

        // 添加滚动事件监听
        if (scrollRectCache != null)
        {
            scrollRectCache.onValueChanged.AddListener(OnScrollValueChanged);
        }
    }

    private void Update()
    {
        // 检测文本内容是否更新
        if (textMeshProCache != null && textMeshProCache.text != "" && isPlayerScrolling == false)
        {
            // 自动滚动到文字底部
            ScrollToBottom();
        }

        if (textMeshProCache.text != previousText)
        {
            previousText = textMeshProCache.text;
            StartCoroutine(Delay(0.7f));
            //isPlayerScrolling = false;
        }
    }

    private void ScrollToBottom()
    {
        if (contentRectTransformCache == null || scrollRectCache == null) return;

        // 计算滚动视图需要滚动的距离
        float scrollPosition = contentRectTransformCache.sizeDelta.y - scrollRectCache.viewport.rect.height;

        // 设置滚动视图的垂直滚动位置
        scrollRectCache.verticalNormalizedPosition = 0f; // 0f 表示滚动到最底部
    }

    // 滚动事件回调
    private void OnScrollValueChanged(Vector2 scrollPosition)
    {
        // 玩家滚动时，停止自动滚动
        isPlayerScrolling = true;
    }

    private void OnDestroy()
    {
        // 移除滚动事件监听
        if (scrollRectCache != null)
        {
            scrollRectCache.onValueChanged.RemoveListener(OnScrollValueChanged);
        }
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isPlayerScrolling = false;
    }
}