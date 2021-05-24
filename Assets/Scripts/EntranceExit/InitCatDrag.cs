using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitCatDrag : MonoBehaviour
{

    [SerializeField] private Texture2D mouseIcon = null;

    void Start()
    {
        GameObject cat = CatController.instance.gameObject;
        cat.AddComponent<DragCat>();
        cat.GetComponent<DragCat>().cursorTexture = mouseIcon;
    }
}
