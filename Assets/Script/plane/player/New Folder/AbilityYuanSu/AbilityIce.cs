// IceAbility.cs - 冰能力
using UnityEngine;

public class IceAbility : AbilityBase
{
    [Header("冰能力配置")]
    public GameObject iceCubePrefab;
    public Transform throwPoint;
    public float throwForce = 10f;
    public float iceDuration = 20f;
    public float maxThrowDistance = 100f;

    public override string AbilityName => "Ice";

    private AbilityManage abilityManage;
    private bool isAbilityUnlocked = false;

    protected override void Awake()
    {
        base.Awake();

        // 查找 AbilityManage
        abilityManage = GetComponent<AbilityManage>();
        if (abilityManage == null)
        {
            abilityManage = FindObjectOfType<AbilityManage>();
        }
    }

    void Start()
    {
        // 初始检查能力是否解锁
        CheckUnlockStatus();
    }

    protected override void Update()
    {
        // 先检查能力是否解锁
        CheckUnlockStatus();

        // 如果能力未解锁，不处理输入
        if (!isAbilityUnlocked) return;

        // 更新冷却
        UpdateCooldown();

        // 检查输入（使用基类的逻辑）
        CheckInput();
    }

    void CheckUnlockStatus()
    {
        if (abilityManage != null)
        {
            isAbilityUnlocked = abilityManage.hasIceAbility;

            // 如果能力被禁用，重置冷却状态
            if (!isAbilityUnlocked)
            {
                isAvailable = true;
                nextUseTime = 0;
            }
        }
    }

    protected override void CheckInput()
    {
        if (isAbilityUnlocked && Input.GetKeyDown(activationKey) && CanUse)
        {
            Use();
        }
    }

    public override void Use()
    {
        base.Use();

        // 计算投掷方向
        Vector2 direction = GetThrowDirection();

        // 从对象池获取冰块
        GameObject ice = ObjectPool.Instance.Get("IceCube", throwPoint.position, throwPoint.rotation);

        // 施加力
        if (ice.TryGetComponent<Rigidbody2D>(out var rb))
            rb.AddForce(direction * throwForce, ForceMode2D.Impulse);

        // 初始化冰块
        if (ice.TryGetComponent<IceCubeBehavior>(out var iceScript))
            iceScript.Initialize(transform, maxThrowDistance, iceDuration);
    }


    Vector2 GetThrowDirection()
    {
        // 优先使用输入方向
        if (playerState.horizontalInput != 0 || playerState.verticalInput != 0)
            return new Vector2(playerState.horizontalInput, playerState.verticalInput).normalized;

        // 否则使用面向方向
        return playerState.GetFacingDirection();
    }

    // 公开方法，供卡牌系统动态设置按键
    public void SetActivationKey(KeyCode newKey)
    {
        activationKey = newKey;
    }
    // 公开方法，供卡牌系统动态启用/禁用
    public void SetUnlocked(bool unlocked)
    {
        isAbilityUnlocked = unlocked;
        if (!unlocked)
        {
            isAvailable = true;
            nextUseTime = 0;
        }
    }
}
