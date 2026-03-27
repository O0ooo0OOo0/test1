// DashAbility.cs - 冲刺能力
using UnityEngine;

public class DashAbility : AbilityBase
{
    [Header("冲刺配置")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public bool allowAirDash = true;
    public float dashAcceleration = 500f;  // 冲刺加速度

    private bool isDashing;
    private float dashTimer;
    private Vector2 dashDirection;
    private float currentDashSpeed;  // 记录当前冲刺速度
    private float originalGravityScale;  // ✅ 保存原始重力

    public override string AbilityName => "Dash";
    public bool IsDashing => isDashing;

    protected override void Awake()
    {
        base.Awake();
        originalGravityScale = rb.gravityScale;  // ✅ 保存原始重力
    }

    protected override void Update()
    {
        base.Update(); // 处理输入和冷却

        if (isDashing)
        {
            UpdateDash();
        }
    }

    // ✅ 获取冲刺速度（用于冲刺跳）
    public float GetDashSpeed()
    {
        return currentDashSpeed > 0 ? currentDashSpeed : dashSpeed;
    }

    void UpdateDash()
    {
        dashTimer -= Time.deltaTime;

        if (dashTimer <= 0)
        {
            EndDash();
        }
        else
        {
            // ✅ 冲刺时设置重力为0
            rb.gravityScale = 0f;



            // ✅ 关键：如果是水平冲刺，锁定垂直速度为0
            if (dashDirection != Vector2.down)
            {            // ✅ 改为叠加速度，而不是覆盖
                float targetSpeed = dashDirection.x * dashSpeed;
                float newVelocityX = Mathf.MoveTowards(rb.velocity.x, targetSpeed, dashAcceleration * Time.deltaTime);
             
                rb.velocity = new Vector2(newVelocityX, 0f);
                currentDashSpeed = dashSpeed;
            }
            else // 向下冲刺
            {
                float targetSpeed = -dashSpeed;
                float newVelocityY = Mathf.MoveTowards(rb.velocity.y, targetSpeed, dashAcceleration * Time.deltaTime);
                rb.velocity = new Vector2(rb.velocity.x, newVelocityY);
                currentDashSpeed = dashSpeed;
            }

            currentDashSpeed = dashSpeed;

            rb.gravityScale = 0f;// ✅ 冲刺时：设置重力为0，不受重力影响
        }
    }


    public override void Use()
    {
        if (!CanUse) return;

        // 检查冲刺条件
        bool canDash = playerState.isGrounded || (allowAirDash && !playerState.isGrounded);
        if (!canDash) return;

        base.Use();

        // 确定冲刺方向
        if (playerState.verticalInput < 0)
            dashDirection = Vector2.down;
        else if (playerState.horizontalInput != 0)
            dashDirection = new Vector2(Mathf.Sign(playerState.horizontalInput), 0);
        else
            dashDirection = playerState.GetFacingDirection();

        isDashing = true;
        dashTimer = dashDuration;
        playerState.isDashing = true;
        currentDashSpeed = dashSpeed;  // ✅ 设置当前冲刺速度
    }

    public void EndDash()
    {
        isDashing = false;
        playerState.isDashing = false;
        currentDashSpeed = 0;  // ✅ 重置冲刺速度
        // ✅ 冲刺结束：恢复重力
            rb.gravityScale = originalGravityScale;

    }
}