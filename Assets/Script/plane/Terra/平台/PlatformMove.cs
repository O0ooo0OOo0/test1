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
    void FixedUpdate()
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

        // 修复：FixedUpdate 中应该用 Time.fixedDeltaTime
        transform.position = Vector2.MoveTowards(transform.position, MovePos.position, MoveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("碰撞到: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}



