using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoGuBossOne : MonoBehaviour
{
    // 技能模组
    public int attackValue;   // 攻击数值
    public int defenseValue;   // 防御数值
    public int dodgeValue;   // 闪避数值
    public int othersValue;   // 其他特殊能力数值
    public bool isUnknow;

    public int stage;
    public bool isSummon;   // 判断当前回合是否召唤 
    public bool isWin;

    // 调用其他脚本
    public BossActionValueManager bavm;
    public RoundManager bout;   // 回合系统
    public DefenseManager defense;
    public PlayerHPManager playerHP;
    public EnemyHPManager enemyHP;
    public SmallCiMaoLin scml;
    public SpreadCi sc;
    public PfMoGuOne pfmgo;

    private void Start()
    {
        ResetBossActionValue();
        isUnknow = false;
        isSummon = false;
        isWin = false;
    }

    // 重置Boss行为数据
    public void ResetBossActionValue()
    {
        attackValue = 0;
        defenseValue = 0;
        dodgeValue = 0;
        othersValue = 0;
    }

    // 判断当前Boss处于的战斗阶段
    public void GetBossActionStage()
    {
        if (enemyHP.currentHealth >= enemyHP.maxHealth * 0.4)
        {
            stage = 0;
        }
        else 
        { 
            stage = 1;
        }
    }

    // 获取Boss行为（对boss该回合的行动进行预测）
    public void GetBossAction()
    {
        ResetBossActionValue();

        //  阶段一：蔓延，无召唤
        if (stage == 0)   
        {
            float randomA = Random.Range(0, 1);
            if (randomA <= 0.3)   // 30%的概率产生蔓延
            {
                SpreadCiValue();

                float randomB = Random.Range(0, 1);
                if (randomB <= 0.15)   // 产生蔓延时，15%的概率进行攻击
                {
                    AttackValue(0.8f);   // 单刺概率80%，多次概率20%
                }
            }
            else   // 未产生蔓延，一定进行攻击
            {
                AttackValue(0.7f);   // 单刺概率70%，多刺概率30%
            }
            DefenseValue();
        }

        // 阶段二：召唤，无蔓延
        if (stage == 1)
        {
            SummonCiMaoLinsValue();

            if (isSummon == true)   // 召唤回合
            {
                float randomA = Random.Range(0, 1);
                if (randomA <= 0.6)   // 60%的概率进行攻击
                {
                    AttackValue(0.9f);   // 单刺概率90%，多次概率10%
                }
            }
            else if (isSummon == false)   // 非召唤回合
            {
                DefenseValue();
                AttackValue(0.8f);   // 单刺概率80%，多次概率20%
            }
        }
        
        bavm.GetBossActionValue(attackValue, defenseValue, dodgeValue, othersValue, isUnknow);   // 传递boss行为参数
    }

    // 执行Boss行为（在boss的行动回合进行boss行为）
    public void RunBossAction()
    {
        // 阶段一：蔓延、攻击、防御
        if (stage == 0)
        {
            SpreadCi();
            Attack();
            Defense();
        }

        // 阶段二：召唤、攻击、防御
        if (stage == 1)
        {
            SummonCiMaoLins();
            Attack();
            Defense();
        }
    }

    /// <summary>
    /// 技能模组-数值
    /// </summary>

    // 攻击：1）单刺攻击：3；2）多刺攻击：6-9
    public void AttackValue(float cut)
    {
        float randomWay = Random.Range(0, 1);
        if (randomWay < cut)   // 攻击方式一
        {
            attackValue = 3;
        }
        else if (randomWay >= cut)   // 攻击方式二
        {
            int random = Random.Range(6, 10);
            attackValue = random;
        }
    }

    // 防御：0.3的概率可以防御（3）
    public void DefenseValue()
    {
        float random = Random.Range(0, 1);
        if (random < 0.3)
        {
            defenseValue = 3;
        }
        else
        {
            defenseValue = 0;
        }
    }

    // 召唤：每两回合召唤至三个刺毛鳞（攻击1-2）
    public void SummonCiMaoLinsValue()
    {
        int roundCp = 0;   // 定义副回合数

        if (scml.count != 0)
        {
            roundCp++;
        }
        else if (scml.count == 0)
        {
            if (scml.isKillAll == false)
            {
                roundCp--;
                if (roundCp < 0)
                {
                    roundCp = 0;
                }
            }
            else if (scml.isKillAll == true)
            {
                scml.isKillAll = false;
            }
        }

        int round = roundCp % 2;

        if (round == 0)   // 补充召唤刺毛鳞
        {
            othersValue = 3 - scml.count;   // 补充缺少的刺毛鳞数量
            scml.count = 3;   // 刺毛鳞数量置3
            if (othersValue != 0)   // 产生了召唤（实际召唤）
            {
                isSummon = true;
            }
        }
    }

    // 蔓延：在五个板块上随机选择一(0.8)/二(0.2)个板块生成刺毛鳞
    public void SpreadCiValue()
    {
        float random = Random.Range(0, 1);

        // 确定本次蔓延的板块数量
        if (random < 0.8)
        {
            othersValue = 1;
        }
        else
        {
            othersValue = 2;
        }

        // 确定本次蔓延的板块编号ID
        for (int i = 0; i < othersValue; i++)
        {
            List<int> unCiId = new();   // 记录目前未被蔓延的板块ID
            int unCiAmount = 0;

            // 确定还未生长刺毛鳞的板块Index及总数
            for (int j = 0; j < sc.ciInfs.Length; j++)
            {
                if (sc.ciInfs[j].ci.activeInHierarchy == false && sc.ciInfs[j].ciTell.activeInHierarchy == false)   // 该板块刺毛鳞未生长
                {
                    unCiId.Add(j);
                    unCiAmount++;
                }
            }

            int randomCp = Random.Range(0, unCiAmount);
            for (int j = 0; j < unCiId.Count; j++)
            {
                if (j == randomCp)
                {
                    int id = unCiId[j];
                    sc.CiPanelTell(id);   // 指示当前回合刺蔓延的板块
                }
            }
        }
    }

    /// <summary>
    /// 技能模组-执行
    /// </summary>

    // 攻击
    public void Attack()
    {
        playerHP.TakeDamage(attackValue);
    }

    // 防御
    public void Defense()
    {
        defense.DefineDefense(defenseValue);
    }

    // 召唤
    public void SummonCiMaoLins()
    {
        scml.CreateCiMao();
    }

    // 蔓延
    public void SpreadCi()
    {
        sc.CiPanelGrow();   // 长出刺
        sc.CiDisappearNaturely();   // 更新刺存在的回合数
        pfmgo.GetPlayerPanelId();   // 判断刺是否对玩家造成伤害
        sc.CiAttackPlayer(pfmgo.panelInd);
    }

    /// <summary>
    /// 获胜
    /// </summary>

    public void GetWin()
    {
        isWin = true;
    }
}
