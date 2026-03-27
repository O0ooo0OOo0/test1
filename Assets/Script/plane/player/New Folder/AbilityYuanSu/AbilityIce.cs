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

    protected override void CheckInput()
    {
        if (Input.GetKeyDown(activationKey) && CanUse)
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
}

// FireAbility.cs - 类似结构