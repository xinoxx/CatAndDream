using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bag : MonoBehaviour, IDropHandler
{
    [HideInInspector] public List<Item> myBag = new List<Item>();
    [HideInInspector] public bool isAdded = false;
        
    public void OnDrop(PointerEventData eventData)
    {
        Item item = eventData.pointerDrag.GetComponent<DragItem>().thisItem;
        InventoryManager.CreateNewItem(item);
        myBag.Add(item);
        isAdded = true;
    }
}
