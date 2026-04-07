using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Unity.Collections.AllocatorManager;

public class PlayerHPManager : MonoBehaviour
{
    public int maxHealth = 100; // 最大血量
    public int currentHealth; // 当前血量
    public Image healthBar; // 血量条（UI）
    public GameObject HP;
    public TMP_Text healthText;

    public DefenseManager defense;

    private void Start()
    {
        HP.SetActive(true);
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    // 回复血量
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(maxHealth, currentHealth); // 防止血量超过最大值
        UpdateHealthBar();
    }

    // 受到伤害
    public void TakeDamage(int amount)
    {
        defense.RemoveDefense(amount);

        if (defense.redundant > 0)
        {
            currentHealth = currentHealth - defense.redundant;
            currentHealth = Mathf.Max(0, currentHealth); // 防止血量小于0
            UpdateHealthBar();
            if (currentHealth == 0)
            {
                Die();
            }
        }
    }

    // 忽略防御直接伤害
    public void AbsoluteDamage(int count)
    {
        currentHealth = currentHealth - count;
        currentHealth = Mathf.Max(0, currentHealth); // 防止血量小于0
        UpdateHealthBar();
        if (currentHealth == 0)
        {
            Die();
        }
    }

    // 更新血量条
    private void UpdateHealthBar()
    {
        healthText.text = currentHealth + "/" + maxHealth;
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    // 死亡逻辑
    private void Die()
    {
        //Debug.Log("Monster is dead!");
        //Destroy(gameObject); // 销毁怪物对象
        gameObject.SetActive(false);
        HP.SetActive(false);
    }
}