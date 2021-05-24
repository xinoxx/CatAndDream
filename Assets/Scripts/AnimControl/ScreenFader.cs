using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private Image blackIn = null;
    [SerializeField] private Image whiteIn = null;
    [SerializeField] private Image outImage = null;
    [SerializeField] private string fadeInType = "black";

    private float alpha = 1.0f;

    void Start()
    {
        StartCoroutine(FadeIn(fadeInType));
    }

    public void FadeTo(string type)
    {
        StartCoroutine(FadeOut(type));
    }

    IEnumerator FadeIn(string type)
    {
        if (type.Equals("black"))
        {
            blackIn.gameObject.SetActive(true);
            alpha = 1.0f;
            while (alpha > 0)
            {
                alpha -= Time.deltaTime;
                blackIn.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
            blackIn.gameObject.SetActive(false);
        }
        else if (type.Equals("white"))
        {
            whiteIn.gameObject.SetActive(true);
            alpha = 1.0f;
            while (alpha > 0)
            {
                alpha -= Time.deltaTime;
                whiteIn.color = new Color(255, 255, 255, alpha);
                yield return null;
            }
            whiteIn.gameObject.SetActive(false);
        }
    }

    IEnumerator FadeOut(string type)
    {
        outImage.gameObject.SetActive(true);
        alpha = 0.0f;
        while (alpha < 1)
        {
            alpha += Time.deltaTime;
            if (type.Equals("white"))
                outImage.color = new Color(255, 255, 255, alpha);
            else if (type.Equals("black"))
            {
                outImage.color = new Color(0, 0, 0, alpha);
            }
            yield return null;
        }
        LoadManager.instance.LoadTargetLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
