// NPCInteraction.cs - 简化的NPC交互脚本
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [Header("NPC设置")]
    [SerializeField] private string npcName = "NPC";
    [SerializeField] private float interactionRadius = 3f;

    [Header("对话状态")]
    [SerializeField] private bool hasTalkedBefore = false;
    [SerializeField] private string firstDialogId = "dialog_1";
    [SerializeField] private string repeatDialogId = "dialog_1_repeat";

    [Header("交互提示UI")]
    [SerializeField] private InteractionPromptUI promptUI;  // 引用UI组件

    private Transform player;
    private DialogManager dialogManager;
    private bool playerInRange = false;

    void Start()
    {
        // 查找玩家
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // 获取DialogManager
        dialogManager = FindObjectOfType<DialogManager>();

        // 如果没有手动指定，尝试获取子物体上的UI组件
        if (promptUI == null)
            promptUI = GetComponentInChildren<InteractionPromptUI>();
    }

    void Update()
    {
        if (player == null || dialogManager == null) return;

        // 计算距离
        float distance = Vector2.Distance(transform.position, player.position);
        playerInRange = distance <= interactionRadius;

        // 更新UI显示
        UpdateUI();

        // F键交互
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && !dialogManager.IsDialogActive)
        {
            Interact();
        }
    }

    void UpdateUI()
    {
        if (promptUI == null) return;

        if (playerInRange && !dialogManager.IsDialogActive)
        {
            // 显示提示UI
            promptUI.UpdatePosition(transform.position);
            promptUI.Show();
        }
        else
        {
            // 隐藏提示UI
            promptUI.Hide();
        }
    }

    void Interact()
    {
        // 根据是否对话过选择不同的对话ID
        string dialogId = hasTalkedBefore ? repeatDialogId : firstDialogId;
        dialogManager.StartDialog(dialogId);

        // 标记为已经对话过
        hasTalkedBefore = true;

        // 交互时立即隐藏提示UI
        if (promptUI != null)
            promptUI.HideImmediate();
    }

    // 可视化交互范围（在Scene视图中显示）
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}