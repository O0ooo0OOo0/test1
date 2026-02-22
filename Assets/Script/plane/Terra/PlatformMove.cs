using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    //设置两个点
    public Transform Pos1, Pos2;
    public Transform MovePos;
    [SerializeField] private float MoveSpeed;
    private Vector2 platformVelocity;
    void Start()
    {
        MovePos = Pos1;
    }

    // Update is called once per frame
    void Update()
    {
        // 计算平台速度
        platformVelocity = MoveSpeed * transform.right;
        Trace();
    }

    private void Trace()
    {
        //到一个点时，追踪另一个点
        if (Vector2.Distance(transform.position, Pos2.position) < 0.1f)
        {
            MovePos = Pos1;
        }

        if (Vector2.Distance(transform.position, Pos1.position) < 0.1f)
        {
            MovePos = Pos2;
        }

        transform.position = Vector2.MoveTowards(transform.position, MovePos.position, MoveSpeed * Time.deltaTime);//来回慢慢移动
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //碰到碰撞器时运行
        Debug.Log("inPlatform");

        //如果player碰到它
        //变为这个触发器的子物体
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = this.transform;
            // 获取PlayerMove组件
            PlayerMove playerMove = collision.GetComponent<PlayerMove>();
            if (playerMove != null)
            {
                Debug.Log(" 将平台速度传递给角色");
            
                // 将平台速度传递给角色
                playerMove.platformVelocity = platformVelocity;
            }
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("离开");

        //如果player碰到它

        //变为这个触发器的子物体
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = null;
            PlayerMove playerMove = collision.GetComponent<PlayerMove>();
            if (playerMove != null)
            {
                playerMove.platformVelocity = Vector2.zero;
            }
        }
    }
}



