using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageOutline2D_UI : MonoBehaviour
{
    [Header("描边颜色与粗细")]
    public Color outlineColor = Color.black;
    [Range(0, 8)]
    public int outlineThickness = 2;

    void Start()
    {
        var img = GetComponent<Image>();

        // 添加或获取 Outline（UnityEngine.UI.Outline）
        var outline = gameObject.GetComponent<Outline>();
        if (outline == null) outline = gameObject.AddComponent<Outline>();

        outline.effectColor = outlineColor;
        outline.effectDistance = new Vector2(outlineThickness, -outlineThickness);
    }
}