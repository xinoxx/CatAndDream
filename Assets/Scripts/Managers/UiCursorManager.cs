using UnityEngine;
using UnityEngine.EventSystems;

public class UiCursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture = null;

    public void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

}
