using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCubeBehavior : MonoBehaviour
{
    [Header("火球效果")]
    public float riseSpeed = 1f;
    public float duration = 10f;
    public AnimationCurve shrinkCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    private float spawnTime;
    private Vector3 originalScale;
    private Rigidbody2D rb;
    private bool isInPool = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
        spawnTime = Time.time;

        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
        }
    }

    public void Initialize(float riseSpeed, float lifeTime)
    {
        this.riseSpeed = riseSpeed;
        this.duration = lifeTime;
    }

    private void ReturnToPool()
    {
        if (isInPool) return;
        isInPool = true;

        ObjectPool.Instance.Return("FireBall", gameObject);
    }

    void Update()
    {
        float age = Time.time - spawnTime;

        if (age >= duration)
        {
            ReturnToPool();
            return;
        }

        // 向上移动
        transform.Translate(Vector3.up * riseSpeed * Time.deltaTime);

        // 逐渐缩小
        float progress = age / duration;
        float scale = shrinkCurve.Evaluate(progress);
        transform.localScale = originalScale * scale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"火球碰到: {other.name}");
    }
}
