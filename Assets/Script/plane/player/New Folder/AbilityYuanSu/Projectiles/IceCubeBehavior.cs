// IceCubeBehavior.cs - 简化版
using UnityEngine;

public class IceCubeBehavior : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public float bounceFactor = 0.8f;
    public int maxBounces = 3;

    private Transform playerTransform;
    private float maxDistance;
    private float lifeTime;
    private float spawnTime;
    private Rigidbody2D rb;
    private int bounceCount;
    private bool isInPool;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Transform player, float maxDist, float duration)
    {
        playerTransform = player;
        maxDistance = maxDist;
        lifeTime = duration;
        spawnTime = Time.time;
        bounceCount = 0;
        isInPool = false;
    }

    void Update()
    {
        // 距离检测
        if (playerTransform && Vector2.Distance(transform.position, playerTransform.position) > maxDistance)
        {
            Destroy(gameObject);
            return;
        }

        // 时间检测
        if (Time.time - spawnTime >= lifeTime)
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 碰到敌人：销毁冰块
        if (IsInLayerMask(collision.gameObject, enemyLayer))
        {
            Destroy(gameObject);
            return;
        }

        // 碰到地面：反弹几次后销毁
        if (IsInLayerMask(collision.gameObject, groundLayer))
        {
            if (bounceCount >= maxBounces)
            {
                Destroy(gameObject);
                return;
            }

            Vector2 normal = collision.GetContact(0).normal;
            rb.velocity = Vector2.Reflect(rb.velocity, normal) * bounceFactor;
            bounceCount++;
        }
    }

    bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << obj.layer)) != 0);
    }
}