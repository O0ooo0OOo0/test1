using UnityEngine;
using System.Collections;

public class rebound2 : MonoBehaviour
{
    public float bounceForce = 15f; // 固定反弹力

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            // 不管速度多少，直接给一个向上的力
            //rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce);

            Debug.Log("强制反弹！");
        }
    }
}