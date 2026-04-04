// GlideAbility.cs - 滑翔能力
using UnityEngine;

public class GlideAbility : AbilityBase
{
    [Header("滑翔配置")]
    public float glideSpeedMultiplier = 0.2f;  // 滑翔时的下落速度倍率（原来的0.2倍）
    public float glideHorizontalMultiplier = 1f;  // 滑翔时水平速度倍率
    public float glideDrag = 0.2f;            // 滑翔时的阻尼

    [Header("输入设置")]
    public KeyCode glideKey = KeyCode.UpArrow;  // 滑翔按键

    private bool isGliding;
    private float originalDrag;
    private float originalGravityScale;

    public override string AbilityName => "Glide";
    public bool IsGliding => isGliding;

    protected override void Awake()
    {
        base.Awake();
        originalDrag = rb.drag;
        originalGravityScale = rb.gravityScale;
    }

    protected override void Update()
    {
        base.Update();

        // 只有能力启用时才处理输入
        if (!enabled) return;

        // 检查滑翔输入
        if (Input.GetKey(glideKey) && CanGlide())
        {
            StartGlide();
        }
        else if (isGliding)
        {
            EndGlide();
        }

        // 滑翔时的速度控制
        if (isGliding)
        {
            UpdateGlide();
        }
    }

    bool CanGlide()
    {
        // 只有在空中且未冲刺时才能滑翔
        return !playerState.isGrounded && !playerState.isJumping && !playerState.isDashing;
    }

    void StartGlide()
    {
        if (isGliding) return;

        isGliding = true;
        playerState.isGliding = true;

        // 设置滑翔参数
        rb.drag = glideDrag;
        rb.gravityScale = 0f;  // 关闭重力，自己控制下落

        Debug.Log("开始滑翔");
    }

    void UpdateGlide()
    {
        // ✅ 将下落速度改为原来的0.2倍
        if (rb.velocity.y < 0)  // 正在下落
        {
            // 计算原来应该有的速度，然后乘以0.2
            float originalVelocity = rb.velocity.y;
            float newVelocity = originalVelocity * glideSpeedMultiplier;
            rb.velocity = new Vector2(rb.velocity.x, newVelocity);
        }

        // 水平速度衰减（可选）
        if (glideHorizontalMultiplier < 1f)
        {
            float horizontalSpeed = rb.velocity.x * glideHorizontalMultiplier;
            rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);
        }
    }

    void EndGlide()
    {
        if (!isGliding) return;

        isGliding = false;
        playerState.isGliding = false;

        // 恢复原始参数
        rb.drag = originalDrag;
        rb.gravityScale = originalGravityScale;

        Debug.Log("结束滑翔");
    }

    public override void Reset()
    {
        base.Reset();
        EndGlide();
    }


}