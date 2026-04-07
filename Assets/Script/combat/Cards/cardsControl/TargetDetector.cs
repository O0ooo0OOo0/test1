using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using System.Linq;
using TMPro;

public class TargetDetector : MonoBehaviour
{
    private GameObject firstChild;
    private GameObject secondChild;
    private static TargetDetector activeDetector = null;
    private static TargetDetector useObjectDetector = null;
    public bool isTargetInside = false;
    public bool isUseObjectInside = false;
    public GameObject currentTarget; // 当前进入检测区域的目标物体
    public GameObject lastTarget; // 上一个进入检测区域的目标物体
    public GameObject lastNumberA;
    public GameObject lastNumberB;
    public GameObject lastElement;
    public GameObject UseObject;

    public Vector2 boxSize = new Vector2(5f, 5f); // 矩形检测区域的大小：宽和高
    public Vector2 boxCenterOffset = new Vector2(0f, 0f); // 矩形检测区域的中心点偏移：相对于 Transform 的偏移

    public bool istarget;
    public bool isnumber;
    public bool iselement;

    private bool isnumberA;
    private bool isnumberB;

    public string targetContent;
    public int numberContent;
    public string elementContent;

    private void Start()
    {
        firstChild = transform.GetChild(0).gameObject;
        secondChild = transform.GetChild(1).gameObject;

        firstChild.SetActive(false);
        secondChild.SetActive(false);

        // 检测物体的子物体及以下中是否存在标签特定的文本组件物体
        var target = transform.GetComponentsInChildren<TMPro.TextMeshProUGUI>(true).FirstOrDefault(t => t.CompareTag("target"));
        var number = transform.GetComponentsInChildren<TMPro.TextMeshProUGUI>(true).FirstOrDefault(n => n.CompareTag("number"));
        var element = transform.GetComponentsInChildren<TMPro.TextMeshProUGUI>(true).FirstOrDefault(e => e.CompareTag("element"));
        istarget = target;
        isnumber = number;
        iselement = element;

        targetContent = null;
        numberContent = 0;
        elementContent = null;

        JudgeNumberType();
    }

    private void Update()
    {
        CheckForTarget();

        if (currentTarget !=null && currentTarget.GetComponent<CardDraggable>().isUsed == true)
        {
            InputContent();
            currentTarget.GetComponent<CardDraggable>().isUsed = false;
            ResetLastCard();
        }
    }

    private void CheckForTarget()
    {
        Vector2 detectionCenter = (Vector2)transform.position + boxCenterOffset;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(detectionCenter, boxSize, 0);

        bool foundTarget = false;
        bool foundUseObject = false;

        foreach (Collider2D collider in colliders)
        {
            if ((collider.CompareTag("NumberCard") && isnumberA == true) || (collider.CompareTag("MultipleCard") && isnumberB == true) || (collider.CompareTag("TargetCard") && istarget == true) || (collider.CompareTag("ElementCard") && iselement == true))
            {
                foundTarget = true;

                if (isTargetInside == false && activeDetector == null)
                {
                    isTargetInside = true;
                    currentTarget = collider.gameObject;
                    activeDetector = this; // 设置为当前激活检测器
                    firstChild.SetActive(true);
                    secondChild.SetActive(true);
                    EnterFunction();
                }
                break;
            }
        }

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player") || collider.CompareTag("Enemy") || collider.CompareTag("Many"))
            {
                foundUseObject = true;

                if (isUseObjectInside == false && useObjectDetector == null)
                {
                    isUseObjectInside = true;
                    UseObject = collider.gameObject;
                    useObjectDetector = this; // 设置为当前激活检测器
                }
                break;
            }
        }

        // 如果当前检测器是激活状态，但没有检测到目标，则重置状态
        if (activeDetector == this && !foundTarget)
        {
            ExitFunction();
            isTargetInside = false;

            if (currentTarget.CompareTag("TargetCard"))
            {
                lastTarget = currentTarget;
            }
            else if (currentTarget.CompareTag("NumberCard"))
            {
                lastNumberA = currentTarget;
            }
            else if (currentTarget.CompareTag("MultipleCard"))
            {
                lastNumberB = currentTarget;
            }
            else if (currentTarget.CompareTag("ElementCard"))
            {
                lastElement = currentTarget;
            }

            currentTarget = null;
            activeDetector = null;
            firstChild.SetActive(false);
            secondChild.SetActive(false);
        }

