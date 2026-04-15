using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameFramework.Samples.Localization;

public class RoundManager : MonoBehaviour
{
    public int currentbout;
    public bool isEnemyBout;
    public Button next;
    public TMP_Text boutCount;   // 回合数

    // 调用其他脚本
    public APManager ap;
    public RecordAction recordAction;
    public RefreshCombatCardsManager refreshCombatCardsManager;

    void Start()
    {
        currentbout = 1;
        isEnemyBout = false;
        RoundText();
        next.interactable = true;

        if (next != null)
        {
            next.onClick.AddListener(TurnEnemy);
        }
    }

    public void TurnEnemy()
    {
        isEnemyBout = true;
        next.interactable = false;
    }

    public void UpdateBoutCount()
    {
        isEnemyBout = false;
        ap.ResetAP();
        recordAction.Initial();
        next.interactable = true;
        currentbout++;
        refreshCombatCardsManager.ResetTimes(currentbout);
        RoundText();
    }

    // 判断语言显示文字
    public void RoundText()
    {
        boutCount.text = currentbout.ToString();
    }
}
