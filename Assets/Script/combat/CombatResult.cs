using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatResult : MonoBehaviour
{
    public Animator playerFail;   // 战斗失败动画
    public Animator bossFail;   // 战斗成功动画
    private int deCount;

    // 调用其他脚本
    public PlayerHPManager phpManager;   
    public EnemyHPManager[] ehpManager;

    void Start()
    {
        playerFail = PlayerManager.pm.gameObject.GetComponent<Animator>();

        if (playerFail != null )
        {
            playerFail.SetBool("isFail", false);
        }
        if (bossFail != null)
        {
            bossFail.SetBool("isSuccess", false);
        }
    }

    void Update()
    {
        if (phpManager.currentHealth == 0)
        {
            EndBattleFailed();
        }
        else
        {
            foreach (var ehp in ehpManager)
            {
                if (ehp.currentHealth == 0)
                {
                    deCount++;
                }
            }
            if (deCount == ehpManager.Length)
            {
                EndBattleSuccessed();
            }
        }
    }

    public void EndBattleFailed()
    {
        playerFail.SetBool("isFail", true);
        //StartCoroutine(ExitToMap());
    }

    public void EndBattleSuccessed()
    {
        bossFail.SetBool("isSuccess", true);
        //StartCoroutine(ExitToMap());
    }

    IEnumerator ExitToMap()
    {
        yield return new WaitForSeconds(1.8f);
    }
}
