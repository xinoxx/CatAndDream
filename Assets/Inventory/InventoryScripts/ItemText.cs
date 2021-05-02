using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemText : MonoBehaviour
{
    [SerializeField] private Item item = null;

    void Start()
    {
        GetComponent<Text>().text = item.itemInformation;
    }
}
