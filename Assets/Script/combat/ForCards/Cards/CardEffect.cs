using UnityEngine;

public class CardEffect : MonoBehaviour
{
    public EnemyHPManager[] enemyHPManager;
    public PlayerHPManager playerHPManager;
    public int enemyCount;

    private void Start()
    {
        enemyCount = enemyHPManager.Length;
    }

    // ¼õÑª
    public void TakeDamage_Eone(GameObject enemy, int count)
    {
        foreach (var enemyTarget in enemyHPManager)
        {
            if (enemyTarget.gameObject == enemy)
            {
                enemyTarget.TakeDamage(count);
            }
        }
    }

    public void TakeDamage_P(int count)
    {
        playerHPManager.TakeDamage(count);
    }

    public void Reduceblood_P(int count)
    {
        playerHPManager.Reduceblood(count);
    }

    public void TakeDamage_Eall(int count)
    {
        foreach (var enemyTarget in enemyHPManager)
        {
            enemyTarget.TakeDamage(count);
        }
    }

    public void TakeDamage_All(int count)
    {
        foreach (var enemyTarget in enemyHPManager)
        {
            enemyTarget.TakeDamage(count);
        }
        playerHPManager.TakeDamage(count);
    }

    // ¼ÓÑª
    public void Heal_Eone(GameObject enemy, int count)
    {
        foreach (var enemyTarget in enemyHPManager)
        {
            if (enemyTarget.gameObject == enemy)
            {
                enemyTarget.Heal(count);
            }
        }
    }

    public void Heal_P(int count)
    {
        playerHPManager.Heal(count);
    }

    public void Heal_Eall(int count)
    {
        foreach (var enemyTarget in enemyHPManager)
        {
            enemyTarget.Heal(count);
        }
    }

    public void Heal_All(int count)
    {
        foreach (var enemyTarget in enemyHPManager)
        {
            enemyTarget.Heal(count);
        }
        playerHPManager.Heal(count);
    }

    // ¼õ¶Ü
    public void RemoveDefense_Eone(GameObject enemy, int count)
    {
        foreach (var enemyTarget in enemyHPManager)
        {
            if (enemyTarget.gameObject == enemy)
            {
                enemyTarget.gameObject.GetComponent<DefenseNumber>().RemoveDefense(count);
                enemyTarget.gameObject.GetComponent<DefenseNumber>().redundant = 0;
            }
        }
    }

    public void RemoveDefense_P(int count)
    {
        playerHPManager.gameObject.GetComponent<DefenseNumber>().RemoveDefense(count);
        playerHPManager.gameObject.GetComponent<DefenseNumber>().redundant = 0;
    }

    public void RemoveDefense_Eall(int count)
    {
        foreach (var enemyTarget in enemyHPManager)
        {
            enemyTarget.gameObject.GetComponent<DefenseNumber>().RemoveDefense(count);
            enemyTarget.gameObject.GetComponent<DefenseNumber>().redundant = 0;
        }
    }

    public void RemoveDefense_All(int count)
    {
        foreach (var enemyTarget in enemyHPManager)
        {
            enemyTarget.gameObject.GetComponent<DefenseNumber>().RemoveDefense(count);
            enemyTarget.gameObject.GetComponent<DefenseNumber>().redundant = 0;
        }
        playerHPManager.gameObject.GetComponent<DefenseNumber>().RemoveDefense(count);
        playerHPManager.gameObject.GetComponent<DefenseNumber>().redundant = 0;
    }

    // ¼Ó¶Ü
    public void AddDefense_Eone(GameObject enemy, int count)
    {
        foreach (var enemyTarget in enemyHPManager)
        {
            if (enemyTarget.gameObject == enemy)
            {
                enemyTarget.gameObject.GetComponent<DefenseNumber>().AddDefense(count);
            }
        }
    }

    public void AddDefense_P(int count)
    {
        playerHPManager.gameObject.GetComponent<DefenseNumber>().AddDefense(count);
    }

    public void AddDefense_Eall(int count)
    {
        foreach (var enemyTarget in enemyHPManager)
        {
            enemyTarget.gameObject.GetComponent<DefenseNumber>().AddDefense(count);
        }
    }

    public void AddDefense_All(int count)
    {
        foreach (var enemyTarget in enemyHPManager)
        {
            enemyTarget.gameObject.GetComponent<DefenseNumber>().AddDefense(count);
        }
        playerHPManager.gameObject.GetComponent<DefenseNumber>().AddDefense(count);
    }
}
