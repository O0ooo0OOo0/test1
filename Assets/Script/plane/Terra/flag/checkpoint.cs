using UnityEngine;


//存档点保留复活坐标

public class checkpoint : MonoBehaviour
{
    [SerializeField] private KeyCode interactKey = KeyCode.F;// 交互按键，默认设为F键
    private InteractionPromptUI promptUI;// 交互提示UI组件
    private Life playerLife;  // 玩家生命/状态组件
    private bool playerInRange = false;  // 玩家是否在存档点范围内
    private bool isActivated = false;  // 存档点是否已被激活


    void Start()
    {
        // 获取挂载在同一物体上的InteractionPromptUI组件
        promptUI = GetComponent<InteractionPromptUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // 当玩家在范围内、存档点未激活、且按下交互键时，激活存档点
        if (playerInRange && !isActivated && Input.GetKeyDown(interactKey))
        {
            ActivateCheckpoint();
        }

        // 如果UI组件存在、玩家在范围内、且存档点未激活，则更新UI位置跟随存档点
        if (promptUI != null && playerInRange)
        {
            promptUI.UpdatePosition(transform.position);
        }
    }

    // 当其他碰撞体进入触发器范围时触发
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查进入的是否是玩家，且存档点未被激活
        if (collision.CompareTag("Player") && !isActivated)
        {
            playerInRange = true;  // 标记玩家进入范围
            playerLife = collision.GetComponent<Life>();  // 获取玩家的Life组件
            promptUI?.Show();  // 显示交互提示UI（如果UI组件存在）
        }
    }

    // 当其他碰撞体离开触发器范围时触发
    private void OnTriggerExit2D(Collider2D collision)
    {
        // 检查离开的是否是玩家
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;  // 标记玩家离开范围
            playerLife = null;  // 清空玩家组件引用
            promptUI?.Hide();  // 隐藏交互提示UI（如果UI组件存在）
        }
    }

    // 激活存档点
    private void ActivateCheckpoint()
    {
        if (playerLife == null) return;  // 安全检查：确保玩家组件存在

        playerLife.pos = transform.position;  // 将玩家的重生点设置为存档点位置
        isActivated = true;  // 标记存档点为已激活状态
        promptUI?.Hide();   // 立即隐藏交互提示UI（无动画）
        Debug.Log($"检查点已激活: {gameObject.name}");  // 输出激活日志
    }
}
