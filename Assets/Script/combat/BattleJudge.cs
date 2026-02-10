using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleJudge : MonoBehaviour
{
    public PlayerHPManager hpManager;
    public EnemyHPManager[] enemyManager;
    public EscCombat escCombat;
    public GameObject TipsPanel;
    public GameObject failTips;
    public GameObject successTips;
    public Animator fail;
    public Animator success;
    private int deCount;

    void Start()
    {
        fail = failTips.GetComponent<Animator>();
        success = successTips.GetComponent<Animator>();

        TipsPanel.SetActive(false);
        failTips.SetActive(false);
        fail.SetBool("isFail", false);
        successTips.SetActive(false);
        success.SetBool("isSuccess", false);
    }

    void Update()
    {
        if (hpManager.currentHealth == 0)
        {
            EndBattleFailed();
        }
        else
        {
            foreach (var enemy in enemyManager)
            {
                if (enemy.currentHealth == 0)
                {
                    deCount++;
                }
            }
            if (deCount == enemyManager.Length)
            {
                EndBattleSuccessed();
            }
        }
    }

    public void EndBattleFailed()
    {
        TipsPanel.SetActive(true);
        failTips.SetActive(true);
        fail.SetBool("isFail", true);
        StartCoroutine(ExitToMap());
    }

    public void EndBattleSuccessed()
    {
        TipsPanel.SetActive(true);
        successTips.SetActive(true);
        success.SetBool("isSuccess", true);
        StartCoroutine(ExitToMap());
    }

    IEnumerator ExitToMap()
    {
        yield return new WaitForSeconds(1.8f);
        StartCoroutine(escCombat.ExitScene());
    }
}
