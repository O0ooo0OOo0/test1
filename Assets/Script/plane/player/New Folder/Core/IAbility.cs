using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IAbility.cs - 能力接口
public interface IAbility
{
    string AbilityName { get; }
    bool CanUse { get; }
    void Use();
    void UpdateCooldown();
    void Reset();
}
// AbilityBase.cs - 能力基类
public abstract class AbilityBase : MonoBehaviour, IAbility
{
    [Header("基础能力配置")]
    public KeyCode activationKey = KeyCode.None;
    public float cooldownTime = 1f;
    public bool isAvailable = true;

    protected float nextUseTime;
    protected PlayerState playerState;
    protected Rigidbody2D rb;



    public abstract string AbilityName { get; }
   
    // 实现接口的 CanUse 属性
    public virtual bool CanUse => isAvailable && Time.time >= nextUseTime;

    protected virtual void Awake()
    {
        playerState = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Update()
    {
        // 每帧更新冷却
        UpdateCooldown();
        CheckInput();
    }

    protected virtual void CheckInput()
    {
        if (activationKey != KeyCode.None && Input.GetKeyDown(activationKey) && CanUse)
        {
            Use();
        }
    }
    // 实现接口的 Use 方法
    public virtual void Use()
    {
        if (!CanUse) return;
        nextUseTime = Time.time + cooldownTime;
        isAvailable = false;
    }

    // ✅ 实现接口的 UpdateCooldown 方法
    public virtual void UpdateCooldown()
    {
        if (!isAvailable && Time.time >= nextUseTime)
        {
            isAvailable = true;
            OnCooldownComplete();
        }
    }

    // 可选：冷却完成时的回调
    protected virtual void OnCooldownComplete() { }
    public virtual void Reset()
    {
        isAvailable = true;
        nextUseTime = 0;
    }

}
