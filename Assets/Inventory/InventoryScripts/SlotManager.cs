using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public GameObject painting;
    public GameObject bear;
    public GameObject balloon;

    public GameObject SetSlotDetail(Item item)
    {
        if (item.itemName.Equals("Family Painting"))
        {
            return painting;
        }
        else if (item.itemName.Equals("Balloon"))
            return balloon;
        else if (item.itemName.Equals("Bear"))
            return bear;
        else
            return null;
    }
}
