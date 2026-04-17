using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadCi : MonoBehaviour
{
    [System.Serializable]
    public class CiInf
    {
        public GameObject ci;   // 蔓延出的刺
        public GameObject ciTell;   // 提示该回合即将蔓延出的刺
        public int ciAliveRound;   // 刺已存在的回合数
    }

    public CiInf[] ciInfs;
    public int ciHurt;   // 刺板块每回合的伤害值

    // 调用其他脚本
    public RoundManager bout;   // 回合系统
    public PlayerHPManager playerHP;

    private void Start()
    {
        for (int i = 0; i < ciInfs.Length; i++)
        {
            ciInfs[i].ci.SetActive(false);
            ciInfs[i].ciTell.SetActive(false);
            ciInfs[i].ciAliveRound = 0;
        }
    }

    // 指示即将蔓延出刺的板块
    public void CiPanelTell(int id)
    {
        ciInfs[id].ciTell.SetActive(true);
    }

    // 刺蔓延板块（在预测的即将蔓延出刺的板块上生长出刺）
    public void CiPanelGrow()
    {
        for (int i = 0; i < ciInfs.Length; i++)
        {
            if (ciInfs[i].ci.activeInHierarchy == false && ciInfs[i].ciTell.activeInHierarchy == true)
            {
                ciInfs[i].ciTell.SetActive(false);
                ciInfs[i].ci.SetActive(true);
            }
        }
    }

    // 如果玩家在已存在刺的板块上，每回合失去2生命值（从刺生成的回合开始）
    public void CiAttackPlayer(int playerPanelIndex)
    {
        for (int i = 0; i < ciInfs.Length; i++)   
        {
            if (playerPanelIndex == i && ciInfs[i].ci.activeInHierarchy == true)
            {
                playerHP.AbsoluteDamage(ciHurt);
            }
        }
    }

    // 三回合后刺自然消失
    public void CiDisappearNaturely()
    {
        for (int i = 0; i < ciInfs.Length; i++)
        {
            if (ciInfs[i].ci.activeInHierarchy == true)
            {
                ciInfs[i].ciAliveRound++;   // 更新刺存在的回合数

                if (ciInfs[i].ciAliveRound > 3)   // 大于三回合的刺自动消失
                {
                    ciInfs[i].ci.SetActive(false);
                    ciInfs[i].ciAliveRound = 0;
                }
            }
            else if (!ciInfs[i].ci.activeInHierarchy)
            {
                ciInfs[i].ciAliveRound = 0;
            }
        }
    }
}
