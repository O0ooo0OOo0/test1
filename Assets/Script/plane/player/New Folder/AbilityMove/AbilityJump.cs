// JumpAbility.cs - 跳跃能力
using System.Collections;
using UnityEngine;

public class JumpAbility : AbilityBase
{
    [Header("跳跃配置")]
    public float jumpForce = 12f;
    public float jumpHoldTime = 0.2f; // 延长跳时间
    //public bool hasDoubleJumpCard = false;  // 是否拥有二段跳卡牌（由AbilityManager控制）

    [Header("预输入配置")]
    public float jumpBufferTime = 0.15f;  // 预输入窗口时间（秒）

    [Header("冲刺跳配置")]
    public float dashJumpDecayTime = 0.5f;  // 惯性衰减时间
    public float dashJumpMultiplier = 1.5f;  // 冲刺跳速度倍率
    private bool isInDashJumpInertia;

    [Header("地面检测")]
    public float groundCheckDistance = 1.1f;
    public LayerMask groundLayer;

    [Header("重力配置")]
    public float gravityMultiplier = 2f;  // 重力倍数（原代码是2倍）

    private bool isJumpHolding;
    private float jumpHoldTimer;
   // private bool canDoubleJump;  // 二段跳是否可用（落地时重置）
    private bool isOnReboundPlatform;  // 添加反弹平台支持
    private DashAbility dashAbility;  // 添加冲刺能力引用

    // 预输入相关变量
    private float jumpBufferTimer;  // 预输入计时器
    private bool isJumpBuffered;     // 是否有预输入的跳跃

    private Coroutine forceCoroutine;  // 添加到类变量中
    public override string AbilityName => "Jump";
    public bool IsInDashJumpInertia => isInDashJumpInertia;

    protected override void Awake()
    {
        base.Awake();
        dashAbility = GetComponent<DashAbility>();  // 获取冲刺能力组件

    }

    void FixedUpdate()
    {
        // 地面检测
        bool wasGrounded = playerState.isGrounded;
        playerState.isGrounded = Physics2D.Raycast(transform.position, Vector2.down,
            groundCheckDistance, groundLayer);

        // 预输入处理：落地时如果有缓冲的跳跃，立即执行
        if (playerState.isGrounded && !wasGrounded)
        {
            if (isJumpBuffered)
            {
                // 执行缓冲的跳跃
                PerformJump();
                isJumpBuffered = false;
                jumpBufferTimer = 0;
            }
        }

        // 延长跳逻辑
        if (isJumpHolding && playerState.isJumping)
        {
            if (jumpHoldTimer < jumpHoldTime)
            {
                // 原代码: rb.velocity += new Vector2(0, -Physics2D.gravity.y * Time.deltaTime);
                rb.velocity += new Vector2(0, -Physics2D.gravity.y * Time.fixedDeltaTime);
                jumpHoldTimer += Time.fixedDeltaTime;
            }
            else
            {
                EndJumpHold();
            }
        }

        //  重力修改
        if (!playerState.isJumping)
        {
            if (isOnReboundPlatform)
            {
                // 在反弹平台上，什么都不做，让物理引擎自然处理重力
                // rb.velocity 会被物理引擎正常改变
            }
            else
            {
                // 正常情况下的强制重力修改
                // 原代码: rb.velocity -= new Vector2(0, -Physics2D.gravity.y * 2 * Time.deltaTime);
                rb.velocity -= new Vector2(0, -Physics2D.gravity.y * gravityMultiplier * Time.fixedDeltaTime);
            }
        }

        // 更新预输入计时器
        if (isJumpBuffered)
        {
            jumpBufferTimer -= Time.fixedDeltaTime;
            if (jumpBufferTimer <= 0)
            {
                isJumpBuffered = false;
            }
        }
    }

    protected override void CheckInput()
    {
        if (Input.GetKeyDown(activationKey))
        {
            //TryJump();
            // 记录跳跃预输入
            TryBufferJump();
        }

        if (Input.GetKeyUp(activationKey))
        {
            EndJumpHold();
        }
    }

    // 预输入跳跃逻辑
    void TryBufferJump()
    {
        // 冲刺跳：在冲刺状态下的跳跃
        if (dashAbility != null && dashAbility.IsDashing && playerState.isGrounded)
        {
           
            PerformDashJump();
            return;
        }

        // 地面跳跃
        if (playerState.isGrounded)
        {
            PerformJump();
        }

        // 在空中且不能二段跳时，记录预输入
        else
        {
            isJumpBuffered = true;
            jumpBufferTimer = jumpBufferTime;
            //Debug.Log($"预输入记录，将在 {jumpBufferTime} 秒内落地触发");
        }
    }

