using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveGame
{
    public List<Item> itemList = new List<Item>();
    public Vector2 playerLocation;
    public int sceneNum;
}
