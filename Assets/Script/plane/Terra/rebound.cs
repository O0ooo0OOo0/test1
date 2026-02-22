using UnityEngine;
using System.Collections;
public class rebound : MonoBehaviour
{
    //反弹系数
    public float jumpforce = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerjump playerJumpScript = collision.gameObject.GetComponent<playerjump>(); // 修正变量名

            //标记
            playerJumpScript.isOnReboundPlatform = true;

            // 获取当前速度
            Vector2 currentVelocity = rb.velocity;
           
            Debug.Log($"碰撞时速度: {currentVelocity.y}");


            // 只反转Y轴速度，保持X轴速度不变
            float newYVelocity = -currentVelocity.y * jumpforce;
            rb.velocity = new Vector2(currentVelocity.x, newYVelocity);
            
            
            //施加向上力
            //rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);

            // GetComponent<AudioSource>()?.Play();
            // 延迟恢复重力修改
            StartCoroutine(ResetGravity(playerJumpScript)); 

        }

    }
    // 极短时间后恢复 
    private IEnumerator ResetGravity(playerjump playerJumpScript)
    {
        yield return new WaitForSeconds(0.01f); // 从0.1f改为0.02f
      
        playerJumpScript.isOnReboundPlatform = false;
 
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
