using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CardDraggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Vector3 offset;
    public bool isDragging = false;
    private Vector3 originalPosition; // 这个变量将存储鼠标点击开始拖拽时的位置
    private float smoothSpeed = 100.0f;

    public ParentToggle parentToggle;
    public CardsTarget cardsTarget;
    public CardsNumber cardsNumber;
    public CardsElement cardsElement;

    public bool isEnterAnyCard;
    public bool isUsed;

    void Start()
    {
        isEnterAnyCard = false;
        isUsed = false;
    }

    // 开始点击进入拖拽
    public void OnPointerDown(PointerEventData eventData)
    {
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;
        originalPosition = transform.position; // 在鼠标点击时记录当前位置

        if (parentToggle != null)
        {
            parentToggle.LeaveParentAndMoveUp();
        }
        if (cardsTarget != null)
        {
            cardsTarget.HideTextAmount();
        }
        if (cardsNumber != null)
        {
            cardsNumber.HideTextAmount();
        }
        if (cardsElement != null)
        {
            cardsElement.HideTextAmount();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        StartCoroutine(ReturnToOriginalPosition());
        UseCard();
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

    // 物体回到原位置
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

        if (parentToggle != null)
        {
            parentToggle.ReturnToParent();
        }
        if (cardsTarget != null)
        {
            cardsTarget.ShowTextAmount();
        }
        if (cardsNumber != null)
        {
            cardsNumber.ShowTextAmount();
        }
        if (cardsElement != null)
        {
            cardsElement.ShowTextAmount();
        }
    }

    public void UseCard()
    {
        if (isEnterAnyCard == true)
        {
            if (cardsTarget != null)
            {
                cardsTarget.UseTargetCard();
            }
            if (cardsNumber != null)
            {
                cardsNumber.UseNumberCard();
            }
            if (cardsElement != null)
            {
                cardsElement.UseElementCard();
            }
            isUsed = true;
            isEnterAnyCard = false;
        }
    }
}