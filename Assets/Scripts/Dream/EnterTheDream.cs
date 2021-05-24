using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class EnterTheDream : MonoBehaviour
{
    public GameObject screenFader;
    public GameObject bag;
    public GameObject enterHint;

    [SerializeField] private Texture2D cursorTexture = null;
    [SerializeField] private float speed = 4.0f;
    private Light2D enterLight;
    private bool canEntering = false;

    void Start()
    {
        enterLight = GetComponentInChildren<Light2D>();
    }

    void Update()
    {
        if (canEntering)
        {
            Vector3 scale = enterLight.transform.localScale;
            float offset = speed * Time.deltaTime;
            enterLight.transform.localScale = new Vector3(scale.x + offset,scale.y + offset, scale.z);
            enterLight.intensity += Time.deltaTime;
            if (enterLight.transform.localScale.x > 35)
            {
                canEntering = false;
                screenFader.GetComponent<ScreenFader>().FadeTo("white");
            }
        }
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseDown()
    {
        int itemCnt = bag.GetComponent<Bag>().itemNum;
        if (itemCnt == 3)
        {
            canEntering = true;
        }
        else if (!canEntering && itemCnt < 3)
        {
            enterHint.SetActive(true);
        }
    }

    public void DoNotEnter()
    {
        canEntering = false;
    }

    public void Enter()
    {
        canEntering = true;
    }

}
