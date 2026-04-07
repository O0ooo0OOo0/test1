using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public KeyCode dashKey = KeyCode.Z;           // 冲刺按键
    public float dashSpeed = 15f;                  // 冲刺速度
    public float dashDuration = 0.2f;               // 冲刺持续时间
    public float dashCooldown = 0.5f;                // 冲刺冷却时间
    public LayerMask groundLayer;

    [SerializeField] private bool isDashing = false;     // 是否正在冲刺
    [SerializeField] private bool canAirDash = true;     // 空中是否还可冲刺
    [SerializeField] private float dashTimer = 0f;       // 冲刺剩余时间
    [SerializeField] private float dashCooldownTimer = 0f; // 冷却剩余时间

    private Rigidbody2D rb;
    private Vector2 dashDir;                       // 当前冲刺方向
    private int facingDir = 1;                      // 玩家最近一次有效水平输入方向（1=右，-1=左）

    public bool IsDashing => isDashing;    // 公共属性，供其他脚本（如移动脚本）判断冲刺状态

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dash();
        }


    void dash()
        {
        // 地面检测
        bool grounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        if (grounded) canAirDash = true;

      // 输入检测
        if (Input.GetKeyDown(dashKey) && !isDashing && dashCooldownTimer <= 0)
        {
            float h = Input.GetAxisRaw("Horizontal");
            if (h != 0) facingDir = (int)Mathf.Sign(h);
            float v = Input.GetAxisRaw("Vertical");

            // 确定冲刺方向 优先级：下 > 当前水平输入 > 记录的面向方向）
            if (v < 0) 
                dashDir = Vector2.down;
            else if (h != 0) 
                dashDir = new Vector2(Mathf.Sign(h), 0);
            else 
                dashDir = new Vector2(transform.localScale.x > 0 ? 1 : -1, 0);

            // 检查冲刺许可（地面或空中且未使用）
            if (grounded || canAirDash)
            {
                isDashing = true;
                dashTimer = dashDuration;
                if (!grounded) canAirDash = false; // 空中冲刺后禁用
            }
        }

        // 冷却计时
        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;

        // 冲刺计时 
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                // 冲刺结束，进入冷却
                isDashing = false;
                dashCooldownTimer = dashCooldown;
            }

            //应用冲刺速度
            if (dashDir == Vector2.down)
                rb.velocity = new Vector2(rb.velocity.x, -dashSpeed);
            // 向下冲刺：水平速度保留，垂直设为负冲刺速度
            else
                rb.velocity = new Vector2(dashDir.x * dashSpeed, 0);
            // 水平冲刺：水平速度设为冲刺方向 * 冲刺速度，垂直速度保持不变（与跳跃/重力兼容）
        }
    }

 

    
}
