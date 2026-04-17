using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;

public class UseCardDraggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Vector3 offset;
    public bool isDragging = false;
    private Vector3 originalPosition; // 这个变量将存储鼠标点击开始拖拽时的位置
    private float smoothSpeed = 100.0f;

    public bool isNeedTarget;
    public bool isNeedNumber;
    public bool isNeedElement;

    public bool istarget;
    public bool isnumber;
    public bool iselement;

    public bool isCanDrag;
    public bool isCanRefresh;
    public GameObject cards_panel, items_panel;

    // 调用其他脚本
    public CardsBackage cardsBackage;
    public ItemsBackage itemsBackage;
    public APManager ap;

    void Start()
    {
        isCanDrag = false;
        isCanRefresh = true;
        originalPosition = transform.position;
    }

    void Update()
    {
        IsTextFull();
        JudgeDrag();
        JudgeRefresh();
    }

    // 检测是否存在缺失内容及所有缺失内容是否已经补充完整
    public void IsTextFull()
    {
        var target = transform.GetComponentsInChildren<TMPro.TextMeshProUGUI>(true).FirstOrDefault(t => t.CompareTag("target"));
        var number = transform.GetComponentsInChildren<TMPro.TextMeshProUGUI>(true).FirstOrDefault(n => n.CompareTag("number"));
        var element = transform.GetComponentsInChildren<TMPro.TextMeshProUGUI>(true).FirstOrDefault(e => e.CompareTag("element"));

        // 检测是否需要内容
        isNeedTarget = target;
        isNeedNumber = number;
        isNeedElement = element;

        // 检测内容是否完整
        if (target == false)
        {
            istarget = true;
        }
        else if (target == true && target.text != "")
        {
            istarget = true;
        }
        else if (target == true && target.text == "")
        {
            istarget = false;
        }

        if (number == false)
        {
            isnumber = true;
        }
        else if (number == true && number.text != "")
        {
            isnumber = true;
        }
        else if (number == true && number.text == "")
        {
            isnumber = false;
        }

        if (element == false)
        {
            iselement = true;
        }
        else if (element == true && element.text != "")
        {
            iselement = true;
        }
        else if (element == true && element.text == "")
        {
            iselement = false;
        }
    }

    // 判断卡牌是否可被拖动
    public void JudgeDrag()
    {
        if (istarget && isnumber && iselement && cardsBackage.isOpenCardsBackage == false && itemsBackage.isOpenItemsBackage == false && ap.currentValue > 0)
        {
            isCanDrag = true;
        }
        else
        {
            isCanDrag = false;
        }
    }

    // 判断卡牌是否可被刷新
    public void JudgeRefresh()
    {
        if ((isNeedTarget && istarget) || (isNeedNumber && isnumber) || (isNeedElement && iselement) || ap.currentValue == 0)
        {
            isCanRefresh = false;
        }
        else
        {
            isCanRefresh = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isCanDrag == false || cards_panel.activeInHierarchy || items_panel.activeInHierarchy)
        {
            return;
        }
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        StartCoroutine(ReturnToOriginalPosition());
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition += eventData.delta / rectTransform.parent.lossyScale.x;
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
    }

    private IEnumerator ReturnToOriginalPosition()
    {
        while ((transform.position - originalPosition).sqrMagnitude > 0.1f)
        {
            float distance = Vector3.Distance(transform.position, originalPosition);
            float dynamicSpeed = smoothSpeed + (distance * 20f);
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, dynamicSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = originalPosition;
    }
}
