using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D handCursor;

    public void SetHandCursor()
    {
        Vector2 hotspot = new Vector2(handCursor.width / 2, handCursor.height / 2);
        Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
    }
}
