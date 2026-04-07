using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossActionUIManager : MonoBehaviour
{
    // Boss行动UI
    public GameObject attack;   // 普通物理攻击
    public GameObject defense;   // 防御
    public GameObject dodge;   // 闪避
    public GameObject others;   // 其他特殊技能
    public GameObject unknow;   // boss行为未知

    // Boss行动数值文本
    public TMP_Text attackValueText;
    public TMP_Text defenseValueText;
    public TMP_Text dodgeValueText;
    public TMP_Text othersValueText;

    // 调用其他脚本
    public BossActionValueManager bavm;

    void Start()
    {
        HideBossActionUI();
    }

    // 显示Boss行为的预测
    public void ShowBossActionUI()
    {
        if (bavm.isUnknow == false)   // 已知boss行为
        {
            if (unknow != null)
            {
                unknow.SetActive(false);
            }

            // 攻击
            if (attack != null)
            {
                if (bavm.attackV != 0)
                {
                    attack.SetActive(true);
                    attackValueText.text = bavm.attackV.ToString();
                }
                else
                {
                    attack.SetActive(false);
                }
            }

            // 防御
            if (defense != null)
            {
                if (bavm.defenseV != 0)
                {
                    defense.SetActive(true);
                    defenseValueText.text = bavm.defenseV.ToString();
                }
                else
                {
                    defense.SetActive(false);
                }
            }
            
            // 闪避
            if (dodge != null)
            {
                if (bavm.dodgeV != 0)
                {
                    dodge.SetActive(true);
                    dodgeValueText.text = bavm.dodgeV.ToString();
                }
                else
                {
                    dodge.SetActive(false);
                }
            }

            // 其他
            if (others != null)
            {
                if (bavm.othersV != 0)
                {
                    others.SetActive(true);
                    othersValueText.text = bavm.othersV.ToString();
                }
                else
                {
                    others.SetActive(false);
                }
            }
        }
        else if (bavm.isUnknow == true)   // 未知boss行为
        {
            HideBossActionUI();
            if (unknow != null)
            {
                unknow.SetActive(true);
            }
        }

    }

    // 隐藏所有Boss行为预测UI
    public void HideBossActionUI()
    {
        attack.SetActive(false); 
        defense.SetActive(false);
        dodge.SetActive(false);
        others.SetActive(false);
        unknow.SetActive(false);
    }
}
