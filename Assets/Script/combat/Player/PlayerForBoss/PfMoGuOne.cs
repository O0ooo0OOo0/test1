using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PfMoGuOne : MonoBehaviour
{
    public int hurt;   // 被召唤出的全部刺毛鳞被清除后，对巨刺毛鳞造成的伤害值
    public int panelInd;   // 玩家所处的板块ID

    // 调用其他脚本
    public SmallCiMaoLin scml;
    public SpreadCi sc;
    public EnemyHPManager enemyHP;

    // 判断是否清除当前场上所有被召唤出的刺毛鳞
    public void IsClearAllCis()
    {
        if (scml.isSummonedCi == true)   // 如果已经召唤出刺毛鳞
        {
            if (scml.count == 0)   // 当前刺毛鳞数量为0：全部清除
            {
                scml.isKillAll = true;
                scml.isSummonedCi = false;   // 当前场上还未召唤刺毛鳞
                enemyHP.AbsoluteDamage(hurt);
            }
        }
    }

    // 获取玩家当前所处板块ID
    public void GetPlayerPanelId()
    {

    }
}
