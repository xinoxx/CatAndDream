using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoTransition : MonoBehaviour
{
    private VideoPlayer vp = null;

    void Start()
    {
        vp = GetComponent<VideoPlayer>();
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f && vp.frame >= (long)vp.frameCount - 1)
                operation.allowSceneActivation = true;
            yield return null;
        }
    }
}
