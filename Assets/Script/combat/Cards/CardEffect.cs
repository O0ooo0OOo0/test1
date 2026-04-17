using UnityEngine;
using UnityEngine.SceneManagement;

public class CardEffect : MonoBehaviour
{
    public CardEffectToPlayer cardPlayer;
    private CardEffectToEnemy cardEnemy;
    public int enemyCount;

    private void Start()
    {
        enemyCount = 0;
    }

    // 找到当前战斗场景中的CardEffectToEnemy脚本
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;    // 注册场景加载事件
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;   // 取消注册，防止内存泄漏
    }

    // 场景加载完成时调用
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 查找并获取
        GameObject csObj = GameObject.FindWithTag("cs");
        if (csObj != null)
        {
            cardEnemy = csObj.GetComponent<CardEffectToEnemy>();
            if (cardEnemy != null )
            {
                enemyCount = cardEnemy.enemyCount;
            }
        }
    }

    /// <summary>
    /// 减血（受到伤害)
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="count"></param>

    // 单个敌人减血
    public void TakeDamage_Eone(GameObject enemy, int count)     
    {
        cardEnemy.TakeDamage_Eone(enemy, count);
    }

    // 所有敌人减血
    public void TakeDamage_Eall(int count)
    {
        cardEnemy.TakeDamage_Eall(count);
    }

    // 角色减血
    public void TakeDamage_P(int count)   
    {
        cardPlayer.TakeDamage_P(count);
    }

    // 角色减血（无视护盾）
    public void Reduceblood_P(int count)   
    {
        cardPlayer.Reduceblood_P(count);
    }

    // 全部对象减血（包括角色及敌人）
    public void TakeDamage_All(int count)
    {
        cardPlayer.TakeDamage_P(count);
        cardEnemy.TakeDamage_Eall(count);
    }

    /// <summary>
    /// 回血
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="count"></param>

    // 单个敌人回血
    public void Heal_Eone(GameObject enemy, int count)
    {
        cardEnemy.Heal_Eone(enemy, count);
    }

    // 所有敌人回血
    public void Heal_Eall(int count)
    {
        cardEnemy.Heal_Eall(count);
    }

    // 角色回血
    public void Heal_P(int count)
    {
        cardPlayer.Heal_P(count);
    }

    // 全部对象回血
    public void Heal_All(int count)
    {
        cardPlayer.Heal_P(count);
        cardEnemy.TakeDamage_Eall(count);
    }

    /// <summary>
    /// 减防
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="count"></param>

    // 单个敌人减防御值
    public void RemoveDefense_Eone(GameObject enemy, int count)
    {
        cardEnemy.RemoveDefense_Eone(enemy, count);
    }

    // 所有敌人减防
    public void RemoveDefense_Eall(int count)
    {
        cardEnemy.RemoveDefense_Eall(count);
    }

    // 角色减防
    public void RemoveDefense_P(int count)
    {
        cardPlayer.RemoveDefense_P(count);
    }

    // 全部对象减防
    public void RemoveDefense_All(int count)
    {
        cardPlayer.RemoveDefense_P(count);
        cardEnemy.RemoveDefense_Eall(count);
    }

    /// <summary>
    /// 加防
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="count"></param>

    // 单一敌人加防
    public void AddDefense_Eone(GameObject enemy, int count)
    {
        cardEnemy.AddDefense_Eone(enemy, count);
    }

    // 所有敌人加防
    public void AddDefense_Eall(int count)
    {
        cardEnemy.AddDefense_Eall(count);
    }

    // 角色加防
    public void AddDefense_P(int count)
    {
        cardPlayer.AddDefense_P(count);
    }

    // 全部对象加防
    public void AddDefense_All(int count)
    {
        cardPlayer.AddDefense_P(count);
        cardEnemy.AddDefense_Eall(count);
    }
}
