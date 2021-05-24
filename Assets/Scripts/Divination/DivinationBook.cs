using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DivinationBook : MonoBehaviour
{
    public GameObject divinationBook;
    public GameObject checkHint;
    [HideInInspector] public GameObject leftButton, rightButton, content;

    [SerializeField] private List<Sprite> pages = new List<Sprite>();
    private int pageNum = 0;

    void Update()
    {
        isMoving();
    }

    private void isMoving()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        leftButton = transform.GetChild(0).gameObject;
        rightButton = transform.GetChild(1).gameObject;
        content = transform.GetChild(2).gameObject;
    }

    public void ClickBook()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        divinationBook.gameObject.SetActive(false);
        gameObject.SetActive(true);
        checkHint.SetActive(false);
    }

    public void PageTurnForward()
    {
        pageNum--;
        if (pageNum < 0) pageNum = 0;
        if (pageNum < 3)
        {
            rightButton.SetActive(true);
        }
        if (pageNum <= 0)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            leftButton.SetActive(false);
        }

        content.GetComponent<Image>().sprite = pages[pageNum];
    }

    public void PageTurnBack()
    {
        pageNum++;
        if (pageNum > 3) pageNum = 3;
        if (pageNum > 0)
        {
            leftButton.SetActive(true);
        }
        if (pageNum >= 3)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            rightButton.SetActive(false);
        }

        content.GetComponent<Image>().sprite = pages[pageNum];
    }
}
