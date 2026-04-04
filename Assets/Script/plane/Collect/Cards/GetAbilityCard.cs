using UnityEngine;

public enum CardAbilityType
{
    DoubleJump,
    Dash,
    Glide,
    IceAbility,
    FireAbility
}

public class CardPickup : MonoBehaviour
{
    [Header("卡牌配置")]
    public CardAbilityType cardType;

    [Header("特效（可选）")]
    public GameObject pickupEffect;
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AbilityManage abilityManage = other.GetComponent<AbilityManage>();

            if (abilityManage != null)
            {
                ApplyAbility(abilityManage);
            }

            // 播放特效
            if (pickupEffect != null)
                Instantiate(pickupEffect, transform.position, Quaternion.identity);

            // 播放音效
            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            // 销毁卡片
            Destroy(gameObject);
        }
    }

    private void ApplyAbility(AbilityManage abilityManage)
    {
        switch (cardType)
        {
            case CardAbilityType.DoubleJump:
                abilityManage.HasDoubleJump = true;
                break;
            case CardAbilityType.Dash:
                abilityManage.HasDash = true;
                break;
            case CardAbilityType.Glide:
                abilityManage.HasGlide = true;
                break;
            case CardAbilityType.IceAbility:
                abilityManage.HasIceAbility = true;
                break;
            case CardAbilityType.FireAbility:
                abilityManage.HasFireAbility = true;
                break;
        }

        Debug.Log($"获得能力: {cardType}");
    }
}