using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item slotItem;
    public Image slotImage;
    public GameObject slotDetail;

    public void ItemOnClicked()
    {
        InventoryManager.ShowItemInfo(slotItem.itemInformation);
        if (transform.parent.GetComponent<SlotManager>() != null)
        {
            slotDetail = transform.parent.GetComponent<SlotManager>().SetSlotDetail(slotItem);
            if (slotDetail != null)
            {
                slotDetail.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }
}
