using UnityEngine;

public class ParentToggle : MonoBehaviour
{
    private Transform originalParent; // 用于存储原始父级
    private int originalSiblingIndex; // 用于存储原始层级索引

    // 用于设置目标父级和目标层级索引
    public Transform targetParent;
    public int targetSiblingIndex;

    void Start()
    {
        // 保存原始父级和层级索引
        originalParent = transform.parent;
        originalSiblingIndex = transform.GetSiblingIndex();
    }

    // 调用此方法使物体离开父级并提升层级
    public void LeaveParentAndMoveUp()
    {
        if (originalParent != null)
        {
            // 离开父级
            transform.SetParent(null);
        }

        // 设置新的父级和层级索引
        if (targetParent != null)
        {
            transform.SetParent(targetParent);
            transform.SetSiblingIndex(targetSiblingIndex);
        }
    }

    // 调用此方法使物体回到父级
    public void ReturnToParent()
    {
        if (originalParent != null)
        {
            // 将物体重新设置为原始父级的子物体
            transform.SetParent(originalParent);
            // 恢复原始层级索引
            transform.SetSiblingIndex(originalSiblingIndex);
        }
    }
}