// PlayerMovement.cs - 移动控制
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("移动配置")]
    public float moveSpeed = 8f;

    private PlayerState playerState;
    private Rigidbody2D rb;
    private DashAbility dashAbility;
    private JumpAbility jumpAbility;  // ✅ 添加跳跃能力引用
    private Animator anim;  // ✅ 添加动画控制器
    private float xInput;   // ✅ 存储水平输入

    public float acceleration = 200f;  // 加速度，控制速度变化的平滑度
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerState = GetComponent<PlayerState>();
        dashAbility = GetComponent<DashAbility>();
        jumpAbility = GetComponent<JumpAbility>();  // ✅ 获取跳跃能力
        anim = GetComponent<Animator>();  // ✅ 获取动画组件
    }
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");  // 每帧获取输入

        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector2(1, 1);//往右移动时
        }
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);//往左移动时翻转
        }

        AnimatorController();
    }

    void FixedUpdate()
    {
        // ✅ 冲刺时禁用移动控制，但冲刺跳惯性期间允许控制
        //bool isDashing = dashAbility != null && dashAbility.IsDashing;
       // bool isInDashJumpInertia = jumpAbility != null && jumpAbility.IsInDashJumpInertia;

        // 冲刺时禁用移动控制
        //if (isDashing && !isInDashJumpInertia)
           // return;

        Move();


    }

    void Move()
    {
        float targetVelocityX = playerState.horizontalInput * moveSpeed;
        //rb.velocity = new Vector2(targetVelocityX, rb.velocity.y);

        float newVelocityX = Mathf.MoveTowards(rb.velocity.x, targetVelocityX, acceleration * Time.fixedDeltaTime);
        rb.velocity = new Vector2(newVelocityX, rb.velocity.y);
    }

    private void AnimatorController()
    {
        if (anim != null)
        {
            bool isRunning = xInput != 0;
            anim.SetBool("isRun", isRunning);
        }
    }
}