using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject bag;
    public GameObject endAnim;
    [HideInInspector] public int[] haveChild = new int[10];

    private int childCnt = 0;
    private int itemCnt = 0;
    private float alpha;
    private GameObject endSprite;
    private List<Item> itemList = new List<Item>();
    private int k = 0;
    private bool canPlayAnim = true;
    private bool flag = false;

    void Start()
    {
        endSprite = transform.GetChild(0).gameObject;
        alpha = endSprite.GetComponent<SpriteRenderer>().color.a;
        itemList = bag.GetComponent<Bag>().inventory.myBag;
        childCnt = transform.childCount;
        canPlayAnim = true;
        flag = false;
    }

    void Update()
    {
        if (canPlayAnim)
        {
            EndPicAnim();
        }
    }



    private void CheckHaveChild()
    {
        for (int i = 0; i < childCnt - 1; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            for (int j = 0; j < itemCnt; j++)
            {
                if (itemList[j].itemName.Equals(child.name))
                {
                    haveChild[i] = 1;
                }
            }
        }
        haveChild[childCnt - 1] = 1;
    }

    private void EndPicAnim()
    {
        if (!flag)
        {
            itemCnt = PlayerPrefs.GetInt("itemNum");
            CheckHaveChild();
            flag = true;
        }
        print("DragGameCnt:" + DragGameObject.dragNum + "itemCnt :" + itemCnt);
        if (DragGameObject.dragNum - 1 == itemCnt)
        {
            if (k == 0)
            {
                menu.SetActive(false);
                bag.SetActive(false);
            }
            if (k < childCnt && haveChild[k] == 1)
            {
                alpha += Time.deltaTime;
                endSprite.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, alpha);
            }
            if (alpha >= 1.0f || haveChild[k] == 0)
            {
                k++;
                if (k < childCnt)
                {
                    endSprite = transform.GetChild(k).gameObject;
                    alpha = endSprite.GetComponent<SpriteRenderer>().color.a;
                }
                else
                {
                    endAnim.SetActive(true);
                    canPlayAnim = false;
                }
            }
        }
    }

    
}
