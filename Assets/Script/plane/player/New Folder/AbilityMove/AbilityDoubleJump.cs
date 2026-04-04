using UnityEngine;

public class DoubleJumpAbility : AbilityBase
{
    [Header("二段跳配置")]
    [SerializeField] private float doubleJumpForceMultiplier = 1f;  // 相对于普通跳跃的倍率（1 = 相同力度）

    private bool canDoubleJump;  // 二段跳是否可用（落地时重置）
    private JumpAbility jumpAbility;  // 引用基础跳跃能力

    public override string AbilityName => "DoubleJump";

    protected override void Awake()
    {
        base.Awake();
        jumpAbility = GetComponent<JumpAbility>();
    }
    private void OnEnable()
    {
        // 组件被启用时重置二段跳
        canDoubleJump = true;
        //Debug.Log("DoubleJumpAbility 已启用，canDoubleJump = true");
    }

    private void OnDisable()
    {
        // 组件被禁用时清理
        canDoubleJump = false;
    }

    void FixedUpdate()
    {
        // 地面检测（通过 JumpAbility 的状态）
        if (playerState.isGrounded)
        {
            // 落地时重置二段跳
            canDoubleJump = true;
        }
    }

    protected override void CheckInput()
    {
        // 只有按下跳跃键且能力启用时才检查
        if (activationKey != KeyCode.None && Input.GetKeyDown(activationKey) && CanUse)
        {
            TryDoubleJump();
        }
    }

    void TryDoubleJump()
    {
        // 在空中、没有跳跃中、还有二段跳次数
        if (!playerState.isGrounded && !playerState.isJumping && canDoubleJump)
        {
            PerformDoubleJump();
        }
    }

    void PerformDoubleJump()
    {
        // 继承普通跳跃的力度
        float jumpForce = jumpAbility != null ? jumpAbility.jumpForce : 10f;
        float finalForce = jumpForce * doubleJumpForceMultiplier;
        // 执行二段跳
        rb.velocity = new Vector2(rb.velocity.x, finalForce);

        // 标记状态
        canDoubleJump = false;
        playerState.isJumping = true;  // 标记为跳跃中

        Debug.Log("执行二段跳！");

        // 可选：播放二段跳特效/音效
        // AudioSource.PlayClipAtPoint(doubleJumpSound, transform.position);
    }

    // 公共方法：外部可以重置二段跳（比如获得新能力时）
    public void ResetDoubleJump()
    {
        canDoubleJump = true;
    }
}