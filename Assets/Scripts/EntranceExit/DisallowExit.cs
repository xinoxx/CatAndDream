using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisallowExit : MonoBehaviour
{
    public GameObject disallowExitHint;

    void OnTriggerStay2D(Collider2D other)
    {
        disallowExitHint.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        disallowExitHint.SetActive(false);
    }
}
