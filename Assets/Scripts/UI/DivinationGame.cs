using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivinationGame : MonoBehaviour
{
    [SerializeField] private GameObject coinBox = null;

    public void ClickCoinBox()
    {
        Destroy(coinBox.gameObject);
        gameObject.SetActive(true);
    }
}
