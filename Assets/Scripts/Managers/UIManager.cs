using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Singleton mode
    public static UIManager instance;

    public void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            if (instance != null) Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void newGameAnim()
    {
        GameObject newGameAnim = GameObject.FindGameObjectWithTag("UI").transform.GetChild(5).gameObject;
        newGameAnim.SetActive(true);
    }
}
