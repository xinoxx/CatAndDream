using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private Image blackImage = null;

    private float alpha = 1.0f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    /*void Update()
    {
        if (vp.frame >= (long)vp.frameCount - 1)
            TransitionByVideoAndFadeIn();
    }*/

    /*private void TransitionByVideoAndFadeIn()
    {
        // The video ends and then fades in.
        cutscene.gameObject.SetActive(false);
        blackImage.gameObject.SetActive(true);

        StartCoroutine(FadeIn());
    }*/

    IEnumerator FadeIn()
    {
        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            blackImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        blackImage.gameObject.SetActive(false);
    }
}
