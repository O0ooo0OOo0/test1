using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityIceCube : MonoBehaviour
{
    [Header("图层设置")]
    public LayerMask groundLayer;
    public LayerMask lavaLayer;

    [Header("反弹")]
    public float bounceFactor = 0.8f;
    public int maxBounces = 3;

    [Header("缩小效果")]
    public bool enableShrink = true;        // 是否启用缩小
    public AnimationCurve shrinkCurve = AnimationCurve.EaseInOut(0, 1, 1, 0); // 缩小曲线

    private Transform playerTransform;
    private float maxDistance;
    private float lifeTime;
    private float spawnTime;
    private Rigidbody2D rb;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;

        // 记录生成时间
        spawnTime = Time.time;
    }

    // 初始化方法（由 AbilityManager 调用）
    public void Initialize(Transform player, float maxDist, float duration)
    {
        playerTransform = player;
        maxDistance = maxDist;
        lifeTime = duration;
    }

    void Update()
    {
        //  距离检测

            float distance = Vector2.Distance(transform.position, playerTransform.position);
            if (playerTransform && distance > maxDistance)
            {
                Destroy(gameObject);
                return;
            }
        

        // 2. 时间检测（10秒后自动消失）
        if (Time.time - spawnTime >= lifeTime)
        {
            Destroy(gameObject);
        }

        // 随时间缩小
        if (enableShrink)
        {
            float progress = (Time.time - spawnTime) / lifeTime; // 0 → 1
            float scaleFactor = shrinkCurve.Evaluate(progress);
            transform.localScale = originalScale * scaleFactor;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null) return;
        }

        // 碰到地面反弹
        if (IsInLayerMask(collision.gameObject, groundLayer))
        {
            Vector2 normal = collision.GetContact(0).normal;
            rb.velocity = Vector2.Reflect(rb.velocity, normal) * bounceFactor;

        }

        // 碰到熔岩，冰块和熔岩都消失
        if (IsInLayerMask(collision.gameObject, lavaLayer))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }


    bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << obj.layer)) != 0);
    }
}