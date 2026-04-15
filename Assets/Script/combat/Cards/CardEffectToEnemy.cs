using UnityEngine;

public class CardEffectToEnemy : MonoBehaviour
{
    public EnemyHPManager[] enemyHPManager;
    public int enemyCount;

    private void Start()
    {
        enemyCount = enemyHPManager.Length;
    }

    // 减血
    public void TakeDamage_Eone(GameObject enemy, int count)     // 单个敌人减血
    {
        foreach (var enemyTarget in enemyHPManager)
        {
            if (enemyTarget.gameObject == enemy)
            {
                enemyTarget.TakeDamage(count);
            }
        }
    }

    public void TakeDamage_Eall(int count)   // 全部敌人减血
    {
        foreach (var enemyTarget in enemyHPManager)
        {
            enemyTarget.TakeDamage(count);
        }
    }

    // 加血
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

    public void Heal_Eall(int count)
    {
        foreach (var enemyTarget in enemyHPManager)
        {
            enemyTarget.Heal(count);
        }
    }

    // 减盾
    public void RemoveDefense_Eone(GameObject enemy, int count)
    {
        foreach (var enemyTarget in enemyHPManager)
        {
            if (enemyTarget.gameObject == enemy)
            {
                enemyTarget.gameObject.GetComponent<DefenseManager>().RemoveDefense(count);
                enemyTarget.gameObject.GetComponent<DefenseManager>().redundant = 0;
            }
        }
    }

    public void RemoveDefense_Eall(int count)
    {
        foreach (var enemyTarget in enemyHPManager)
        {
            enemyTarget.gameObject.GetComponent<DefenseManager>().RemoveDefense(count);
            enemyTarget.gameObject.GetComponent<DefenseManager>().redundant = 0;
        }
    }

    // 加盾
    public void AddDefense_Eone(GameObject enemy, int count)
    {
        foreach (var enemyTarget in enemyHPManager)
        {
            if (enemyTarget.gameObject == enemy)
            {
                enemyTarget.gameObject.GetComponent<DefenseManager>().AddDefense(count);
            }
        }
    }

    public void AddDefense_Eall(int count)
    {
        foreach (var enemyTarget in enemyHPManager)
        {
            enemyTarget.gameObject.GetComponent<DefenseManager>().AddDefense(count);
        }
    }
}

