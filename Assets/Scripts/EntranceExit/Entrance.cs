using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{
    void Start()
    {
        CatController.instance.transform.localScale = Vector3.one;
        CatController.instance.transform.position = transform.position;
        if (SceneManager.GetActiveScene().buildIndex > 1)
            DataManager.instance.SaveGame();
    }
}
