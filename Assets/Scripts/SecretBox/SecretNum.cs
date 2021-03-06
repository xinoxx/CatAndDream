using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecretNum : MonoBehaviour
{
    [SerializeField] private Sprite newImage = null;
    [SerializeField] private Sprite originalImage = null;
    private bool isChanged = false;
    public char charNum = '0';

    public void ChangeImage()
    {
        // false: set new image(1)
        // true: set original image(0)
        if (!isChanged)
        {
            transform.GetChild(0).gameObject.GetComponent<Image>().sprite = newImage;
            charNum = '1';
            isChanged = true;
        }
        else
        {
            transform.GetChild(0).gameObject.GetComponent<Image>().sprite = originalImage;
            charNum = '0';
            isChanged = false;
        }
    }
}
