using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public static MouseManager mouse;

    private void Awake()
    {
        if (mouse == null)
        {
            mouse = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // “˛≤ÿ Û±Í
    public void HideMouse()
    {
        Cursor.visible = false;
    }

    // œ‘ æ Û±Í
    public void ShowMouse()
    {
        Cursor.visible = true;
    }

    // «–ªª Û±Íœ‘ æ◊¥Ã¨
    public void ToggleMouse()
    {
        Cursor.visible = !Cursor.visible;
    }
}
