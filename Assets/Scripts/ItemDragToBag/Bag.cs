using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Bag : MonoBehaviour, IDropHandler
{
    public Inventory inventory;
    [HideInInspector] public bool isAdded = false;
    [HideInInspector] public int itemNum = 0;

    void Start()
    {
        InitBag();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragedItem = eventData.pointerDrag.gameObject;
        if (dragedItem != null)
        {
            Item item = dragedItem.GetComponent<DragItem>().thisItem;
            InventoryManager.CreateNewItem(item);
            inventory.myBag.Add(item);
            PlayerPrefs.SetInt(item.itemName, 1);
            isAdded = true;
            ++itemNum;
        }
    }

    private void InitBag()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneContants.Ch01_Dream)
        {
            itemNum = 0;
            int cnt = inventory.myBag.Count;
            for( int i = 0; i < cnt; i++)
            {
                Item item = inventory.myBag[i];
                if (PlayerPrefs.HasKey(item.itemName))
                {
                    if (PlayerPrefs.GetInt(item.itemName) == 1)
                    {
                        InventoryManager.CreateNewItem(item);
                        itemNum++;
                    }
                }
            }
            PlayerPrefs.SetInt("itemNum", itemNum);
            Debug.Log("bag, itemNum:" + itemNum);
        }
    }
}
