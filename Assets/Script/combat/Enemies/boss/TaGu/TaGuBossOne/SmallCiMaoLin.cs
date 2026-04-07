using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCiMaoLin : MonoBehaviour
{
    public int count;   // 刺毛鳞数量
    public int[] attackValues;   // 刺毛鳞攻击数值
    public bool isSummonedCi;   // 是否召唤过刺毛鳞
    public bool isKillAll;

    // UI
    public GameObject[] cimaos;

    // 调用其他脚本
    public BossActionValueManager[] bavms;   // 敌人行为数值组
    public RoundManager bout;   // 回合系统
    public PlayerHPManager playerHP;
    public EnemyHPManager[] enemyHPs;
    public MoGuBossOne mgo;

    void Start()
    {
        count = 0;
        isSummonedCi = false;
        isKillAll = false;

        foreach (var cimao in cimaos)
        {
            cimao.SetActive(false);
        }
    }

    // 召唤刺毛鳞
    public void CreateCiMao()
    {
        isSummonedCi = true;

        for (int i = 0; i < count; i++)
        {
            if (cimaos[i].activeInHierarchy == false)
            {
                cimaos[i].SetActive(true);
                enemyHPs[i].currentHealth = enemyHPs[i].maxHealth;
                enemyHPs[i].UpdateHealthBar();
            }
        }
    }

    // 刺毛鳞行为预测
    public void GetCiMaosAction()
    {
        Attack();
        for (int i = 0; i < 3; i++)
        {
            if (cimaos[i].activeInHierarchy == true)
            {
                bavms[i].GetBossActionValue(attackValues[i], 0, 0, 0, false);
            }
        }
    }

    // 刺毛鳞行为执行
    public void RunCiMaosAction()
    {
        for (int i = 0; i < 3; i++)
        {
            if (cimaos[i].activeInHierarchy == true)
            {
                playerHP.TakeDamage(attackValues[i]);
            }
        }
    }

    // 刺毛鳞攻击模组
    public void Attack()
    {
        for (int i = 0; i < 3; i++)
        {
            if (cimaos[i].activeInHierarchy == true)
            {
                int random = Random.Range(1, 3);   // 攻击数值：1-2
                attackValues[i] = random;
            }
        }
    }
}
