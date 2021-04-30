using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture = null;
    [SerializeField] private GameObject checkImage = null;

    private void Update()
    {
        if (checkImage.activeSelf)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                // Conceal the item.
                checkImage.SetActive(false);
                // Enable collider of this gameObject.
                this.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    private void OnMouseOver()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseEnter()
    {
        transform.localScale += Vector3.one * 0.1f;
    }

    private void OnMouseExit()
    {
        transform.localScale -= Vector3.one * 0.1f;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseUpAsButton()
    {
        checkImage.SetActive(true);
        // This gameObject can not been click again when checkImage has shown in screen.
        this.GetComponent<BoxCollider2D>().enabled = false;
    }
}
