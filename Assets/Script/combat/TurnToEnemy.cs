using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnToEnemy : MonoBehaviour
{
    public Button next;
    public TMP_Text boutCount;
    public int currentbout;
    public bool isEnemyBout;

    public APnumber ap;
    public RecordAction recordAction;

    void Start()
    {
        isEnemyBout = false;
        currentbout = 1;
        boutCount.text = "回合: " + currentbout;

        if (next != null)
        {
            next.onClick.AddListener(TurnEnemy);
        }
        next.interactable = true;
    }

    public void TurnEnemy()
    {
        isEnemyBout = true;
        next.interactable = false;
    }

    public void UpdateBoutCount()
    {
        isEnemyBout = false;
        ap.DefineAP();
        recordAction.Initial();
        next.interactable = true;
        currentbout++;
        boutCount.text = "回合: " + currentbout;
    }
}
