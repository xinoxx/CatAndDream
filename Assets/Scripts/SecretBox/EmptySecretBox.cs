using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySecretBox : MonoBehaviour
{
    public GameObject secretBox;

    [SerializeField] private Sprite emptyBox = null;

    public void ChangeSecretBoxToEmpty()
    {
        secretBox.GetComponent<Animator>().enabled = false;
        secretBox.GetComponent<SpriteRenderer>().sprite = emptyBox;
    }
}
