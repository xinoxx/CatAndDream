using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bag : MonoBehaviour, IDropHandler
{
    public List<Item> myBag = new List<Item>();
    public bool isAdded = false;
        
    public void OnDrop(PointerEventData eventData)
    {
        Item item = eventData.pointerDrag.GetComponent<UIDragController>().thisItem;
        InventoryManager.CreateNewItem(item);
        myBag.Add(item);
        isAdded = true;
    }
}
