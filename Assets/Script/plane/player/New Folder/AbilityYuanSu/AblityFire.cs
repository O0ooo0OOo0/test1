// AbilityFire.cs - 火能力
using UnityEngine;

public class FireAbility : AbilityBase
{
    [Header("火能力配置")]
    public GameObject fireBallPrefab;
    public Transform throwPoint;
    public float throwForce = 10f;
    public float fireDuration = 20f;
    public float maxThrowDistance = 100f;
    public float riseSpeed = 3f;  // 火球上升速度

    public override string AbilityName => "Fire";

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

        // 计算发射方向（向上）
        Vector2 direction = GetThrowDirection();

        // 从对象池获取火球
        GameObject fireBall = ObjectPool.Instance.Get("FireBall", throwPoint.position, throwPoint.rotation);

        // 施加力
        //if (fireBall.TryGetComponent<Rigidbody2D>(out var rb))
          //  rb.AddForce(direction * throwForce, ForceMode2D.Impulse);

        // 初始化火球
        if (fireBall.TryGetComponent<AbilityFireBall>(out var fireScript))
            fireScript.Initialize(riseSpeed, fireDuration);
    }

    Vector2 GetThrowDirection()
    {
        // 优先使用输入方向
        if (playerState.horizontalInput != 0 || playerState.verticalInput != 0)
            return new Vector2(playerState.horizontalInput, playerState.verticalInput).normalized;

        // 否则使用向上方向
        return Vector2.up;
    }
}