        // 如果当前检测器是激活状态，但没有检测到使用目标，则重置状态
        if (useObjectDetector == this && !foundUseObject)
        {
            isUseObjectInside = false;
            UseObject = null;
            useObjectDetector = null;
        }
    }

    // 可选：在编辑器中可视化检测区域
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube((Vector3)transform.position + (Vector3)boxCenterOffset, new Vector3(boxSize.x, boxSize.y, 0));
    }

    //卡牌消耗
    public void EnterFunction()
    {
        currentTarget.GetComponent<CardDraggable>().isEnterAnyCard = true;
    }

    public void ExitFunction()
    {
        currentTarget.GetComponent<CardDraggable>().isEnterAnyCard = false;
    }

    // 记录输入卡的信息
    public void InputContent()
    {
        var target = transform.GetComponentsInChildren<TMPro.TextMeshProUGUI>(true).FirstOrDefault(t => t.CompareTag("target"));
        var number = transform.GetComponentsInChildren<TMPro.TextMeshProUGUI>(true).FirstOrDefault(n => n.CompareTag("number"));
        var element = transform.GetComponentsInChildren<TMPro.TextMeshProUGUI>(true).FirstOrDefault(e => e.CompareTag("element"));

        if (currentTarget.CompareTag("TargetCard") && target == true)
        {
            target.text = currentTarget.GetComponent<CardsTarget>().cardName;
            targetContent = target.text.ToString();
        }
        if (currentTarget.CompareTag("NumberCard") && number == true && !gameObject.CompareTag("live") && elementContent != "生命")
        {
            number.text = currentTarget.GetComponent<CardsNumber>().numberCardValue.ToString();
            numberContent = currentTarget.GetComponent<CardsNumber>().numberCardValue;
            JudgeNumberType();
        }
        if (currentTarget.CompareTag("MultipleCard") && number == true && (gameObject.CompareTag("live") || elementContent == "生命" || elementContent == null))
        {
            number.text = currentTarget.GetComponent<CardsNumber>().numberCardValue.ToString();
            numberContent = currentTarget.GetComponent<CardsNumber>().numberCardValue;
            JudgeNumberType();
        }
        if (currentTarget.CompareTag("ElementCard") && element == true)
        {
            if ((numberContent == 1 || numberContent == 2 || numberContent == 3) && currentTarget.GetComponent<CardsElement>().cardName == "生命")
            {
                return;
            }
            else
            {
                element.text = currentTarget.GetComponent<CardsElement>().cardName;
                elementContent = element.text.ToString();
            }
            JudgeNumberType();
        }
    }

    public void ResetLastCard()
    {
        if (currentTarget.CompareTag("TargetCard") && lastTarget != null)
        {
            lastTarget.GetComponent<CardsTarget>().ResetTargetCard();
        }
        else if (currentTarget.CompareTag("NumberCard") && lastNumberA != null)
        {
            lastNumberA.GetComponent<CardsNumber>().ResetNumberCard();
        }
        else if (currentTarget.CompareTag("MultipleCard") && lastNumberB != null) 
        {
            lastNumberB.GetComponent<CardsNumber>().ResetNumberCard();
        }
        else if (currentTarget.CompareTag("ElementCard") && lastElement != null)
        {
            lastElement.GetComponent<CardsElement>().ResetElementCard();
        }
    }

    public void JudgeNumberType()
    {
        if (isnumber == true)
        {
            if (gameObject.CompareTag("live") || elementContent == "生命")
            {
                isnumberA = false;
                isnumberB = true;
            }
            else if ((!gameObject.CompareTag("live") && elementContent != "生命" && elementContent != null) || (iselement == false && !gameObject.CompareTag("live")))
            {
                isnumberA= true;
                isnumberB= false;
            }
            else if (!gameObject.CompareTag("live") && elementContent == null)
            {
                isnumberA = true;
                isnumberB = true;
            }
        }
        else if (isnumber == false)
        {
            isnumberA = false;
            isnumberB = false;
        }
    }
}