using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DivinationGame : MonoBehaviour
{
    public GameObject coinBox;
    public GameObject coinPanel;
    public GameObject checkHint;
    public int[] coinResult = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    [HideInInspector] public int num1 = 0, num0 = 0;
    [HideInInspector] public int tossTimes = 0;
    [HideInInspector] public GameObject tossButton;

    private GameObject coin1, coin2, coin3;

    void Start()
    {
        coin1 = coinPanel.transform.GetChild(0).gameObject;
        coin2 = coinPanel.transform.GetChild(1).gameObject;
        coin3 = coinPanel.transform.GetChild(2).gameObject;
        tossButton = transform.GetChild(3).gameObject;
    }

    public void ClickCoinBox()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        coinBox.gameObject.SetActive(false);
        gameObject.SetActive(true);
        checkHint.SetActive(false);
    }

    public void TossCoins()
    {
        // Disallow continuous button clicking.
        //tossButton.GetComponent<Button>().enabled = false;

        System.Random random = new System.Random();
        bool times1, times2, times3;
        times1 = random.Next(1, 5) % 2 == 1;
        times2 = random.Next(1, 5) % 2 == 1;
        times3 = random.Next(1, 5) % 2 == 1;
        num0 = num1 = 0;

        int result = 0;
        result = times1 ? ++num0 : ++num1;
        result = times2 ? ++num0 : ++num1;
        result = times3 ? ++num0 : ++num1;
        print("num1:" + num1 + " num0:" + num0);

        Animator anim1 = coin1.GetComponent<Animator>();
        Animator anim2 = coin2.GetComponent<Animator>();
        Animator anim3 = coin3.GetComponent<Animator>();
        anim1.SetBool("tossing", true);
        anim2.SetBool("tossing", true);
        anim3.SetBool("tossing", true);

        anim1.SetBool("isOdd", times1);
        anim2.SetBool("isOdd", times2);
        anim3.SetBool("isOdd", times3);

        anim1.CrossFade("DefalutState", 0, -1, 0);
        anim2.CrossFade("DefalutState1", 0, -1, 0);
        anim3.CrossFade("DefalutState1", 0, -1, 0);

        // result:
        // 111 -> 0   100 -> 0
        // 110 -> 1   000 -> 1
        if (num1 == 3 || (num1 == 1 && num0 == 2))
        {
            coinResult[tossTimes] = 0;
        }
        else if (num0 == 3 || (num1 == 2 && num0 == 1))
        {
            coinResult[tossTimes] = 1;
        }
        tossButton.GetComponent<Button>().enabled = true;
        tossTimes++;

        if (tossTimes >= 6)
        {
            // Can not toss coins.
            tossButton.GetComponent<Button>().enabled = false;
            tossButton.transform.GetChild(1).gameObject.SetActive(true);
            tossButton.GetComponent<EventTrigger>().enabled = false;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    public void ExitGame()
    {
        gameObject.SetActive(false);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        // Enable cat movement.
        GameObject.FindGameObjectWithTag(TagContants.PLAYER).gameObject.GetComponent<CatController>().enabled = true;
    }

}
