using System.Collections;
using UnityEngine;

public class TwoWayPlatform : MonoBehaviour
{
    public float disableDuration = 0.5f;//按下键后禁用碰撞的时间

    private Collider2D platformCollider;

    private IEnumerator DisablePlatformCoroutine()
    {
        //找到标签player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Collider2D playerCollider = player.GetComponent<Collider2D>();
        
        //忽略碰装
        Physics2D.IgnoreCollision(platformCollider, playerCollider, true);
        yield return new WaitForSeconds(disableDuration);
        Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platformCollider = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //碰撞检测
        if ( Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(DisablePlatformCoroutine());
        }
    }
}
