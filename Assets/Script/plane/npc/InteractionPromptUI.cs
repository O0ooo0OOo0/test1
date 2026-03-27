// InteractionPromptUI.cs - 交互提示UI组件
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    [Header("UI提示")]
    [SerializeField] private RectTransform interactPrompt;      // 交互提示UI
    [SerializeField] private Vector3 promptOffset = new Vector3(0, 2f, 0); // UI偏移

    [Header("动画设置")]
    [SerializeField] private float fadeInDuration = 0.3f;      // 淡入时间
    [SerializeField] private float fadeOutDuration = 0.5f;     // 淡出时间

    private Camera mainCamera;
    private Canvas parentCanvas;
    private CanvasGroup canvasGroup;
    private bool isShowing = false;

    void Start()
    {
        InitializeUI();
    }

    // 初始化UI
    private void InitializeUI()
    {
        if (interactPrompt == null) return;

        mainCamera = Camera.main;
        parentCanvas = interactPrompt.GetComponentInParent<Canvas>();
        if (parentCanvas == null) return;

        // 获取或添加CanvasGroup组件
        canvasGroup = interactPrompt.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = interactPrompt.gameObject.AddComponent<CanvasGroup>();

        // 初始状态：透明且隐藏
        canvasGroup.alpha = 0f;
        interactPrompt.gameObject.SetActive(false);
    }

    // 更新UI位置（每帧调用）
    public void UpdatePosition(Vector3 worldPosition)
    {
        if (!isShowing && !interactPrompt.gameObject.activeSelf) return;
        if (interactPrompt == null || mainCamera == null || parentCanvas == null) return;

        // 将世界坐标转换为屏幕坐标
        Vector3 worldPos = worldPosition + promptOffset;
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);

        // 检查物体是否在相机前方
        if (screenPos.z < 0)
        {
            HideImmediate();
            return;
        }

        // 转换为Canvas坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            screenPos,
            null,  // Overlay模式不需要相机
            out Vector2 canvasPos
        );

        interactPrompt.anchoredPosition = canvasPos;
    }

    // 显示UI（带淡入效果）
    public void Show()
    {
        if (isShowing) return;

        isShowing = true;
        if (!interactPrompt.gameObject.activeSelf)
            interactPrompt.gameObject.SetActive(true);

        // 开始淡入协程
        StopAllCoroutines();
        StartCoroutine(FadeRoutine(0f, 1f, fadeInDuration));
    }

    // 隐藏UI（带淡出效果）
    public void Hide()
    {
        if (!isShowing) return;

        isShowing = false;

        // 开始淡出协程
        StopAllCoroutines();
        StartCoroutine(FadeRoutine(canvasGroup.alpha, 0f, fadeOutDuration, () =>
        {
            if (canvasGroup.alpha <= 0f && interactPrompt.gameObject.activeSelf)
            {
                interactPrompt.gameObject.SetActive(false);
            }
        }));
    }

    // 立即隐藏（无动画）
    public void HideImmediate()
    {
        isShowing = false;
        StopAllCoroutines();
        canvasGroup.alpha = 0f;
        if (interactPrompt.gameObject.activeSelf)
            interactPrompt.gameObject.SetActive(false);
    }

    // 淡入淡出协程
    private System.Collections.IEnumerator FadeRoutine(float startAlpha, float endAlpha, float duration, System.Action onComplete = null)
    {
        float elapsed = 0f;
        canvasGroup.alpha = startAlpha;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
        onComplete?.Invoke();
    }

    // 检查UI是否正在显示
    public bool IsShowing => isShowing;
}