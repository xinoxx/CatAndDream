using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public List<Item> itemList = new List<Item>();
    public Vector2 playerPos;
    public int sceneNum;
}
