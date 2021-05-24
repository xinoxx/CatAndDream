using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour
{
    public void EnableAnim()
    {
        gameObject.SetActive(true);
    }

    public void DisableAnim()
    {
        gameObject.SetActive(false);
    }
}
