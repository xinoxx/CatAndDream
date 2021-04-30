using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture = null;
    [Header("Collider Size")]
    [SerializeField] private Vector2 openedOffset = Vector2.zero;
    [SerializeField] private Vector2 openedSize = Vector2.zero;

    private BoxCollider2D boxCollider2D;
    private Vector2 originalSize;
    private Vector2 originalOffset;
    private Animator anim;
    private bool isOpening = false;

    private int openId;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        originalSize = boxCollider2D.size;
        originalOffset = boxCollider2D.offset;
        anim = GetComponent<Animator>();
        openId = Animator.StringToHash("open");
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null , Vector2.zero, CursorMode.Auto);
    }

    void OnMouseDown()
    {
        if (!isOpening)
        {
            anim.SetBool(openId, true);
            boxCollider2D.size = openedSize;
            boxCollider2D.offset = openedOffset;
        }
        else
        {
            anim.SetBool(openId, false);
            boxCollider2D.size = originalSize;
            boxCollider2D.offset = originalOffset;
        }
        isOpening = !isOpening;
    }
}
