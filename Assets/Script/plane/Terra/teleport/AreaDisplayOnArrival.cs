using UnityEngine;
using System.Collections;
using TMPro;  // 如果是TextMeshPro

public class AreaDisplayOnArrival : MonoBehaviour
{
    [Header("区域名称")]
    [SerializeField] private string areaName = "新区域";  // 要显示的地名

    [Header("UI提示")]
    [SerializeField] private GameObject areaNamePanel;    // 地名显示面板
    [SerializeField] private float displayDuration = 2f;   // 显示时间
    [SerializeField] private float fadeDuration = 0.5f;    // 淡入淡出时间

    [Header("触发设置")]
    [SerializeField] private bool showOnStart = false;     // 游戏开始时显示（用于初始区域）
    [SerializeField] private bool showOnlyOnce = true;     // 是否只显示一次

    private CanvasGroup canvasGroup;
    private TextMeshProUGUI areaText;
    private bool hasShown = false;  // 记录是否已经显示过

    private void Start()
    {
        // 初始化地名显示UI
        InitializeUI();

        // 如果设置了游戏开始时显示
        if (showOnStart)
        {
            StartCoroutine(ShowAreaName());
        }
    }

    private void InitializeUI()
    {
        if (areaNamePanel == null)
        {
            Debug.LogError("请指定 areaNamePanel！");
            return;
        }

        // 获取或添加CanvasGroup用于淡入淡出
        canvasGroup = areaNamePanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = areaNamePanel.gameObject.AddComponent<CanvasGroup>();

        // 获取Text组件
        areaText = areaNamePanel.GetComponentInChildren<TextMeshProUGUI>();

        // 初始状态：透明
        canvasGroup.alpha = 0f;

        // 确保面板是激活的（这样才能显示）
        areaNamePanel.SetActive(true);

        Debug.Log("UI初始化完成，面板保持激活但透明");
    }

    // 玩家进入触发区域时显示地名
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"触发！碰到: {other.name}, Tag: {other.tag}");

        if (other.CompareTag("Player"))
        {
            Debug.Log("玩家进入，准备显示地名");

            // 如果设置了只显示一次且已经显示过，就不再显示
            if (showOnlyOnce && hasShown)
            {
                Debug.Log("已经显示过，跳过");
                return;
            }

            StartCoroutine(ShowAreaName());
        }
    }

    // 显示地名的协程
    private IEnumerator ShowAreaName()
    {
        Debug.Log("ShowAreaName 开始执行");


        // === 加1秒延迟 ===
        yield return new WaitForSeconds(1f);

        hasShown = true;

        if (areaNamePanel == null)
        {
            Debug.LogError("areaNamePanel 为 null！");
            yield break;
        }

        Debug.Log($"areaNamePanel 当前状态: {areaNamePanel.activeSelf}");

        // 确保面板是激活的
        if (!areaNamePanel.activeSelf)
        {
            Debug.Log("面板未激活，强制激活");
            areaNamePanel.SetActive(true);
        }

        // 确保父Canvas是激活的
        Canvas parentCanvas = areaNamePanel.GetComponentInParent<Canvas>();
        if (parentCanvas != null && !parentCanvas.gameObject.activeSelf)
        {
            Debug.Log("父Canvas未激活，强制激活");
            parentCanvas.gameObject.SetActive(true);
        }

        // 设置地名文本
        if (areaText != null)
        {
            areaText.text = areaName;
            Debug.Log($"文本设置为: {areaName}");
        }
        else
        {
            // 如果areaText为空，重新获取
            areaText = areaNamePanel.GetComponentInChildren<TextMeshProUGUI>();
            if (areaText != null)
                areaText.text = areaName;
            else
                Debug.LogError("找不到 TextMeshProUGUI 组件！");
        }

        Debug.Log($"面板激活状态: {areaNamePanel.activeSelf}");

        // 淡入
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = elapsedTime / fadeDuration;
            Debug.Log($"淡入 alpha: {canvasGroup.alpha}");
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // 停留
        yield return new WaitForSeconds(displayDuration);

        // 淡出
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = 1f - (elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0f;

        // 隐藏（其实保持透明就行，不用真的禁用）
        // areaNamePanel.SetActive(false);  // 注释掉这行，保持激活但透明
        Debug.Log("地名显示结束");
    }

    // 公共方法：可以从其他脚本手动调用显示地名
    public void ShowArea()
    {
        StartCoroutine(ShowAreaName());
    }
}