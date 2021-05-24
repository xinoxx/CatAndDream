using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [SerializeField] GameObject slotGrid = null;
    [SerializeField] Slot slotPrefab = null;
    [SerializeField] private Text itemInfo = null;

    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            if (instance != this) Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        instance.itemInfo.text = "";
    }

    public static void ShowItemInfo(string itemInfo)
    {
        instance.itemInfo.text = itemInfo;
    }

    public static void CreateNewItem(Item item)
    {
        Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        newItem.transform.localScale = Vector3.one;
    }
}
