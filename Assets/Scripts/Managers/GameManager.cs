using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton Mode
    public static GameManager instance;
    public GameObject menu;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            if (instance != null) Destroy(gameObject);
        }
    }

    public void PauseGame()
    {
        menu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainPage()
    {
        Time.timeScale = 1;
        LoadManager.instance.LoadTargetLevel(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
