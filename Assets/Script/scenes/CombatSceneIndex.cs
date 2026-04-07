using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatSceneManager : MonoBehaviour
{
    public static CombatSceneManager csm;

    public int[] combatIndex;   // 战斗场景序列数数组
    public bool isCombatScene;   

    private void Awake()
    {
        if (csm == null)
        {
            csm = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 判断当前场景是否是战斗场景
    public void JudgeIsCombatScene(int index)
    {
        for (int i = 0; i < combatIndex.Length; i++)
        {
            if (index == combatIndex[i])
            {
                isCombatScene = true;
                break;
            }
            isCombatScene = false;
        }
    }
}
