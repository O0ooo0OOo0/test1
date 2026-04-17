using UnityEngine;

public class CardEffectToPlayer : MonoBehaviour
{
    public PlayerHPManager playerHPManager;

    // ¼õÑª
    public void TakeDamage_P(int count)
    {
        playerHPManager.TakeDamage(count);
    }

    public void Reduceblood_P(int count)
    {
        playerHPManager.AbsoluteDamage(count);
    }

    // ¼ÓÑª
    public void Heal_P(int count)
    {
        playerHPManager.Heal(count);
    }

    // ¼õ¶Ü
    public void RemoveDefense_P(int count)
    {
        playerHPManager.gameObject.GetComponent<DefenseManager>().RemoveDefense(count);
        playerHPManager.gameObject.GetComponent<DefenseManager>().redundant = 0;
    }

    // ¼Ó¶Ü
    public void AddDefense_P(int count)
    {
        playerHPManager.gameObject.GetComponent<DefenseManager>().AddDefense(count);
    }
}
