// AbilityManager.cs - 管理所有能力
using System.Collections.Generic;
using UnityEngine;

public class AbilityManage : MonoBehaviour
{
    [Header("卡牌能力配置")]
    public bool hasDoubleJump = true;
    public bool hasDash = true;
    public bool hasGlide = false;
    public bool hasIceAbility = false;
    public bool hasFireAbility = false;

    private Dictionary<System.Type, AbilityBase> abilities = new Dictionary<System.Type, AbilityBase>();

    void Awake()
    {
        // 自动注册所有能力组件
        var allAbilities = GetComponents<AbilityBase>();
        foreach (var ability in allAbilities)
        {
            abilities[ability.GetType()] = ability;

            // 根据卡牌配置启用/禁用能力
            if (ability is JumpAbility jump)
                jump.hasDoubleJumpCard = hasDoubleJump;
            else if (ability is DashAbility dash)
                dash.enabled = hasDash;
            //else if (ability is GlideAbility glide)
             //   glide.enabled = hasGlide;
            else if (ability is IceAbility ice)
                ice.enabled = hasIceAbility;
            //else if (ability is FireAbility fire)
            //    fire.enabled = hasFireAbility;
        }
    }

    public T GetAbility<T>() where T : AbilityBase
    {
        if (abilities.TryGetValue(typeof(T), out var ability))
            return ability as T;
        return null;
    }

    // 动态获得/失去能力（卡牌切换）
    public void GrantAbility<T>() where T : AbilityBase
    {
        var ability = GetComponent<T>();
        if (ability != null)
            ability.enabled = true;
    }

    public void RemoveAbility<T>() where T : AbilityBase
    {
        var ability = GetComponent<T>();
        if (ability != null)
            ability.enabled = false;
    }
}