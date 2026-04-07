using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePoint : MonoBehaviour
{
    public Texture2D mouse;

    private void Start()
    {
        Vector2 hotspot = new Vector2(5, mouse.height-5);
        Cursor.SetCursor(mouse, hotspot, CursorMode.ForceSoftware);
    }
}
