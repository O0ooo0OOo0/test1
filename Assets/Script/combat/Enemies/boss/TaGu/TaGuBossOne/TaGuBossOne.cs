using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaGuBossOne : MonoBehaviour
{
    // 技能模组
    public int attackValue;   // 攻击数值
    public int defenseValue;   // 防御数值
    public int dodgeValue;   // 闪避数值
    public int othersValue;   // 其他特殊能力数值
    public bool isUnknow; 

    public int targetHP;   // 获胜的目标血量
    public int targetBout;   // 触发特殊事件的回合数
    public bool isWin;

    // 调用其他脚本
    public BossActionValueManager bavm;
    public RoundManager bout;   // 回合系统
    public DefenseManager defense;
    public PlayerHPManager playerHP;
    public EnemyHPManager enemyHP;   

    private void Start()
    {
        attackValue = 0;
        defenseValue = 0;
        dodgeValue = 0;
        othersValue = 0;
        isUnknow = false;
    }

    private void Update()
    {
        if (bout.currentbout > targetBout && enemyHP.currentHealth <= targetHP && isWin == false)
        {
            GetWin();
        }
    }

    // 获取Boss行为（对boss该回合的行动进行预测）
    public void GetBossAction()
    {
        Attack();
        Defense();
        UpHealth();
        bavm.GetBossActionValue(attackValue, defenseValue, dodgeValue, othersValue, isUnknow);
    }   
    
    // 执行Boss行为（在boss的行动回合进行boss行为）
    public void RunBossAction()
    {
        playerHP.TakeDamage(attackValue);
        defense.DefineDefense(defenseValue);
        enemyHP.Heal(othersValue);
    }

    /// <summary>
    /// 技能模组
    /// </summary>

    // 攻击：1）突刺（直接进行攻击：4-6）/ 2）飞石（操控石头进行攻击：3-7）
    public void Attack()
    {
        int randomWay = Random.Range(0, 2);
        if (randomWay == 0)   // 0.5的概率攻击方式一
        {
            int random = Random.Range(4, 7);
            attackValue = random;
        }
        else if (randomWay == 1)   // 0.5的概率攻击方式二
        {
            int random = Random.Range(3, 8);
            attackValue = random;
        }
    }

    // 防御：0.3的概率可以防御（5）
    public void Defense()
    {
        float random = Random.Range(0, 1);
        if (random < 0.3)
        {
            defenseValue = 5;
        }
        else
        {
            defenseValue = 0;
        }
    }

    // 回复血量：每两回合恢复自身血量的10%~20%
    public void UpHealth()
    {
        int round = bout.currentbout % 2;
        if (round == 0)
        {
            int adHP = 0;
            float random = Random.Range(0, 1);
            if (random <= 0.5)   // 50%的概率回复10%
            {
                adHP = (int)(enemyHP.currentHealth * 0.1);   // 向下取整
            }
            else if (random > 0.5 && random <= 0.8)   // 30%的概率回复15%
            {
                adHP = (int)(enemyHP.currentHealth * 0.15);  
            }
            else if (random > 0.8)   // 20%的概率回复20%
            {
                adHP = (int)(enemyHP.currentHealth * 0.2);   
            }
            othersValue = adHP;
        }
    }

    /// <summary>
    /// 获胜
    /// </summary>

    public void GetWin()
    {
        isWin = true;
    }
}
