using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInWorld : MonoBehaviour
{
    public Bag myBag;
    public GameObject checkHint;
    public GameObject checkObject;

    [HideInInspector] public bool inScale = false;
    [HideInInspector] public bool isShowing = false;

    private EmptySecretBox emptySecretBox = null;

    void Start()
    {
        emptySecretBox = gameObject.GetComponent<EmptySecretBox>();
    }

    void Update()
    {
        CloseObjectDetail();
        ShowObjectDetail();
    }

    private void ShowObjectDetail()
    {
        if (inScale)
        {
            // If player presses F key, the item detail picture will be shown in the screen.
            if (Input.GetKey(KeyCode.F) && !checkObject.activeSelf)
            {
                isShowing = true;
                checkObject.SetActive(true);
            }
        }

        // Disable checkhint.
        if (checkObject.activeSelf && checkHint.activeSelf)
        {
            checkHint.SetActive(false);
        }

        // Disable checkObject if player moves when the checkObject is showing.
        if (isShowing)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                checkObject.SetActive(false);
                isShowing = false;
            }
        }
    }

    private void CloseObjectDetail()
    {
        if (myBag != null && myBag.isAdded && checkObject != null && checkObject.gameObject.activeSelf)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            checkObject.gameObject.SetActive(false);
            myBag.isAdded = false;
            isShowing = false;
            inScale = false;
            if (emptySecretBox != null)
            {
                emptySecretBox.ChangeSecretBoxToEmpty();
            }
            gameObject.SetActive(false);
        }
    }

    // If player moves near the item, the pick action is triggered.
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(TagContants.PLAYER))
        {
            // Pop up the hint information.
            checkHint.SetActive(true);
            inScale = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        checkHint.SetActive(false);
        inScale = false;
    }

}
