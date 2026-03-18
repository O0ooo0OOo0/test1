using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [Header("NPC设置")]
    [SerializeField] private string npcName = "NPC";           // NPC名字
    [SerializeField] private float interactionRadius = 3f;      // 交互范围

    [Header("UI提示")]
    [SerializeField] private RectTransform interactPrompt;        // 交互提示UI
    [SerializeField] private Vector3 promptOffset = new Vector3(0, 2f, 0);//F偏移

    [SerializeField] private float fadeInDuration = 0.3f;     // 淡入时间
    [SerializeField] private float fadeOutDuration = 0.5f;    // 淡出时间
    private bool wasInRange = false;

    [Header("对话状态")]
    [SerializeField] private bool hasTalkedBefore = false;     // 是否已经对话过
    [SerializeField] private string firstDialogId = "dialog_1";      // 第一次对话ID
    [SerializeField] private string repeatDialogId = "dialog_1_repeat"; // 重复对话ID

    private Transform player;
    private bool playerInRange = false;
    
    private Camera mainCamera;
    private Canvas parentCanvas;
    private CanvasGroup canvasGroup;  // 用于控制透明度


    // 获取DialogManager的快捷方式
    private DialogManager dialogManager;

    private void Start()
    {
        // 查找玩家
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        // 获取DialogManager
        dialogManager = FindObjectOfType<DialogManager>();
        mainCamera = Camera.main;

        InitializeUI();

    }

    // 初始化UI
    private void InitializeUI()
    {
        if (interactPrompt == null) return;

        // 获取父Canvas（如果提示UI是Canvas下的子物体）
        parentCanvas = interactPrompt.GetComponentInParent<Canvas>();
        if (parentCanvas == null)return;

        // 获取或添加CanvasGroup组件（用于控制透明度）
        canvasGroup = interactPrompt.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = interactPrompt.gameObject.AddComponent<CanvasGroup>();

        // 初始状态：透明且隐藏
        canvasGroup.alpha = 0f;
        // 确保UI初始状态是隐藏的
        interactPrompt.gameObject.SetActive(false);
    }

        private void Update()
    {
           if (player == null || 
            dialogManager == null || 
            interactPrompt == null || 
            mainCamera == null || 
            parentCanvas == null) 
            return;

        // 计算距离
        float distance = Vector2.Distance(transform.position, player.position);
        playerInRange = distance <= interactionRadius;

        // 处理提示UI显示和位置
        UpdatePromptUI();

        // F键交互
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && !dialogManager.IsDialogActive)
        {
            // 根据是否对话过选择不同的对话ID
            string dialogId = hasTalkedBefore ? repeatDialogId : firstDialogId;
            dialogManager.StartDialog(dialogId);

            // 标记为已经对话过
            hasTalkedBefore = true;
        }

        // 记录当前状态，供下一帧比较
        wasInRange = playerInRange;
    }
    private void UpdatePromptUI()
    {
        // 先处理位置更新（只要有可能显示，就更新位置）
        if (canvasGroup.alpha > 0f || playerInRange)
        {
            UpdateUIPosition();
        }

        // 处理渐入渐出
        HandleFade();
    }

    private void UpdateUIPosition()
    {
        // 只有UI激活时才更新位置（节省性能）
        if (!interactPrompt.gameObject.activeSelf) return;

        // 将NPC的世界坐标加上偏移，转换为屏幕坐标
        Vector3 worldPos = transform.position + promptOffset;
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);

            // 检查物体是否在相机前方（避免物体在相机后面时也显示UI）
            if (screenPos.z < 0)
            {
                interactPrompt.gameObject.SetActive(false);
                return;
            }

            // 【关键修改】Overlay模式下，第三个参数传null
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentCanvas.transform as RectTransform,
                screenPos,
                null,  // Overlay模式不需要相机！
                out Vector2 canvasPos
            );


            // 设置UI在Canvas中的位置
            interactPrompt.anchoredPosition = canvasPos;
        
    }

    private void HandleFade()
    {
        // 判断应该显示还是隐藏
        bool shouldShow = playerInRange && !dialogManager.IsDialogActive;

        if (shouldShow)
        {
            // 进入范围：淡入
            if (!interactPrompt.gameObject.activeSelf)
                interactPrompt.gameObject.SetActive(true);
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, 1f, Time.deltaTime / fadeInDuration);
        }
        else
        {
            // 淡出 只有在UI激活的状态下才执行淡出
            if (interactPrompt.gameObject.activeSelf)
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, 0f, Time.deltaTime / fadeOutDuration);

                // 完全透明后隐藏
                if (canvasGroup.alpha <= 0f && interactPrompt.gameObject.activeSelf)
                {
                    interactPrompt.gameObject.SetActive(false);
                }
            }
        }
    }


    // 可视化交互范围（在Scene视图中显示）
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
