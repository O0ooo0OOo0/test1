using UnityEngine;

public class AbilityFireBall : MonoBehaviour
{
    [Header("火球效果")]
    public float riseSpeed = 1f;
    public float duration = 10f;
    public AnimationCurve shrinkCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    private float spawnTime;
    private Vector3 originalScale;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
        spawnTime = Time.time;

        // 如果是2D，让 Rigidbody 不受重力影响
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

    void Update()
    {
        float age = Time.time - spawnTime;

        // 10秒后消失
        if (age >= duration)
        {
            Destroy(gameObject);
            return;
        }

        // **缓慢向上移动**
        transform.Translate(Vector3.up * riseSpeed * Time.deltaTime);

        // **逐渐缩小**
        float progress = age / duration;
        float scale = shrinkCurve.Evaluate(progress);
        transform.localScale = originalScale * scale;
    }

    // 可选：碰到任何东西就消失（或触发效果）
    void OnTriggerEnter2D(Collider2D other)
    {
        // 这里可以添加火球碰到敌人的效果
        Debug.Log($"火球碰到: {other.name}");
        // Destroy(gameObject); // 如果想碰到就消失，取消注释
    }
}