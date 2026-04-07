using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // 添加UI命名空间用于按钮交互

public class ZhuziBattle : MonoBehaviour
{
    public Animator zhuzi;
    public int attackWays;
    public RoundManager bout;
    public Button enemy;
    public int pre_boutCount;

    public PlayerHPManager playerHPManager;
    public EnemyHPManager enemyHPManager;
    public DefenseManager defenseNumber;
    public TMP_Text fangyuNumber;
    public TMP_Text gongjiNumber;
    public GameObject fangyuLabel;
    public GameObject gongjiLabel;

    void Start()
    {
        zhuzi = GetComponent<Animator>();
        pre_boutCount = 1;
        CombatWay();

        if (enemy != null)
        {
            enemy.onClick.AddListener(AttackWay);
        }
    }

    private void Update()
    {
        if (pre_boutCount != bout.currentbout)
        {
            CombatWay();
            pre_boutCount = bout.currentbout;
        }
        if (enemyHPManager.currentHealth == 0)
        {
            ClearTips();
        }
    }

    public void CombatWay()
    {
        float randomValue = Random.Range(0f, 1f);
        if (randomValue < 0.3f)
        {
            attackWays = 0;
        }
        else if (randomValue >= 0.3f && randomValue <= 0.8f)
        {
            attackWays = 1;
        }
        else if ( 0.8 < randomValue)
        {
            attackWays = 2;
        }
        AttackTips();
    }

    public void AttackTips()
    {
        if (attackWays == 0)
        {
            fangyuLabel.SetActive(true);
            fangyuNumber.text = "5";
        }
        else if (attackWays == 1)
        {
            gongjiLabel.SetActive(true);
            gongjiNumber.text = "6";
        }
        else if (attackWays == 2)
        {
            fangyuLabel.SetActive(true);
            fangyuNumber.text = "3";

            gongjiLabel.SetActive(true);
            gongjiNumber.text = "4";
        }
    }

    public void ClearTips()
    {
        fangyuLabel.SetActive(false);
        gongjiLabel.SetActive(false);
        fangyuNumber.text = "";
        gongjiNumber.text = "";
        defenseNumber.DefineDefense(0);
    }

    public void AttackWay()
    {
        ClearTips();

        if (attackWays == 0)
        {
            defenseNumber.DefineDefense(5);
            StartCoroutine(Delay());
        }
        else if (attackWays == 1)
        {
            zhuzi.SetBool("isZhuziAttack", true);
            //enemyHPManager.gongjiE = 6;
            StartCoroutine(Attack());
        }
        else if (attackWays == 2)
        {
            //enemyHPManager.fangyuE = 3;
            defenseNumber.DefineDefense(3);
            zhuzi.SetBool("isZhuziAttack", true);
            //enemyHPManager.gongjiE = 4;
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.4f);
        //playerHPManager.TakeDamage(enemyHPManager.gongjiE);

        yield return new WaitForSeconds(0.2f);
        zhuzi.SetBool("isZhuziAttack", false);
        bout.UpdateBoutCount();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.6f);
        bout.UpdateBoutCount();
    }
}