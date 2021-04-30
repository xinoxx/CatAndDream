using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CheckSecret : MonoBehaviour
{
    public GameObject secretBox;
    public GameObject painting;

    private int openingId;
    private Animator anim;

    void Start()
    {
        openingId = Animator.StringToHash("opening");
        anim = secretBox.GetComponent<Animator>();
    }

    void Update()
    {
        GetSecretNumber();
    }

    private void GetSecretNumber()
    {
        string secret = "000000";
        StringBuilder secretNum = new StringBuilder(secret);
        int len = transform.childCount;
        for (int i = 0; i < len; i++)
        {
            secretNum[i] = transform.GetChild(i).GetComponent<SecretNum>().number;
        }
        secret = secretNum.ToString();
        if (secret.Equals("111111"))
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            anim.SetBool(openingId, true);
            painting.SetActive(true);
            secretBox.GetComponent<BoxCollider2D>().enabled = false;
            secretBox.GetComponent<ItemInWorld>().isShowing = false;
            secretBox.GetComponent<ItemInWorld>().inScale = false;
            Destroy(gameObject);
        }
    }

}
