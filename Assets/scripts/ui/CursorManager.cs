using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance = null;

    [SerializeField] private Texture2D pointerSprite;
    [SerializeField] private Texture2D handSprite;

    private void Start()
    {
        Globals.MakeSingleton<CursorManager>(ref instance, gameObject);
        SetToPointer();
    }

    public static void SetToPointer()
    {
        Cursor.SetCursor(instance.pointerSprite, Vector2.zero, CursorMode.Auto);
    }

    public static void SetToHand()
    {
        Cursor.SetCursor(instance.handSprite, Vector2.zero, CursorMode.Auto);
    }
}
