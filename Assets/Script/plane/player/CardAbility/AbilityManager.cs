using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [Header("冰能力")]
    public float icecooldownTime = 0.5f;      // 冷却时间1秒
    private float nextIceTime = 0f;   // 下次可以使用能力的时间

    public float IceAbilityDuration = 20f;    

    [Header("预制体")]
    public GameObject iceCubePrefab;       // 冰块预制体
    public Transform throwPoint;           // 投掷点
    public float throwForce = 10f;         // 投掷力度

    public float maxThrowDistance = 100f;    // 冰块最大存在距离

    [Header("火能力")]
    public float FirecooldownTime = 0.5f;      // 冷却时间1秒
    private float nextFireTime = 0f;   // 下次可以使用能力的时间
    public float fireRiseSpeed = 1f;
    public float FireAbilityDuration = 20f;

    [Header("预制体")]
    public GameObject FireCubePrefab;       // 预制体
    public Transform RisePoint;           // 投掷点
    public float fireDuration = 10f;         


    private PlayerMove playerMove;          // 引用玩家移动脚本（如果需要获取玩家朝向）

    void Start()
    {
        // 获取玩家移动脚本（用于获取玩家朝向）
        playerMove = GetComponent<PlayerMove>();

        // 如果没有设置投掷点，默认使用玩家位置
        if (throwPoint == null)
            throwPoint = transform;
    }

    void Update()
    {
        // X键 - 冰能力
        if (Input.GetKeyDown(KeyCode.X)) TryUseIce();

        // C键 - 火能力
        if (Input.GetKeyDown(KeyCode.C)) TryUseFire();
    }

    // 尝试使用能力
    void TryUseIce()
    {
        // 检查是否还在冷却中
        if (Time.time < nextIceTime) return;
        // 可以使用能力
        if (iceCubePrefab == null) return;
       
        //计算方向
        // 获取方向输入
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 如果没按方向键，默认朝玩家面向方向
        if (horizontal == 0 && vertical == 0)
            horizontal = transform.localScale.x > 0 ? 1 : -1;

        // 计算八方向向量并归一化
        Vector2 direction = new Vector2(horizontal, vertical).normalized;

        // 实例化冰块
        GameObject ice = Instantiate(iceCubePrefab, throwPoint.position, throwPoint.rotation);

        // 施加力
        if (ice.TryGetComponent<Rigidbody2D>(out var rb))
            rb.AddForce(direction * throwForce, ForceMode2D.Impulse);

        // 初始化冰块脚本
        if (ice.TryGetComponent<AbilityIceCube>(out var script))
            script.Initialize(transform, maxThrowDistance, IceAbilityDuration);

        // 设置下次可用时间（当前时间 + 冷却时间）
        nextIceTime = Time.time + icecooldownTime;
    }

    void TryUseFire()
    {
        if (Time.time < nextFireTime) return;
        if (!FireCubePrefab) return;

        // 获取玩家朝向（1=朝右，-1=朝左）
        float direction = transform.localScale.x > 0 ? 1 : -1;
        RisePoint.localScale = new Vector3(direction, 1, 1);

        // 生成火球
        GameObject fire = Instantiate(FireCubePrefab, RisePoint.position, Quaternion.identity);

        // 让火球也面向正确的方向
        fire.transform.localScale = new Vector3(direction, 1, 1);

        // 初始化火球脚本
        if (fire.TryGetComponent<AbilityFireBall>(out var script))
            script.Initialize(fireRiseSpeed, fireDuration);

    



    nextFireTime = Time.time + FirecooldownTime;
    }

    // 可选：显示冷却进度
    void OnGUI()
    {
        // 如果想在游戏画面上显示冷却进度，可以取消下面的注释
        /*
        float remaining = nextAbilityTime - Time.time;
        if (remaining > 0)
        {
            GUI.Label(new Rect(10, 10, 200, 20), $"能力冷却: {remaining:F1}s");
        }
        else
        {
            GUI.Label(new Rect(10, 10, 200, 20), "能力就绪");
        }
        */
    }
}