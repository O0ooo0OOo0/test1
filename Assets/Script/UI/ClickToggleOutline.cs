using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ClickToggleOutline : MonoBehaviour, IPointerClickHandler
{
    [Header("Ãè±ßÑùÊ½")]
    public Color outlineColor = new Color(1f, 0.84f, 0f);    // Ãè±ßÑÕÉ«
    [Range(1, 8)]
    public int outlineThickness = 3;   // Ãè±ß´ÖÏ¸

    private Outline outline;
    private bool isOutlined;

    void Start()
    {
        // »ñÈ¡»òÌí¼ÓOutline×é¼ş
        outline = GetComponent<Outline>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
            outline.effectColor = outlineColor;
            outline.effectDistance = new Vector2(outlineThickness, outlineThickness);
        }

        // ³õÊ¼Òş²Ø
        isOutlined = false;
        outline.enabled = false;
    }

    // µã»÷ÇĞ»»Ãè±ß
    public void OnPointerClick(PointerEventData eventData)
    {
        isOutlined = !isOutlined;
        outline.enabled = isOutlined;
    }

    // Òş²ØÃè±ß
    public void HideOutline()
    {
        isOutlined = false;
        outline.enabled = false;
    }

    // ÏÔÊ¾Ãè±ß
    public void ShowOutline()
    {
        isOutlined = true;
        outline.enabled = true;
    }
}