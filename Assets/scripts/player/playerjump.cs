using UnityEngine;

public class playerjump : MonoBehaviour
{
    private Rigidbody2D rb;//��������Ϊ���塣�洢���ڸ���Ķ���
    private Animator anim;

    [SerializeField] public float jumpForce;
    [SerializeField] public float isGroundCheck;
    [SerializeField] public LayerMask GroundLayer;

    [SerializeField] public float jumpAddTime;//延长跳
    [SerializeField] private float jumpAddController;//延长跳

    private bool isRunScript;
    private bool isJumpScript;
    private bool isJumping;
    private bool isGround;
    private bool DoubleJump;
    public bool HasDoubleJump = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    
    void Update()
    {
        //跳跃

        Jump();


        //光线投射检测地面
        isGround = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, GroundLayer);
        anim.SetBool("isGround", isGround);
        //自身位置，乡下投射，1.1，
    }

    private void Jump()
    {
        if (Input.GetButton("Jump"))
        {
            //Debug.Log("holddd");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumpScript = true;
            DoubleJump = true;
            jumpAddController = 0;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && DoubleJump && !isGround && HasDoubleJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpAddController = 0;
            isJumping = true;
            isJumpScript = true;
       
            DoubleJump = false;
            
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumpScript = false;
           
        }

        //延长跳
        if(isJumpScript )
        {
            if(jumpAddController < jumpAddTime)
            {
               rb.linearVelocity += new Vector2(0, -Physics2D.gravity.y * Time.deltaTime);
                jumpAddController += Time.deltaTime;
            }
            else
            {
                isJumpScript = false;
            }
           

        }


        //重力
        if(!isJumpScript)
        {
            rb.linearVelocity -= new Vector2(0, -Physics2D.gravity.y* 2 * Time.deltaTime);
            jumpAddController = 0;
        }

        anim.SetBool("isJump", isJumpScript);//设置布尔类型
    }
}