    void PerformJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        playerState.isJumping = true;
        isJumpHolding = true;
        jumpHoldTimer = 0;

        // 播放音效
        var audioSource = GetComponent<AudioSource>();
        //audioSource?.Play();
    }

    void PerformDashJump()
    {
        isInDashJumpInertia = true;  // 开始惯性状态

        // 1. 获取冲刺速度
        float dashSpeed = dashAbility.GetDashSpeed();
        //float dashSpeed = 30f;

        // 3. 确定方向（优先使用输入方向，否则使用面向方向）
        float direction = playerState.facingDirection;
        if (playerState.horizontalInput != 0)
        {
            direction = Mathf.Sign(playerState.horizontalInput);
        }
        else if (playerState.facingDirection != 0)
        {
            direction = playerState.facingDirection;
        }
        else
        {
            direction = 1; // 默认向右
        }
        float horizontalSpeed = direction * dashSpeed * dashJumpMultiplier ;
        float verticalSpeed = jumpForce * 1.2f;

        // 立即结束冲刺
        dashAbility.EndDash();

        rb.drag = 0.1f;  // 阻尼值，越大减速越快
        //  延迟恢复阻尼（0.5秒后恢复为0）
        Invoke(nameof(ResetDashJumpDrag), dashJumpDecayTime);


        // 执行冲刺跳
        rb.velocity = new Vector2( horizontalSpeed, verticalSpeed);  // 垂直方向也增强

        playerState.isJumping = true;
        isJumpHolding = true;
        jumpHoldTimer = 0;

        dashAbility.EndDash();
        // 7. 开始持续施加水平力（随衰减时间慢慢减弱）
        StartCoroutine(ApplyDashJumpForce(horizontalSpeed, dashJumpDecayTime));
        // 5. 结束冲刺
        //dashAbility.EndDash();
        


    }

    IEnumerator ApplyDashJumpForce(float initialSpeed, float decayTime)
    {
        float elapsedTime = 0f;
        float currentForce = initialSpeed;
        float targetForce = Mathf.Sign(initialSpeed) * 8f;  // 最终衰减到的速度（普通移动速度）

        while (elapsedTime < decayTime && isInDashJumpInertia)
        {

            //elapsedTime += Time.deltaTime;

            // 计算当前应该施加的力（线性衰减）
            //float t = elapsedTime / decayTime;
            //currentForce = Mathf.Lerp(initialSpeed, targetForce, t);

            // 非线性衰减：使用 SmoothStep 曲线（先快后慢）
            float t = elapsedTime / decayTime;

            // 方法2：使用指数衰减（更自然的物理感）
             float exponent = 1 - Mathf.Pow(1 - t, 2);  // 二次曲线
            currentForce = Mathf.Lerp(initialSpeed, targetForce, exponent);

            // 持续施加水平力（保持水平速度）
            rb.velocity = new Vector2(currentForce, rb.velocity.y);

            yield return null;
        }

        // 确保最终速度
        rb.velocity = new Vector2(targetForce, rb.velocity.y);

        // 惯性结束
        isInDashJumpInertia = false;
        //Debug.Log($"冲刺跳力衰减结束，最终速度: {rb.velocity}");
    }
    // 使用 Unity 自带的碰撞检测（最简单）
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查是否碰到 ground 图层的物体
        if (IsInLayerMask(collision.gameObject, groundLayer))
        {
            // 如果是冲刺跳惯性状态，立即停止
            if (isInDashJumpInertia)
            {
                // 停止协程
                if (forceCoroutine != null)
                {
                    StopCoroutine(forceCoroutine);
                    forceCoroutine = null;
                }

                // 清除惯性状态
                isInDashJumpInertia = false;

                // 可选：保留一些速度，不会完全停止
                // rb.velocity = new Vector2(rb.velocity.x * 0.5f, rb.velocity.y);

                //Debug.Log($"碰到地面/墙壁，冲刺跳惯性停止");
            }
        }
    }
    // 辅助方法
    bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << obj.layer)) != 0);
    }

    // 恢复阻尼的方法
    void ResetDashJumpDrag()
    {
        rb.drag = 0f;
        isInDashJumpInertia = false;
    }

    void EndJumpHold()
    {
        isJumpHolding = false;
        playerState.isJumping = false;
        jumpHoldTimer = 0;
    }
}