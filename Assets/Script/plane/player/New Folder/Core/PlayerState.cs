using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerState.cs - 统一管理玩家状态
public class PlayerState : MonoBehaviour
{
    [Header("状态标志")]
    public bool isGrounded;
    public bool isOnWall;
    public bool isDashing;
    public bool isGliding;
    public bool isJumping;
    public bool canDoubleJump;

    [Header("移动状态")]
    public Vector2 velocity;
    public float horizontalInput;
    public float verticalInput;
    public int facingDirection = 1; // 1=右, -1=左

    private Rigidbody2D rb;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 每帧更新输入和速度
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        velocity = rb.velocity;

        // 更新面向方向
        if (horizontalInput != 0)
            facingDirection = (int)Mathf.Sign(horizontalInput);
        else if (velocity.x != 0)
            facingDirection = (int)Mathf.Sign(velocity.x);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public Vector2 GetFacingDirection()
    {
        return new Vector2(facingDirection, 0);
    }
}
