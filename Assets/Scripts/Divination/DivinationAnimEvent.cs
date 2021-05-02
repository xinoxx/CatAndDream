using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DivinationAnimEvent : MonoBehaviour
{
    public GameObject divinationGame;
    public GameObject resultPanel;

    [SerializeField] private Sprite negative = null;
    private DivinationGame gameScript;


    void Start()
    {
        gameScript = divinationGame.GetComponent<DivinationGame>();
    }
    public void GetTossResult()
    {
        if (gameScript.num1 == 3 || (gameScript.num1 == 1 && gameScript.num0 == 2))
        {
            // Show toss result in the result panel.
            GameObject child = resultPanel.transform.GetChild(gameScript.tossTimes-1).gameObject;
            child.SetActive(true);
            child.GetComponent<Image>().sprite = negative;
        }
        else if (gameScript.num0 == 3 || (gameScript.num1 == 2 && gameScript.num0 == 1))
        {
            resultPanel.transform.GetChild(gameScript.tossTimes-1).gameObject.SetActive(true);
        }

        // Enable the toss button.
        gameScript.tossButton.GetComponent<Button>().enabled = true;
    }

    public void DisallowTossButton()
    {
        // Disallow continuous button clicking.
        gameScript.tossButton.GetComponent<Button>().enabled = false;
    }
}
