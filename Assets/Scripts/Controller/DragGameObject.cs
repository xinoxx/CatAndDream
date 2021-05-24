using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragGameObject : MonoBehaviour
{
    public GameObject originalObj;
    public GameObject targetObj;
    public Texture2D cursor;
    [HideInInspector] public static int dragNum = 0;

    [SerializeField] private Vector2 offset = Vector2.one;
    private Vector3 initPos = Vector3.zero;

    private void OnMouseEnter()
    {
        Vector3 temp = transform.localScale;
        transform.localScale = new Vector3(1.5f, 1.5f, 0.0f);
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Vector3 temp = transform.localScale;
        if (temp.x != 1.0f)
            transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseDrag()
    {
        transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                         Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnMouseDown()
    {
        initPos = transform.position;
    }

    private void OnMouseUp()
    {
        float minX, maxX, minY, maxY;
        Vector2 pos = originalObj.transform.position;
        minX = pos.x - offset.x;
        maxX = pos.x + offset.x;
        minY = pos.y - offset.y;
        maxY = pos.y + offset.y;
        Vector2 thisPos = transform.position;

        if (thisPos.x > minX && thisPos.x < maxX && thisPos.y > minY && thisPos.y < maxY)
        {
            this.gameObject.SetActive(false);
            originalObj.SetActive(true);
            targetObj.SetActive(true);
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            ++dragNum;
        }
        else
        {
            transform.position = initPos;
        }
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);
    }

    }
