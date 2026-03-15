using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float xInput;
    private Rigidbody2D rb;
    private Animator anim;//动画

    public Vector2 platformVelocity = Vector2.zero;

    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpForce;
    [SerializeField] public float isGroundCheck;
    [SerializeField] public LayerMask GroundLayer;

    private bool isRunScript;

    private PlayerDash playerDash;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        playerDash = GetComponent<PlayerDash>();
    }


    void Update()
    {
        Xmove();

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

    private void AnimatorController()
    {
        isRunScript = xInput != 0;
        anim.SetBool("isRun", isRunScript);//设置布尔类型数值为
    }

    //左右移动
    private void Xmove()
    {
        // 如果正在冲刺，则跳过移动逻辑，让冲刺脚本控制速度
        if (playerDash != null && playerDash.IsDashing)
            return;

        xInput = Input.GetAxisRaw("Horizontal");
        
        float LastVelocityX = xInput * (moveSpeed + 5 * platformVelocity.x);

        Debug.Log(platformVelocity.x);
        Debug.Log(LastVelocityX);
        rb.velocity = new Vector2( LastVelocityX, rb.velocity.y);

    }






    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
